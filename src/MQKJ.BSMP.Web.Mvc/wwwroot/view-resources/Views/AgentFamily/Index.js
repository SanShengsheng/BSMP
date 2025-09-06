



(function () {

    window.onload = function () {
        $(window.document).on('click', '.vtl-rcg', function (e) {
            var $self = $(this);
            e.preventDefault();
            var playerid = $(this).data("player");
            var agent_id = $(this).data("agent-id");
            if (!playerid) return;
            var promise = abp.services.app.coinRecharge.toggleVtlRhgPerm(playerid);
            abp.ui.setBusy(null, promise);
            promise.then(function (data) {
                if (data.success) {
                    $self.hide();
                    $self.siblings().show();
                    //abp.ui.alert("设置成功");

                } else {
                    //abp.ui.alert("设置失败");
                }
            });
            promise.always(function () {
                abp.ui.clearBusy();
            });
        });
    }

    $(function () {
        var _agentService = abp.services.app.mqAgent;
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

        function searchAgentList(page) {
            var search = new Object();
            search.startTime = $('#startTime').val() + ' 00:00:00';
            search.endTime = $('#endTime').val() + ' 23:59:59';
            search.name = $('#nickName').val();
            search.phoneNumber = $('#phone').val();
            search.pageIndex = page;
            search.pageSize = (page - 1) * 10;
            search.sorting = $('#sorting').val();
            search.sortType = $('#SortingType').val();
            search.companyName = $('#companyName').val();
            abp.ui.setBusy(_$loadingModal);
            //获取列表
            $.post(abp.appPath + 'ChineseBabies/AgentFamily', { input: search }, function (content) {
                $('#dataList').html(content);
                ajaxPage();
                //searchAgentList(page);
                abp.ui.clearBusy(_$loadingModal);
            }, "html");
        }

        $('#searchBtn').click(function () {
            var page = getCurrentPage();
            searchAgentList(1);
        })


        function getCurrentPage() {
            var page = parseInt($(".pagination .active span").text());
            if (page.isNaN) {
                page = 1;
            }

            return page;
        }

        $('#ExportToExcel').on('click', function (e) {
            var search = new Object();
            search.startTime = $('#startTime').val() + ' 00:00:00';
            search.endTime = $('#endTime').val() + ' 23:59:59';
            search.name = $('#nickName').val();
            search.phoneNumber = $('#phone').val();
            search.sorting = $('#sorting').val();
            search.sortType = $('#SortingType').val();
            abp.ui.setBusy(_$loadingModal);
            $.post(abp.appPath + 'ChineseBabies/AgentFamily/ExportToExcel', { input: search }, function (content) {
                console.log(content);
                window.open(abp.appPath + content);
                abp.ui.clearBusy(_$loadingModal);
            });
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
                    page = totalPage;
                } else if (isNaN(parseInt(page))) {
                    return;
                }
                if (totalPage == '1')
                    return;
                searchAgentList(page);
            });

        }

        function refreshAgentList() {
            //location.reload(true);
            ajaxPage();
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