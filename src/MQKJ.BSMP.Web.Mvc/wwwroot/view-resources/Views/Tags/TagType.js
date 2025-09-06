(function () {
	$(function () {

		var _tagTypeService = abp.services.app.tagType;
		var _$modal = $('#TagTypeCreateModal');
        var _$form = _$modal.find('form');
        console.log('12223', _$modal);
        _$form.find('#submitbtn').click(function (e) {
			e.preventDefault();
            console.log('submit');
			if (!_$form.valid()) {
				return;
            }
            console.log('submit');
			var tagType = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
			
			abp.ui.setBusy(_$modal);
			_tagTypeService.createOrUpdateTagType(tagType).done(function () {
				_$modal.modal('hide');
				location.reload(true); //reload page to see new role!
			}).always(function () {
				abp.ui.clearBusy(_$modal);
                });

            return false;
		});

        $('.edit-tagtype').click(function (e) {
			var tagTypeId = $(this).attr("data-tagtype-id");

			e.preventDefault();
			$.ajax({
				url: abp.appPath + 'tagtype/EditModal?tagTypeId=' + tagTypeId,
				type: 'POST',
				contentType: 'application/html',
				success: function (content) {
					$('#TagTypeEditModal div.modal-content').html(content);
				},
				error: function (e) { }
			});
		});
        $('.delete-tagtype').click(function () {
			var tagtypeId = $(this).attr("data-tagtype-id");
			var tagtypename = $(this).attr('data-tagtype-name');

			deleteRole(tagtypeId, tagtypename);
		});
        function deleteRole(tagtypeId, tagtypename) {
			abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'BSMP'), tagtypename),
				function (isConfirmed) {
					if (isConfirmed) {
						_tagTypeService.deleteTagType({
							id: tagtypeId
						}).done(function () {
                            location.reload(true);
						});
					}
				}
			);
		}
	});
})();

console.log('123');