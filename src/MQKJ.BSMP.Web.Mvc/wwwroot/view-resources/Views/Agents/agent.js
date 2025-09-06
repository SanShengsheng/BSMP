


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
        var _$modal = $('#setRatioModal');
        var _$sourcemodal = $('#setSourceModal');
        var _$form = _$modal.find('form');
        var _$loadingModal = $('#loadingModal');
        var _$companymodal = $('#setCompanyModal');
        var _$companyForm = _$companymodal.find("form");
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
            var $searchForm = $("#searchform");
            var query = $($searchForm).serializeFormToObject();
            var startTime = $('#startTime').val() + ' 00:00:00';
            var endTime = $('#endTime').val() + ' 23:59:59';
            var userName = $('#nickName').val();
            var state = parseInt($('#agentState').val());
            var agentLevel = parseInt($('#agentLevel').val());
            var sourceName = $('#sourceName').val();
            var phoneNumber = $('#phone').val();
            var company = $('#company').val();
            phoneNumber = phoneNumber && String.prototype.trim.call(phoneNumber);
            if (phoneNumber!=='' && !/^[0-9]{11}$/.test(phoneNumber)) {
                abp.message.error("请核对手机号码格式!");
                return;
            }
            var upperNickName = $('#upperNickName').val();
            var promoterWithdrawalRatio = $('#promoterWithdrawalRatio').val();
            if (promoterWithdrawalRatio !== '' && !/(^[0]{1}$)|(^[1-9]{1}[0-9]{0,100}$)/.test(promoterWithdrawalRatio)) {
                abp.message.error("请核对推广提现比例参数格式");
                return;
            }
            var agentWithdrawalRatio = $('#agentWithdrawalRatio').val();
            if (agentWithdrawalRatio !== '' && !/(^[0]{1}$)|(^[1-9]{1}[0-9]{0,100}$)/.test(agentWithdrawalRatio)) {
                abp.message.error("请核对代理提现比例参数格式");
                return;
            }
            abp.ui.setBusy(_$loadingModal);
            //获取列表
            $.post(abp.appPath + 'ChineseBabies/Agent', { promoterWithdrawalRatio: promoterWithdrawalRatio, agentWithdrawalRatio: agentWithdrawalRatio, upperNickName: upperNickName, phone: phoneNumber, page: page, starttime: startTime, endtime: endTime, username: userName, state: state, agentlevel: agentLevel, sourceName: sourceName, company: company }, function (content) {
                $('#dataList').html(content);
                ajaxPage();
                abp.ui.clearBusy(_$loadingModal);
            }, "html");
        }

        $('#searchBtn').click(function ()
        {
            searchAgentList();
            //var $searchForm = $("#searchform");
            //var query = $($searchForm).serializeFormToObject();
            //var startTime = $('#startTime').val();
            //var endTime = $('#endTime').val();
            //var userName = $('#nickName').val();
            //var state = parseInt($('#agentState').val());
            //var agentLevel = parseInt($('#agentLevel').val());

            //var url = '/ChineseBabies/Agent?starttime=' + startTime + '&endtime=' + endTime + '&username=' + userName + '&state=' + state + '&agentlevel=' + agentLevel;

            //window.location.href = url;
            //return;
            //获取列表
            //$.get(abp.appPath + 'ChineseBabies/Agent', { userName: userName, startTime: startTime, endTime: endTime, state: state, agentLevel: agentLevel }, function (content) {
            //    $('#dataList').html(content);
            //}, "html"); 
        })
        $(document).on('click', '.editstate', function (e) {
            e.preventDefault();
            var id = $(this).attr("data-agent-id");
            var state = parseInt($(this).attr("data-agent-state"));
            var stateName = $(this).attr("data-state-name");
            var page = getCurrentPage();
            var msg = '确定要' + stateName + '该用户吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy(_$loadingModal);
                        $.ajax({
                            url: abp.appPath + 'ChineseBabies/Agent/UpdateAgentState',
                            type: 'POST',
                            data: { id: id },
                            success: function (content) {
                                abp.ui.clearBusy(_$loadingModal);
                                if (content == null || content == '') {
                                    searchAgentList(page);
                                    alert('操作成功');
                                } else {
                                    alert(content);
                                }
                            },
                            error: function (e) {
                                console.log(e);
                                abp.ui.clearBusy(_$loadingModal);
                            }
                        });
                        //_agentService.updateAgentState({ id: id }).done(function (d) {
                        //    abp.ui.clearBusy(_$loadingModal);
                        //    searchAgentList(page);
                        //});
                    }
                }
            );
        })
        //$('.editstate').on('click', function (e) {
        //});



        $('#setRatioModal').on('show.bs.modal', function (event) {
            var btnThis = $(event.relatedTarget); //触发事件的按钮
            var modal = $(this);  //当前模态框
            var agentId = $(btnThis).attr("data-agent-id");
            var agentType = $(btnThis).attr("data-agent-type");
            $("#setagentRatio").val(agentId);
            $("#agentType").val(agentType);
            $('#selectType').val(btnThis[0].dataset.agentRatio);
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var ratio = parseInt($('#selectType').val().replace('%', ''));
            if (ratio > 100) {
                alert("比例不能大于100");
                return;
            }
            if (ratio < 0) {
                alert("比例不能小于0");
                return;
            }
            var query = _$form.serializeFormToObject();
            var id = parseInt($(this).val());
            query.agentId = id;
            query.ratio = ratio;
            if (!_$form.valid()) {
                return;
            }

            var model = $('#modal-backdrop');
            $('#setRatioModal').modal('hide'); 
            var page = getCurrentPage();
            abp.ui.setBusy(_$loadingModal);
            var agentType = $("#agentType").val();
            if (agentType == "agent") { //代理
                $.ajax({
                    url: abp.appPath + 'ChineseBabies/Agent/UpdateAgentRatio',
                    type: 'POST',
                    data: query,
                    success: function (content) {
                        abp.ui.clearBusy(_$loadingModal);
                        if (content == null || content == '') {
                            searchAgentList(page);
                            alert('设置成功');
                        } else {
                            alert(content);
                        }
                    },
                    error: function (e) {
                        console.log(e);
                    }
                });
                //_agentService.updateAgentRatio(query).done(function (d) {
                //    searchAgentList(page);
                //    abp.ui.clearBusy(_$loadingModal);
                //}).always(function (re) {
                //    abp.ui.clearBusy(_$modal);
                //});
            } else if (agentType == "promoter") {//推广
                $.ajax({
                    url: abp.appPath + 'ChineseBabies/Agent/UpdatePromoterRatio',
                    type: 'POST',
                    data: query,
                    success: function (content) {
                        if (content == null || content == '') {
                            searchAgentList(page);
                            alert('设置成功');
                        } else {
                            alert(content);
                        }
                        abp.ui.clearBusy(_$loadingModal);
                    },
                    error: function (e) {
                        console.log(e);
                    }
                });
                //_agentService.updatePromoterRatio(query).done(function (d) {
                //    searchAgentList(page);
                //    abp.ui.clearBusy(_$loadingModal);
                //});
            }
        });

        //设置公司
        $('#setCompanyModal').on('show.bs.modal', function (event) {
            var btnThis = $(event.relatedTarget); //触发事件的按钮
            var agentId = $(btnThis).attr("data-agent-id");
            var query = _$form.serializeFormToObject();
            $('#agentId').val(agentId);
            //query.agentId = agentId;
            $.ajax({
                url: abp.appPath + 'ChineseBabies/Agent/GetCompanyList',
                type: 'GET',
                data: {},
                success: function (data) {
                    $('#companyList').empty();
                    $('#companyList').append("<option value='" + 0 + "'>" + '不设置公司' + "</option>");
                    for (var i = 0; i < data.result.length; i++) {
                        $('#companyList').append("<option value='" + data.result[i].id + "'>" + data.result[i].name +"</option>");
                    }
                    if (data.result.length == 0) {
                        $('#companyList').append("<option value=''>" + "请选择" + "</option>");
                    }
                    $('#companyList').selectpicker('refresh');
                },
                error: function (e) {
                    console.log(e);
                }
            });
        });

        $(document).on('click', '#submitSetCompany', function (e) {
            var agentId = $('#agentId').val();
            var currentCompanyId = $('#companyList').val();
            if (currentCompanyId == '' || currentCompanyId == null) {
                alert("请选择公司");
                return;
            }
            var query = _$companyForm.serializeFormToObject();
            query.agentId = agentId;
            query.companyId = currentCompanyId;
            $.ajax({
                url: abp.appPath + 'ChineseBabies/Agent/UpdateAgentCompany',
                type: 'GET',
                data: query,
                success: function (data) {
                    if (data == '' || data == null) {
                        alert('设置成功!');
                    }
                },
                error: function (e) {
                    console.log(e);
                }
            });
            searchAgentList(getCurrentPage());
            _$companymodal.modal('hide');
        })

        function getCurrentPage() {
            var page = parseInt($(".pagination .active span").text());
            if (page.isNaN) {
                page = 1;
            }

            return page;
        }

        //$('.applyPassed').on('click', function (e) {
        //    e.preventDefault();
        //    var id = $(this).attr("data-agent-id");
        //    var msg = '确定要审核通过该用户提现吗？';
        //    abp.message.confirm(
        //        msg, function (isConfirmed) {
        //            if (isConfirmed) {
        //                _agentService.apllyWithdrawMoney({ id: id }).done(function (d) {
        //                    searchAgentList();
        //                });
        //            }
        //        }
        //    );
        //});

        //$('.setWithdrawalRatio').on('click', function (e) {
        //    e.preventDefault();
        //    var id = $(this).attr("data-agent-id");
        //    var msg = '确定要审核通过该用户提现吗？';
        //    abp.message.confirm(
        //        msg, function (isConfirmed) {
        //            if (isConfirmed) {
        //                _agentService.apllyWithdrawMoney({ id: id }).done(function (d) {
        //                    refreshAgentList();
        //                });
        //            }
        //        }
        //    );
        //});


        $('#setSourceModal').on('show.bs.modal', function (event) {
            var btnThis = $(event.relatedTarget); //触发事件的按钮
            var modal = $(this);  //当前模态框
            var agentId = $(btnThis).attr("data-agent-id");
            $("#setSources").val(agentId);
        });
        _$sourcemodal.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var query = _$form.serializeFormToObject();
            var id = parseInt($(this).val());
            var sourceName = $('#SourceName').val();
            query.agentId = id;
            query.sourceName = sourceName;
            var model = $('#modal-backdrop');
            $('#setSourceModal').modal('hide');
            var page = getCurrentPage();
            abp.ui.setBusy(_$loadingModal);
            _agentService.upAgentSource(query).done(function (d) {
                searchAgentList(page);
                abp.ui.clearBusy(_$loadingModal);
            });
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