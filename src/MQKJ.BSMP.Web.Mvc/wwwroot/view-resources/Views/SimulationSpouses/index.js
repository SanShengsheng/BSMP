

(function () {
    $(function () {

        var _wecharWebUserService = abp.services.app.weChatWebUser;
        var _$modal = $('#searchform');
        var _$setStatemodal = $('#wechatAccountModal');
        var _$loadingModal = $('#loadingModal');
        var _$form = _$modal.find('form');

        ajaxPage();

        $('#searchBtn').click(function () {
            var page = getCurrentPage();
            searchUserList(page);
        })

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
                searchUserList(page);
            });
        }

        //查询
        function searchUserList(page) {
            var nickName = $('#nickName').val();
            $.post(abp.appPath + 'Activities/SimulationSpouse', { page: page, nickName:nickName }, function (content) {
                $('#dataList').html(content);
                ajaxPage();
            },"html"); 
        }

        $('#wechatAccountModal').on('show.bs.modal', function (event) {
            var btnThis = $(event.relatedTarget); //触发事件的按钮
            var modal = $(this);  //当前模态框
            var userId = $(btnThis).attr("data-user-id");
            $("#user-Id").val(userId);
            var option = $(btnThis).attr("data-option");
            $('#optionType').val(option);
            var wechatAccount = $(btnThis).attr('data-wechatAccount');
            $('#wechatAccount').val(wechatAccount);
            if (option == 'signUp') {
                $('#labelTitle').text('请输入用户微信号');
            } else {
                $('#labelTitle').text('请输入匹配方微信号');
            }
        });

        _$setStatemodal.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var query = _$form.serializeFormToObject();
            var id = $("#user-Id").val();
            var wechatAccount = $('#wechatAccount').val();
            var model = $('#modal-backdrop');
            $('#wechatAccountModal').modal('hide');
            var page = getCurrentPage();
            abp.ui.setBusy(_$loadingModal);
            var option = $('#optionType').val();
            var actionName = '';
            if (option == 'signUp') {
                actionName = 'SignUp';
            } else if (option == 'match') {
                actionName = 'Match';
            } else {
                alert('无效操作');
                return;
            }
            $.post(abp.appPath + 'Activities/SimulationSpouse/' + actionName, { id: id, wechatAccount: wechatAccount, }, function (content) {
                abp.ui.clearBusy(_$loadingModal);
                var page = getCurrentPage();
                searchUserList(page);
                if (content != '') {
                    alert(content);
                } else {
                    alert('更新成功');
                }
            });
        });

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