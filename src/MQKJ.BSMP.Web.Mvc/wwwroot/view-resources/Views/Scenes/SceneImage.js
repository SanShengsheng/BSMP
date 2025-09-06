(function () {
    $(function () {
        //localhost:21012/isceneservice
        var _sceneFileService = abp.services.app.sceneFile;
        var _$modal = $('#uploadSceneImageModal');
        var _$form = _$modal.find('form');
        //上传图片
        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var fileUpload = $("#scenefile").get(0);
            var files = fileUpload.files;
            var formData = new FormData();
            formData.append("fileinput", files[0]);
            formData.append("sceneId", getUrlParms("Id"));
            formData.append("scenefilename", $('#scenefilename').val())
            $.ajax({
                url: abp.appPath + 'Scenes/UploadImageFile',
                type: 'POST',
                contentType: false,
                data: formData,
                processData: false,
                //dataType: "json",
                mimeType: "multipart/form-data",
                success: function (content) {
                    refreshSceneFileList();
                },
                error: function (e) { console.log(e) }
            });
        });
        $('.delete-scene').click(function () {
            var sceneFileId = $(this).attr('data-sceneFile-id');
            deleteSceneFile(sceneFileId);
        });

        $('.default-sceneimage').click(function () {
            var sceneFileId = $(this).attr('data-scenefile-id');
            var sceneId = $(this).attr('data-sceneId');
            setdefaultimage(sceneId, sceneFileId);
        });
        $('.create-thumbnail-sceneimage').click(function () {
            var sceneFileId = $(this).attr('data-scenefile-id');
            createThumbnailImage(sceneFileId);
        });

        //创建缩略图
        function createThumbnailImage(id) {
            abp.message.confirm(
                "是否生成缩略图", function (isConfirmed) {
                    if (isConfirmed) {
                        _sceneFileService.createSceneImageThumbnail({ id: id }).done(function () {
                            abp.message.success('生成成功！','提示');
                            refreshSceneFileList();
                        });
                    }

                }
            );
        }

        function setdefaultimage(sceneid, scenefileId) {
            abp.message.confirm(
                "是否设为该图片为默认", function (isConfirmed) {
                    if (isConfirmed) {
                        _sceneFileService.updateSceneFile({ sceneId: sceneid ,sceneFileId: scenefileId }).done(function () {
                            refreshSceneFileList();
                        });
                    }

                }
            );
        }

        //删除场景图片
        function deleteSceneFile(id) {
            abp.message.confirm(
                "是否删除图片", function (isConfirmed) {
                    if (isConfirmed) {
                        _sceneFileService.deleteSceneFile({ id: id }).done(function () {
                            refreshSceneFileList();
                        });
                    }

                }
            );

        }
        //刷新
        function refreshSceneFileList() {
            location.reload(true); //reload page to see new user!
        }

        //获取地址栏参数，name:参数名称
        function getUrlParms(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = decodeURI(window.location.search).substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return null;
        }
    });
})(); 
