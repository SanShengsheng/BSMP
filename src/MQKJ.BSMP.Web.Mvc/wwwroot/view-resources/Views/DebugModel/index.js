(function () {
    $(function () {
        var _friendService = abp.services.app.friend;
        var _$modal = $('#searchform');
        var _$form = _$modal.find('form');
        var friendId = null;

        searchData();

        //修改按钮点击
        $('body').delegate('.edit-floor','click',function (e) {
            var floor = $(this).attr("data-floor");
            $('#floorInput').val(floor);
            friendId = $(this).attr("data-friend-id");
        })

        $('#modifyFloorBtn').click(function () {
            var floor = $('#floorInput').val();
            _friendService.updateFloor({ id: friendId, floor: floor }).done(function () {

                location.reload(true);
            })
        })

        //查询
        $("#SearchBtn").click(function () {
            var query = _$form.serializeFormToObject();
            if (query.inviterName == "") {
                alert("请输入邀请方昵称");
                return;
            }
            searchData(query.inviterName, query.inviteeName);
        });

        function searchData(inviterName,inviteeName) {
            abp.ui.setBusy(_$modal);
            _friendService.getAllFriendList({ inviterName: inviterName, inviteeName: inviteeName }).done(function (data) {
                _$modal.modal('hide');
                console.log(data);
                var contentHtml = "";
                for (var i = 0; i < data.items.length; i++) {
                    contentHtml += '<tr>' +
                        '<td>' + data.items[i].inviterName + '</td>' +
                        '<td>' + data.items[i].inviteeName + '</td>' +
                        '<td>' + data.items[i].floor + '</td>' +
                        '<td><a href="#" class="waves-effect waves-block edit-floor" data-floor=' + data.items[i].floor + ' data-friend-id=' + data.items[i].id + ' data-toggle="modal" data-target="#floorEditModal"><i class="material-icons"></i>修改关卡</a></td>' +
                        '</tr>';
                }
                $('#tbody_data').html(contentHtml);
                abp.ui.clearBusy(_$modal);
            })
        }
    })
})();