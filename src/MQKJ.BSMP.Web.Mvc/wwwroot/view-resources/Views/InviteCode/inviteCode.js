
(function () {
    $(function () {

        var _inviteCodeService = abp.services.app.agentInviteCode;
        var _$modal = $('#agentCreateModal');
        var _$form = _$modal.find('form');

        ajaxPage();

        $('#searchBtn').click(function () {
            searchInviteCodeList();
        })
        $('#add-invitecode').click(function(){
            addInviteCode();
        })

        function addInviteCode() {
            var page = getCurrentPage();
            var inviteCodeType = parseInt($('#selectType').val());
            $.ajax({
                url: abp.appPath + 'ChineseBabies/AgentInviteCode/AddInviteCode',
                type: 'POST',
                data: { inviteCodeType: inviteCodeType },
                success: function (content) {
                    _$modal.modal('hide');
                    abp.ui.clearBusy(_$modal);
                    if (content == "true") {
                        searchInviteCodeList(page);
                        alert('操作成功');
                    } else {
                        alert(content);
                    }
                },
                error: function (e) {
                    console.log(e);
                    if (e.status == 401) {
                        _$modal.modal('hide');
                        abp.ui.clearBusy(_$modal);
                        alert("您没有权限操作该功能，请联系管理员");
                    }
                    //abp.ui.clearBusy(_$modal);
                }
            });
            //_inviteCodeService.createOrUpdate({ agentInviteCode }).done(function () {
            //    _$modal.modal('hide');
            //    refreshCodeList();
            //}).always(function () {
            //    abp.ui.clearBusy(_$modal);
            //});
        }

        //分页
        function ajaxPage() {
            $(".pagination a").each(function () {
                $(this).attr("href", "javascript:void(0)");
            });
            $(".pagination a").click(function (obj) {
                var page = $(this).text();
                if (page.indexOf('首页') != -1) {
                    page = 1;
                } else if (page.indexOf('末页') != -1) {
                    var startIndex = $('.PagedList-pageCountAndLocation a').text().lastIndexOf('共');
                    page = $('.PagedList-pageCountAndLocation a').text().substring(startIndex + 1).replace(/[^0-9]/ig, "");
                }
                searchInviteCodeList(page);
            });
        }

        //查询
        function searchInviteCodeList(page) {
            var query = _$form.serializeFormToObject();
            var state = parseInt($('#inviteCodeState').val());
            var type = parseInt($('#inviteCodeType').val());
            $.post(abp.appPath + 'ChineseBabies/AgentInviteCode', { page: page, agentCategory: type, inviteCodeState: state }, function (content) {
                $('#dataList').html(content);
                ajaxPage();
            },"html"); 
        }

        function getCurrentPage() {
            var page = parseInt($(".pagination .active span").text());
            if (page.isNaN) {
                page = 1;
            }

            return page;
        }

        //刷新
        function refreshCodeList() {
            //location.reload(true); //reload page to see new user!
            ajaxPage();
        }
    });
})();