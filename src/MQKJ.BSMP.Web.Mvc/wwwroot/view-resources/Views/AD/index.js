(function () {

    $(function () {
        var _adService = abp.services.app.adviertisement;
        var _$loadingModal = $('#loadingModal');
        var _$addADModal = $('#AddADModal');
        var $addadForm = _$addADModal.find("form");
        ajaxPage();

        laydate.render({
            elem: '#endTime',
            //max: getNowFormatDate(),
            value: getNowFormatDate(),
            format: 'yyyy-MM-dd',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });

        function searchADList(page) {
            var name = $('#adName').val();
            var state = parseInt($('#adState').val());
            var platform = parseInt($('#adPlatform').val());
            var tenantId = parseInt($('#softwarePlatform').val());
            abp.ui.setBusy(_$loadingModal);
            //获取列表
            $.post(abp.appPath + 'Adviertisements/Adviertisement', { page: page, name: name, platform: platform, state: state, teantId: tenantId }, function (content) {
                $('#dataList').html(content);
                ajaxPage();
                abp.ui.clearBusy(_$loadingModal);
            }, "html");
        }

        $('#searchBtn').click(function () {
            searchADList();
        })


        function getCurrentPage() {
            var page = parseInt($(".pagination .active span").text());
            if (page.isNaN) {
                page = 1;
            }

            return page;
        }

        $('#AddADModal').on('show.bs.modal', function (event) {
            _$addADModal.find('input').each(function () {
                $(this).val('');
            })
        });

        //分页
        function ajaxPage() {
            var startIndex = $('.PagedList-pageCountAndLocation a').text().lastIndexOf('共');
            var totalPage = $('.PagedList-pageCountAndLocation a').text().substring(startIndex + 1).replace(/[^0-9]/ig, "");
            $(".pagination a").each(function () {
                $(this).attr("href", "javascript:void(0)");
            });
            $(".pagination a").click(function (obj) {
                var page = $(this).text();
                if (page.indexOf('首页') != -1) {
                    page = 1;
                } else if (page.indexOf('末页') != -1) {
                    page = totalPage;
                } else if (isNaN(parseInt(page))) {
                    return;
                }
                if (totalPage == '1')
                    return;
                searchADList(page);
            });

        }

        $(document).on('click', '.RefreshButton', function (e) {
            e.preventDefault();
            refreshAdList();
        });

        function refreshAdList() {
            ajaxPage();
        }

        $(document).on('click', '#saveBtn', function (e) {
            abp.ui.setBusy(_$loadingModal);
            var query = $addadForm.serializeFormToObject();
            if (query.Name == null || query.Name == '') {
                alert('名称不能为空');
                return;
            }
            _adService.createOrUpdateAdviertisement({ adviertisementDto: query }).done(function (d) {
                $('#AddADModal').modal('hide');
                searchADList(getCurrentPage());
            }).always(function () {
                abp.ui.clearBusy(_$loadingModal);
                _$addADModal.modal("hide");
            });
        });

        $('.edit-ad').click(function (e) {
            var adId = $(this).attr("data-ad-id");
            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Adviertisements/Adviertisement/EditAd?adId=' + adId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#AdEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        $(document).on('click', '.delete-ad',function (e) {
            var adId = $(this).attr("data-ad-id");
            var name = $(this).attr("data-ad-name");
            e.preventDefault();
            deleteAd(adId, name);
        });

        function deleteAd(adId, name) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'BSMP'), name),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _adService.deleteAd({
                            id: adId
                        }).done(function () {
                            searchADList(getCurrentPage());
                        });
                    }
                }
            );
        }

        function getNowFormatDate() {
            var date = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate;
            return currentdate;
        }
    });
})();