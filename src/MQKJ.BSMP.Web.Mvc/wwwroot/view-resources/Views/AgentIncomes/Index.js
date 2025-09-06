
(function () {
	$(function () {

        var _agentService = abp.services.app.mqAgent;
        var _$modal = $('#setRatioModal');
        var _$sourcemodal = $('#setSourceModal');
        var _$form = _$modal.find('form');
        var _$loadingModal = $('#loadingModal');
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

        function searchAgentIncomeList(page) {
            var userName = $('#nickName').val();
            var upAgentName = $('#upAgentName').val();
            var sorting = $('#sorting').val();
            var sortType = $('#SortingType').val();
            var startTime = $('#startTime').val() + ' 00:00:00';
            var endTime = $('#endTime').val() + ' 23:59:59';
            var phone = $('#phoneNumber').val();
            var company = $('#company').val();
            abp.ui.setBusy(_$loadingModal);
            //获取列表
            $.get(abp.appPath + 'ChineseBabies/AgentIncome/index', { page: page, userName: userName, upAgentName: upAgentName, sorting: sorting, sortType: sortType, startTime: startTime, endTime: endTime, phone: phone, company: company }, function (content) {
                $('#dataList').html(content);
                ajaxPage();
                abp.ui.clearBusy(_$loadingModal);
            }, "html");
        }

        $('#searchBtn').click(function ()
        {
            searchAgentIncomeList();
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
                console.log($(this));
                if (page.indexOf('首页') != -1) {
                    page = 1;
                } else if (page.indexOf('末页') != -1) {
                    page = totalPage;
                } else if (isNaN(parseInt(page))) {
                    return;
                }
                if (totalPage == '1')
                    return;
                searchAgentIncomeList(page);
            });

        }

        $('#ExportToExcel').on('click', function (e) {
            var userName = $('#nickName').val();
            //var upAgentName = $('#upAgentName').val();
            var sorting = $('#sorting').val();
            var sortType = $('#SortingType').val();
            var startTime = $('#startTime').val() + ' 00:00:00';
            var endTime = $('#endTime').val() + ' 23:59:59';
            var phone = $('#phoneNumber').val();
            abp.ui.setBusy(_$loadingModal);
            $.post(abp.appPath + 'ChineseBabies/AgentIncome/ExportToExcel', { userName: userName, sorting: sorting, sortType: sortType, startTime: startTime, endTime: endTime, phone: phone }, function (content) {
                console.log(content);
                window.open(abp.appPath + content);
                abp.ui.clearBusy(_$loadingModal);
            });
        })

        function refreshAgentList() {
            //location.reload(true);
            ajaxPage();
        }

        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
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

	});
})();