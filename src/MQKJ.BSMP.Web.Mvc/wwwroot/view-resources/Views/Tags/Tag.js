(function () {
	$(function () {

		var _tagService = abp.services.app.tag;
		var _$modal = $('#tagCreateModal');
        var _$form = _$modal.find('form');
        console.log('12223', _$modal);
        _$form.find('#submitbtn').click(function (e) {
			e.preventDefault();
            console.log('submit');
			if (!_$form.valid()) {
				return;
            }
            console.log('submit');
			var tag = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
			
			abp.ui.setBusy(_$modal);
			_tagService.createOrUpdateTag(tag).done(function () {
				_$modal.modal('hide');
				location.reload(true); //reload page to see new role!
			}).always(function () {
				abp.ui.clearBusy(_$modal);
                });

            return false;
		});

        $('.edit-tag').click(function (e) {
			var tagId = $(this).attr("data-tag-id");

			e.preventDefault();
			$.ajax({
				url: abp.appPath + 'tag/EditModal?tagId=' + tagId,
				type: 'POST',
				contentType: 'application/html',
				success: function (content) {
					$('#tagEditModal div.modal-content').html(content);
				},
				error: function (e) { }
			});
		});
        $('.delete-tag').click(function () {
			var tagId = $(this).attr("data-tag-id");
			var tagname = $(this).attr('data-tag-name');

			deleteRole(tagId, tagname);
		});
        function deleteRole(tagId, tagname) {
			abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'BSMP'), tagname),
				function (isConfirmed) {
					if (isConfirmed) {
						_tagService.deleteTag({
							id: tagId
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