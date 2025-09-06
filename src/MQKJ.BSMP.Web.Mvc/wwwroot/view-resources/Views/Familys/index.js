(function () {
    var _self = _self || {};
    _self.orders = [];
    _self.each = function (arr, callback) {
        for (var i = 0; i < arr.length; i++) {
            callback && callback(arr[i], i);
        }
    }
    _self.removeBy = function (arr, callback) {
        if (!(arr && Object.prototype.toString.call(arr) === '[object Array]')) return;
        for (var i = 0; i < arr.length; i++) {
            if (callback && callback(arr[i], i)) {
                return arr.splice(i, 1);
            }
        }
    }
    $(function () {
        var _coinRechargeService = abp.services.app.coinRecharge;
        var _agentService = abp.services.app.mqAgent;
        var _$modal = $('#rechargeCoinModal');
        var _$loadingModal = $('#loadingModal');
        var _$form = _$modal.find('form');
        var _$subsidyModal = $("#subsidyModal");
        ajaxPage();
        laydate.render({
            elem: '#startTime',
            format: 'yyyy-MM-dd',
            value: getDateDiff(30),
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });
        laydate.render({
            elem: '#endTime',
            //max: getNowFormatDate(),
            value: getNowFormatDate(),
            format: 'yyyy-MM-dd',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });
        laydate.render({
            elem: '#subsidyDate',
            type: 'datetime',
            //max: getNowFormatDate(),
            value: getNowFormatDate() + " 12:00:00",
            format: 'yyyy-MM-dd HH:mm:ss',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });
        //添加字段排序
        $(document).on('click', '.sort-by-money,.sort-by-recharge', function (e) {
            var $self = $(this);
            var ordertype = $self.attr("data-order");
            if ($self.hasClass("sort-by-money")) {

                if (ordertype === "0") {
                    _self.orders.push({
                        fieldType: 1,
                        orderType: 1
                    });
                } else {
                    _self.removeBy(_self.orders, function (item) {
                        return item.fieldType === 1;
                    });
                    if (ordertype === "1") {
                        _self.orders.push({
                            fieldType: 1,
                            orderType: 2
                        });
                    } else {
                        _self.removeBy(_self.orders, function (item) {
                            return item.fieldType === 1;
                        });
                    }
                }

            }
            if ($self.hasClass("sort-by-recharge")) {

                if (ordertype === "0") {
                    _self.orders.push({
                        fieldType: 2,
                        orderType: 1
                    });
                } else {
                    _self.removeBy(_self.orders, function (item) {
                        return item.fieldType === 2;
                    });
                    if (ordertype === "1") {
                        _self.orders.push({
                            fieldType: 2,
                            orderType: 2
                        });
                    } else {
                        _self.removeBy(_self.orders, function (item) {
                            return item.fieldType === 2;
                        });
                    }
                }
            }
            searchFamilyList(_self.page);
            console.log(_self.orders);

        });
        function searchFamilyList(page) {
            var fatherName = $('#fatherName').val();
            var motherName = $('#motherName').val();
            var babyName = $('#babyName').val();
            var familyLevel = $('#familyLevel').val();
            var rechargeLevel = parseInt($('#rechargeRange').val());
            var startTime = $('#startTime').val() + " 00:00:00";
            var endTime = $('#endTime').val() + " 23:59:59";
            abp.ui.setBusy(_$loadingModal);
            //获取列表
            $.ajax({
                type: 'post',

                url: abp.appPath + 'ChineseBabies/Family/IndexAjax',
                data: JSON.stringify({
                    page: page,
                    fatherName: fatherName,
                    motherName: motherName,
                    babyName: babyName,
                    familyLevel: familyLevel,
                    rechargeLevel: rechargeLevel,
                    startTime: startTime,
                    endTime: endTime,
                    orders: _self.orders
                }),
                contentType: "application/json; charset=utf-8",
                dataType: 'html'
            }).then(function (content) {
                $('#dataList').html(content);
                if (_self.orders.length > 0) {
                    _self.each(_self.orders, function (item) {
                        if (item.fieldType == 1) {
                            $(".sort-by-money").attr("data-order", item.orderType);
                            if (item.orderType === 1) {
                                $(".sort-by-money").find("span").removeClass();
                                $(".sort-by-money").find("span").addClass("glyphicon glyphicon-sort-by-attributes");
                            } else {
                                $(".sort-by-money").find("span").removeClass();
                                $(".sort-by-money").find("span").addClass("glyphicon glyphicon-sort-by-attributes-alt");
                            }

                        } else if (item.fieldType == 2) {
                            $(".sort-by-recharge").attr("data-order", item.orderType);
                            if (item.orderType === 1) {
                                $(".sort-by-recharge").find("span").removeClass();
                                $(".sort-by-recharge").find("span").addClass("glyphicon glyphicon-sort-by-attributes");
                            } else {
                                $(".sort-by-recharge").find("span").removeClass();
                                $(".sort-by-recharge").find("span").addClass("glyphicon glyphicon-sort-by-attributes-alt");
                            }
                        }
                    })
                } else {
                }
                ajaxPage();
                abp.ui.clearBusy(_$loadingModal);
            });
        }

        $('#searchBtn').click(function () {
            searchFamilyList();
        })

        //分页
        function ajaxPage() {
            var startIndex = $('.PagedList-pageCountAndLocation a').text().lastIndexOf('共');
            var totalPage = $('.PagedList-pageCountAndLocation a').text().substring(startIndex + 1).replace(/[^0-9]/ig, "");
            $(".pagination a").each(function () {
                $(this).attr("href", "javascript:void(0)");
            });
            $(".pagination a").click(function (obj) {
                var page = $(this).text();
                if (page.indexOf('首页') != -1) {
                    page = 1;
                } else if (page.indexOf('末页') != -1) {
                    page = totalPage
                } else if (isNaN(parseInt(page))) {
                    return;
                }
                if (totalPage == '1')
                    return;
                _self.page = page;
                searchFamilyList(page);
            });

        }

        _$modal.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var familyId = $(this).val();
            var coinCount = $('#coinCount').val();
            var orderNumber = $('#orderNumber').val();
            var msg = '确定要为该家庭充值金币吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        var page = parseInt($(".pagination .active span").text());
                        if (page.isNaN) {
                            page = 1;
                        }
                        abp.ui.setBusy(_$loadingModal);
                        _coinRechargeService.v2_SupplementCoinForFamily({ familyId: familyId, coinCount: coinCount, orderNumber: orderNumber }).done(function (data) {
                            abp.ui.clearBusy(_$loadingModal);
                            searchFamilyList(page);
                            $('#rechargeCoinModal').modal('hide');
                        }).always(function (result) {
                            console.log(result);
                            abp.ui.clearBusy(_$loadingModal);
                        });
                    }
                }
            );
        });

        $('#rechargeCoinModal').on('show.bs.modal', function (event) {

            var btnThis = $(event.relatedTarget); //触发事件的按钮
            var modal = $(this);  //当前模态框
            var familyId = $(btnThis).attr("data-family-id");
            $("#familyId").val(familyId);
        });
        $('#subsidyModal').on('show.bs.modal', function (event) {
            var btnThis = $(event.relatedTarget); //触发事件的按钮
            var modal = $(this);  //当前模态框
            var familyId = $(btnThis).attr("data-family-id");
            $("#subsidy_btn").val(familyId);
            $("#babyNameLabel").html($(btnThis).attr("data-baby-name"));
        });
        $('#rechargeCoinModal').on('show.bs.modal', function (event) {

            var btnThis = $(event.relatedTarget); //触发事件的按钮
            var modal = $(this);  //当前模态框
            var familyId = $(btnThis).attr("data-family-id");
            $("#familyId").val(familyId);
        });

        $('#ExportToExcel').on('click', function (e) {
            var fatherName = $('#fatherName').val();
            var motherName = $('#motherName').val();
            var babyName = $('#babyName').val();
            var familyLevel = $('#familyLevel').val();
            var rechargeLevel = parseInt($('#rechargeRange').val());
            var startTime = $('#startTime').val() + " 00:00:00";
            var endTime = $('#endTime').val() + " 23:59:59";
            $.ajax({
                type: 'post',
                url: abp.appPath + 'ChineseBabies/Family/ExportToExcel',
                data: JSON.stringify({
                    //page: 100,//
                    fatherName: fatherName,
                    motherName: motherName,
                    babyName: babyName,
                    familyLevel: familyLevel,
                    rechargeLevel: rechargeLevel,
                    startTime: startTime,
                    endTime: endTime,
                    orders: _self.orders
                }),
                contentType: "application/json; charset=utf-8",
                dataType: 'html',
                success: function (content) {
                    window.open(abp.appPath + content);
                },
            });
        })


        function refreshAgentList() {
            location.reload(true);
            //$dataTableHot.bootstrapTable('refresh');
        }

        //格式化日期
        function getNowFormatDate() {
            var date = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate;
            return currentdate;
        }

        //获取与当前日期相差天数的日期
        function getDateDiff(number) {
            var nowDate = new Date();
            var weekDate = new Date(nowDate - number * 24 * 3600 * 1000);
            var seperator1 = "-";
            var seperator2 = ":";
            var month = weekDate.getMonth() + 1;
            var strDate = weekDate.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = weekDate.getFullYear() + seperator1 + month + seperator1 + strDate;

            return currentdate;
        }

        $('#sendMsgCode').click(function () {
            $.ajax({
                type: 'post',
                url: abp.appPath + 'ChineseBabies/Family/SendMsgCode',
                contentType: "application/json; charset=utf-8",
                dataType: 'html',
                success: function (content) {
                    if (content === 'True') {
                        alert("短信验证码发送成功");
                    } else {
                        alert("短信验证码发送失败");
                    }
                },
            });
        });

        // 提交补贴流水
        _$subsidyModal.find('#subsidy_btn').click(function (e) {
            e.preventDefault();
            var familyId = $(this).val();
            var subsidyMoney = $('#subsidyMoney').val();
            var smsCode = $("#SMSCode").val();
            var draww = $("input[name='subsidyPerson']:checked").val();
            var subsidyDate = $("#subsidyDate").val();
            if (smsCode == '' || smsCode == null || smsCode == undefined) {
                alert("验证码不能为空！");
                return;
            }
            var re = /^[0-9]+.?[0-9]*/;
            if (!re.test(subsidyMoney)) {
                alert("请输入有效的金额！");
                return;
            }
            var msg = '确定要为该家庭补贴？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        var page = parseInt($(".pagination .active span").text());
                        if (page.isNaN) {
                            page = 1;
                        }
                        abp.ui.setBusy(_$loadingModal);
                        _coinRechargeService.subsidyMoney({
                            familyId: familyId,
                            subsidyMoneyAmount: subsidyMoney,
                            SMSCode: smsCode,
                            draww: draww,
                            subsidyDate: subsidyDate
                        }).done(function (data) {
                            abp.ui.clearBusy(_$loadingModal);
                            abp.notify.success("提交成功！");
                            searchFamilyList(page);
                            $('#subsidyModal').modal('hide');
                        }).always(function (result) {
                            console.log(result);
                            abp.ui.clearBusy(_$loadingModal);
                        });
                    }
                }
            );
        });
    });


})();