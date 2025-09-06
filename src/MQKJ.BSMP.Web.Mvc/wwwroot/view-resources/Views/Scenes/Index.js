(function () {
    $(function () {
        //localhost:21012/isceneservice
        var _sceneService = abp.services.app.scene;
        var _$modal = $('#SceneCreateModal');
        var _$form = _$modal.find('form');
        //��ӳ���
        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var sceneEditDto = _$form.serializeFormToObject();
            abp.ui.setBusy(_$modal);
            _sceneService.createOrUpdateScene({ sceneEditDto }).done(function () {
                _$modal.modal('hide');
                refreshSceneList();
            }).always(function () {
                abp.ui.clearBusy(_$modal);

            });
        });

        $('.delete-scene').click(function () {
            var sceneId = $(this).attr('data-scene-id');
            var sceneName = $(this).attr('data-scene-name');
            deleteScene(sceneId, sceneName);
        });

        //ɾ������
        function deleteScene(id, name) {
            abp.message.confirm(
                "�Ƿ�ɾ��������" + name + "", function (isConfirmed) {
                    if (isConfirmed) {
                        _sceneService.deleteScene({ id: id }).done(function () {
                            refreshSceneList();
                        });
                    }

                }
            );

        }

        //�༭������Ϣ
        $('.edit-scene').click(function (e) {
            $('.modal-title').html('�༭����');
            e.preventDefault();
            var sceneId = $(this).attr('data-scene-id');
            _sceneService.getSceneForEdit({ id: sceneId }).done(function (data) {
                $('input[name=Id]').val(data.sceneEditDto.id);
                $('input[name=SceneName]').val(data.sceneEditDto.sceneName).parent().addClass('focused');

            });
        });

        $('#SceneCreateModal').on('hide.bs.modal', function () {
            _$form[0].reset();
        });
        $('#SceneEditModal').on('hide.bs.modal', function () {
            _$form[0].reset();
        });

        $('#RefreshButton').click(function () {
            refreshSceneList();
        });

        //ˢ��
        function refreshSceneList() {
            location.reload(true); //reload page to see new user!
        }
    });
})();
