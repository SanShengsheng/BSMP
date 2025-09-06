(function ($) {
    var _adService = abp.services.app.adviertisement;
    var _$modal = $('#UserEditModal');
    var _$form = $('form[name=AdEditForm]');
    laydate.render({
        elem: '#edit_endTime',
        //max: getNowFormatDate(),
        //value: getNowFormatDate(),
        format: 'yyyy-MM-dd',
        theme: 'molv',
        done: function (value, date, endDate) {
        }
    });

    function save() {
        if (!_$form.valid()) {
            return;
        }
        var ad = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        abp.ui.setBusy(_$form);
        _adService.createOrUpdateAdviertisement({ adviertisementDto: ad}).done(function () {
            _$modal.modal('hide');
            $('#UserEditModal').hide();
            location.reload(true); //reload page to see edited user!
        }).always(function () {
            abp.ui.clearBusy(_$modal);
            _$modal.modal('hide');
            $('#UserEditModal').css("display","none");
        });
    }


    //Handle save button click
    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);