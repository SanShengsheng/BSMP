

(function () {
    $(function () {
        var _withDrawMoneyService = abp.services.app.enterpirsePaymentRecord;
        var _$modal = $('#modalloading');
        var _$form = _$modal.find('form');
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
        $('#searchBtn').click(function () {
            searchWithDrawMoneyRecordList();
        })
        function searchWithDrawMoneyRecordList(page) {
            var query = _$form.serializeFormToObject();
            var startTime = $('#startTime').val() + ' 00:00:00';
            var endTime = $('#endTime').val() + ' 23:59:59';
            var userName = $('#userName').val();
            var withdrawMoneyType = $('#withdrawMoneyType').val();
            var withdrawMoneyState = parseInt($('#withDrawMoneyState').val());
            var orderNumber = $('#orderNumber').val();
            abp.ui.setBusy(_$modal);
            //获取列表
            $.post(abp.appPath + 'ChineseBabies/WithdrawMoney', { page: page, userName: userName, withdrawMoneyState: withdrawMoneyState, startTime: startTime, endTime: endTime, withdrawMoneyType: withdrawMoneyType, orderNumber: orderNumber }, function (content) {
                $('#dataList').html(content);
                ajaxPage();
                abp.ui.clearBusy(_$modal);
            }, "html");
        }

        $(document).on('click', 'UpdateWithDrawRecordState', function (e) {
            var id = $(this).attr("data-withDrawMoney-id");
            var msg = '确定更新该记录的状态为成功吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        e.preventDefault();
                        abp.ui.setBusy(_$modal);
                        $.ajax({
                            url: abp.appPath + 'ChineseBabies/WithDrawMoney/UpdateWithdrawRecord?id=' + id,
                            type: 'POST',
                            contentType: 'application/html',
                            success: function (content) {
                                abp.ui.clearBusy(_$modal);
                                searchWithDrawMoneyRecordList();
                                var result = JSON.parse(content);
                                if (!result.IsSuccess) {
                                    alert(result.ErrorMessage);
                                } else {
                                    alert("更新成功！");
                                }
                                console.log(content);
                            },
                            error: function (XMLHttpRequest, testStatus, error) {
                                console.log(e);
                                alert(error);
                                searchWithDrawMoneyRecordList();
                            }
                        });
                    }
                }
            );
        });

        $(document).on('click', '.ManualPass', function (e) {

            var id = $(this).attr("data-withDrawMoney-id");
            var msg = '确定标记该记录的状态为 成功 吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        e.preventDefault();
                        abp.ui.setBusy(_$modal);
                        $.ajax({
                            url: abp.appPath + 'ChineseBabies/WithDrawMoney/ManualPass?id=' + id,
                            type: 'POST',
                            contentType: 'application/html',
                            success: function (content) {
                                console.log(content);
                                abp.ui.clearBusy(_$modal);
                                searchWithDrawMoneyRecordList();
                                alert(content);
                            },
                            error: function (XMLHttpRequest, testStatus, error) {
                                console.log(testStatus);
                                alert(error);
                                searchWithDrawMoneyRecordList();
                            }
                        });
                    }
                }
            );
        });
        // 云账户审核
        $(document).on('click', '.applyPassedByCloudPlatform', function (e) {
            e.preventDefault();
            var id = $(this).attr("data-withDrawMoney-id");
            var requestType = $(this).attr("data-request-payType");
            var msg = '确定提现吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        e.preventDefault();
                        abp.ui.setBusy(_$modal);
                        var order = {
                            applyId: id,
                            requestPlatform: requestType
                        };
                        abp.ajax({
                            url: '/ChineseBabies/WithdrawMoney/PostOrderAsync',
                            data: JSON.stringify(order)
                        }).done(function (data) {
                            abp.ui.clearBusy(_$modal);
                            //var result = JSON.parse(content.result.result);
                            if (data.code === "0000") {
                                abp.notify.success("操作成功！");
                            } else {
                                abp.notify.error(data.message);
                            }
                            //console.log(content);
                            searchWithDrawMoneyRecordList();
                        }).fail(function (err) {
                            abp.ui.clearBusy(_$modal);
                        });
                    }
                }
            );
        });
        // 微信商户平台 审核
        $(document).on('click', '.applyPassed', function (e) {
            e.preventDefault();
            var id = $(this).attr("data-withDrawMoney-id");
            var msg = '确定提现吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        //_withDrawMoneyService.withDrawMoneyForAgent({ id: id }).done(function (d) {
                        //    refreshAgentList();
                        //});
                        e.preventDefault();
                        abp.ui.setBusy(_$modal);
                        $.ajax({
                            url: abp.appPath + 'ChineseBabies/WithDrawMoney/WithDrawMoney?id=' + id,
                            type: 'POST',
                            contentType: 'application/html',
                            success: function (content) {
                                abp.ui.clearBusy(_$modal);
                                searchWithDrawMoneyRecordList();
                                var result = JSON.parse(content);
                                if (!result.IsSuccess) {
                                    alert(result.ErrorMessage);
                                } else {
                                    alert("恭喜您，提现成功！");
                                }
                                console.log(content);
                            },
                            error: function (XMLHttpRequest, testStatus, error) {
                                console.log(e);
                                alert(error);
                                searchWithDrawMoneyRecordList();
                            }
                        });
                        //$.post(abp.appPath + 'ChineseBabies/WithDrawMoney/WithDrawMoney', { id: id }, function (content) {
                        //    var result = JSON.parse(content);
                        //    if (!result.IsSuccess) {
                        //        alert(result.ErrorMessage);
                        //    } else {
                        //        //alert("提现成功");
                        //        refreshAgentList()
                        //    }
                        //    console.log(content);
                        //})
                    }
                }
            );
        });

        $(document).on('click', '.refusePass', function (e) {
            var id = $(this).attr("data-withDrawMoney-id");
            e.preventDefault();
            abp.message.confirm(
                '确定要拒绝吗？', function (isConfirmed) {
                    if (isConfirmed) {
                        $.post(abp.appPath + 'ChineseBabies/WithDrawMoney/RefuseWithDrawMoney', { id: id }, function (content) {
                            alert(content);
                            refreshAgentList();
                        })
                    }
                }
            );
        });
        // 标记为失败
        $(document).on('click', '.ManualFail', function (e) {
            e.preventDefault();
            var id = $(this).attr("data-withDrawMoney-id");
            var msg = '确定将此条提现申请标记为 失败 吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        e.preventDefault();
                        abp.ui.setBusy(_$modal);
                        var order = {
                            orderId: id
                        };
                        abp.ajax({
                            url: '/ChineseBabies/WithdrawMoney/PostSetOrderFailAsync',
                            data: JSON.stringify(order)
                        }).done(function (data) {
                            abp.ui.clearBusy(_$modal);
                            if (data.msg === null) {
                                abp.notify.success("操作成功！");
                            } else {
                                abp.notify.error(data.msg);
                            }
                            searchWithDrawMoneyRecordList();
                        }).fail(function (err) {
                            abp.ui.clearBusy(_$modal);
                        });
                    }
                }
            );
        });
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
                if (totalPage == '1') {
                    return;
                }
                searchWithDrawMoneyRecordList(page);
            });

        }

        $('#ExportToExcel').on('click', function (e) {

            var query = _$form.serializeFormToObject();
            var startTime = $('#startTime').val() + ' 00:00:00';
            var endTime = $('#endTime').val() + ' 23:59:59';
            var userName = $('#userName').val();
            var withdrawMoneyType = $('#withdrawMoneyType').val();
            var withdrawMoneyState = parseInt($('#withDrawMoneyState').val());
            var orderNumber = $('#orderNumber').val();
            //获取列表
            $.post(abp.appPath + 'ChineseBabies/WithDrawMoney/ExportToExcel', { userName: userName, withdrawMoneyState: withdrawMoneyState, startTime: startTime, endTime: endTime, withdrawMoneyType: withdrawMoneyType, orderNumber: orderNumber }, function (content) {
                //let blob = new Blob([content], {
                //    //type: 'application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
                //    type:'application/vnd.ms-excel;charset=utf-8'
                //})
                //if (window.navigator.msSaveOrOpenBlob) {
                //    navigator.msSaveBlob(blob);
                //} else {
                //    let elink = document.createElement('a');
                //    elink.download = "提现记录表.xlsx";
                //    elink.style.display = 'none';
                //    elink.href = URL.createObjectURL(blob);
                //    document.body.appendChild(elink);
                //    elink.click();
                //    document.body.removeChild(elink);
                //}
                window.open(abp.appPath + content);
            });
        })


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