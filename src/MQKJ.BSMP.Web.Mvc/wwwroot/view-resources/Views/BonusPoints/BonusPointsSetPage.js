
(function(){
    $(function(){
        var _bonusPointsService = abp.services.app.bonusPoint;
        var _$modal = $('#BonusPointEditModal');
        var _$form = _$modal.find('form');

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var bonusPoint = _$form.serializeFormToObject();
            abp.ui.setBusy(_$modal);
            _bonusPointsService.createOrUpdateBonusPoint({bonusPoint}).done(function(){
                _$modal.modal('hide');
                location.reload();
            }).always(function(){
                abp.ui.clearBusy(_$modal);
            })
        });

        //编辑
        $('.edit-bonuspoint').click(function (e) {
            $('.modal-title').html('编辑积分');
            e.preventDefault();
            var id = $(this).attr('data-bonusPoint-id');
            _bonusPointsService.getBonusPointById({ id: id }).done(function (data) {
                $('input[name=Id]').val(data.id);
                $('input[name=pointsCount]').val(data.pointsCount).parent().addClass('focused');

            });
        });

        $('#BonusPointEditModal').on('hide.bs.modal', function () {
            _$form[0].reset();
        });

        $('#RefreshButton').click(function () {
            refreshSceneList();
        });

        //刷新
        function refreshSceneList() {
            location.reload(true); //reload page to see new user!
        }
    });

})();