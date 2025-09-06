
(function() {
    $(function() {

        var _userService = abp.services.app.user;
        var _$modal = $('#UserCreateModal');
        var _$form = _$modal.find('form');


        var _$passwordModal = $('#ModifyPasswordModal');
        var _$passwordForm = _$modal.find('form');

        _$form.validate({
            rules: {
                Password: "required",
                ConfirmPassword: {
                    equalTo: "#Password"
                }
            }
        });

        _$passwordForm.validate({
            rules: {
                OldPassword: "required",
                ModifyConfirmPassword: {
                    equalTo: "#NewPassword"
                }
            }
        });

        $('#RefreshButton').click(function () {
            refreshUserList();
        });

        $('.delete-user').click(function () {
            var userId = $(this).attr("data-user-id");
            var userName = $(this).attr('data-user-name');

            deleteUser(userId, userName);
        });

        $('.edit-user').click(function (e) {
            var userId = $(this).attr("data-user-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Users/EditUserModal?userId=' + userId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#UserEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        $('.modify-password').click(function (e) {
            var userId = $(this).attr("data-user-id");
            $('#modifyPasswordBtn').val(userId);
        });

        $('#modifyPasswordBtn').click(function (e) {
            var userId = $('#modifyPasswordBtn').val();
            var oldPassword = $('#OldPassword').val();
            var newPassword = $('#NewPassword').val();
            var confirmPassword = $('#ModifyConfirmPassword').val();
            e.preventDefault();
            if (!_$passwordForm.valid()) {
                return;
            }
            if (newPassword != confirmPassword) {
                alert('两次密码输入不一致');
                return;
            }
            abp.ui.setBusy(_$passwordModal);
            $.ajax({
                url: abp.appPath + 'Users/ModifyPassword?userId=' + userId + '&oldPassword=' + oldPassword + '&newPassword=' + newPassword,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    abp.ui.clearBusy(_$passwordModal);
                    if (content == null || content == '') {
                        _$passwordModal.modal('hide')
                        alert('修改成功');
                        //window.location.href = '/Account/Logout';
                    } else {
                        alert(content);
                    }
                    console.log(content);
                },
                error: function (e) {
                    abp.ui.clearBusy(_$passwordModal);
                    alert("修改失败");
                }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var user = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            user.roleNames = [];
            var _$roleCheckboxes = $("input[name='role']:checked");
            if (_$roleCheckboxes) {
                for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                    var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                    user.roleNames.push(_$roleCheckbox.val());
                }
            }

            abp.ui.setBusy(_$modal);
            _userService.create(user).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new user!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });
        
        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshUserList() {
            location.reload(true); //reload page to see new user!
        }

        function deleteUser(userId, userName) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'BSMP'), userName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _userService.delete({
                            id: userId
                        }).done(function () {
                            refreshUserList();
                        });
                    }
                }
            );
        }
    });
})();