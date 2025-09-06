// (function () {
//     $(function () {

//         var _agentService = abp.services.app.order;
//         var _$modal = $('#searchform');
//         var _$form = _$modal.find('form');

//         laydate.render({
//             elem: '#startTime',
//             format: 'yyyy-MM-dd',
//             value: getDateDiff(30),
//             theme: 'molv',
//             done: function (value, date, endDate) {
//             }
//         });
//         laydate.render({
//             elem: '#endTime',
//             max: getNowFormatDate(),
//             value: getNowFormatDate(),
//             format: 'yyyy-MM-dd',
//             theme: 'molv',
//             done: function (value, date, endDate) {
//             }
//         });


//         $('#searchBtn').click(function () {
//             searchPlayerList();
//         })

//         //查询
//         function searchPlayerList() {
//               var startTime = $('#startTime').val();
//             var endTime = $('#endTime').val();
//             var userName = $('#userName').val();
//             var state = parseInt($('#orderState').val());
//             $.post(abp.appPath + 'ChineseBabies/Order', { userName: userName, startTime: startTime, endTime: endTime, state: state }, function (content) {
//                 $('#dataList').html(content);
//             },"html"); 
//         }
//         //刷新
//         function refreshCodeList() {
//             location.reload(true); //reload page to see new user!
//         }

//         //格式化日期
//         function getNowFormatDate() {
//             var date = new Date();
//             var seperator1 = "-";
//             var seperator2 = ":";
//             var month = date.getMonth() + 1;
//             var strDate = date.getDate();
//             if (month >= 1 && month <= 9) {
//                 month = "0" + month;
//             }
//             if (strDate >= 0 && strDate <= 9) {
//                 strDate = "0" + strDate;
//             }
//             var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate;
//             return currentdate;
//         }

//         //获取与当前日期相差天数的日期
//         function getDateDiff(number) {
//             var nowDate = new Date();
//             var weekDate = new Date(nowDate - number * 24 * 3600 * 1000);
//             var seperator1 = "-";
//             var seperator2 = ":";
//             var month = weekDate.getMonth() + 1;
//             var strDate = weekDate.getDate();
//             if (month >= 1 && month <= 9) {
//                 month = "0" + month;
//             }
//             if (strDate >= 0 && strDate <= 9) {
//                 strDate = "0" + strDate;
//             }
//             var currentdate = weekDate.getFullYear() + seperator1 + month + seperator1 + strDate;

//             return currentdate;
//         }
//     });
// })();

var loading;
var vueapp = new Vue({
    el: '#vueapp',
    data: function () {
        return {
            states: [{
                    text: '所有',
                    value: 0
                },
                {
                    text: '未付款',
                    value: 1
                },
                {
                    text: '已付款',
                    value: 2
                },
                {
                    text: '支付失败',
                    value: 99
                },
            ],
            PaymentTypes:[{
                text: '所有',
                value: 0
            },
                {
                    text: '微信公众号',
                    value: 1
                },
                {
                    text: '微信小程序',
                    value: 2
                },
                {
                    text: '支付宝',
                    value: 3
                },
            ],
            //agentCounts: [{
            //    text: '所有',
            //    value: 0
            //},
            //{
            //    text: '单代理',
            //    value: 1
            //},
            //{
            //    text: '双代理',
            //    value: 2
            //},
            //{
            //    text: '无代理',
            //    value: 3
            //},
            //],
            search: {
                orderState: 0,
                userName: '',
                startTime: '',
                endTime: '',
                orderNumber: '',
                PaymentType: 0,
                Amount: 0,
                tenantId:0,
                pageIndex: 1,
                pageSize: 10,
            },
            tenants:[],
            rechargeList:[],
            items: [],
            pickerdate: [],
            pickerOptions2: {
                shortcuts: [{
                    text: '最近一周',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                        picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近一个月',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                        picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近三个月',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                        picker.$emit('pick', [start, end]);
                    }
                }]
            },
            loaditem: null,
            total: 0
        };
    },
    created: function () {
        var end = new Date();
        var start = new Date();
        start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
        this.pickerdate = [dateFormat('yyyy-MM-dd', start), dateFormat('yyyy-MM-dd', end)];
        this.onsearch();

    },
    methods: {
        onsearch: function () {
            var url = '/ChineseBabies/order/Search';
            var self = this;
            self.showloading('加载中');
            self.search.startTime = self.pickerdate[0] + ' 00:00:00';
            self.search.endTime = self.pickerdate[1] + ' 23:59:59';
            $.post(abp.appPath + 'ChineseBabies/Order/search', self.search, function (res) {
                self.hidLoading();
                if (res.success) {
                    var orderList = res.result.orderListDto;
                    self.total = orderList.totalCount;
                    self.items = orderList.items;
                    self.rechargeList = res.result.coinRechargeListDto.items;
                    self.tenants = res.result.tenants;
                } else {
                    self.$message.error(res.error);
                }
            });
        },
        showloading: function (title) {
            var self = this;
            loading = self.$loading({
                lock: true,
                text: title,
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
        },
        hidLoading: function () {
            setTimeout(function () {
                loading.close();
            }, 500);

        },
        formatDate: function (row, column, cellValue, index) {
            console.log(cellValue);
            if (cellValue == null) {
                return "无";
            }
            var date = new Date(cellValue);
            // return date;
            return dateFormat('yyyy-MM-dd hh:mm', date);
        },
        getStateText: function (row, column, cellValue, index) {
            switch (cellValue) {
                case 1:
                    return '未付款';
                case 2:
                    return '已付款';
                case 99:
                    return '付款失败';
                default:
                    return '未知';
            }
        },
        getPaymentType: function (row, column, cellValue, index) {
            switch (cellValue) {
                case 1:
                    return '微信公众号';
                case 2:
                    return '小程序支付';
                case 3:
                    return '支付宝';
                default:
                    return '未知';
            }
        },
        handleSizeChange: function (val) {
            var self = this;
            self.search.pageSize = val;
            self.onsearch();
            console.log(`每页 ${val} 条`);
        },
        handleCurrentChange: function (val) {
            console.log(`当前页: ${val}`);
            var self = this;
            self.search.pageIndex = val;
            self.onsearch();
        },
        queryOrderSate:function(ordernumber){
            console.log('开始查询订单，订单号：', ordernumber);
            var self = this;
            var url = 'ChineseBabies/order/QueryOrderState';
            var params = {
                OutTradeNo:ordernumber
            };
            self.showloading('加载中');

            $.post(abp.appPath + url, params, function (res) {
                self.hidLoading();
                console.log(res);
                if (res.success) {
                    var result = res.result;
                    console.log('支付状态：', result.isSuccess);
                    if(!result.isSuccess){
                        self.onsearch();
                    }
                } else {
                    self.$message.error(res.error);
                }
            });
        },
        ExportToExcel: function () {
            var self = this;
            self.showloading('加载中');
            self.search.startTime = self.pickerdate[0] + ' 00:00:00';
            self.search.endTime = self.pickerdate[1] + ' 23:59:59';
            $.post(abp.appPath + 'ChineseBabies/Order/ExportToExcel', self.search, function (res) {
                self.hidLoading();
                console.log(res);
                window.open(abp.appPath + res);
            });
        }
    }
})