

(function () {
	$(function () {

		var _loveCardService = abp.services.app.loveCard;
        var _$modal = $('#loveCardCreateModal');
        var _$form = _$modal.find('form');

        laydate.render({
            elem: '#startTime',
            format: 'yyyy-MM-dd',
            value: getDateDiff(30),
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });
        laydate.render({
            elem: '#endTime',
            max: getNowFormatDate(),
            value: getNowFormatDate(),
            format: 'yyyy-MM-dd',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });

        $('#searchBtn').click(function () {
            var query = _$form.serializeFormToObject();
            //获取列表
            $.post(abp.appPath + 'CardManage', query, function (content) {
                $('#dataList').html(content);
            }, "html"); 
        })

        $('.editstate').on('click', function (e) {
            e.preventDefault();
            var id = $(this).attr("data-tag-id");
            var msg = '确定要审核通过吗？';
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        _loveCardService.updateLoveCardState({ id: id }).done(function (d) {
                            refreshLoveCardList();
                        });
                    }
                }
            );
        });

        function refreshLoveCardList() {
            location.reload(true);
        }

        //格式化日期
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

        //获取与当前日期相差天数的日期
        function getDateDiff(number) {
            var nowDate = new Date();
            var weekDate = new Date(nowDate - number * 24 * 3600 * 1000);
            var seperator1 = "-";
            var seperator2 = ":";
            var month = weekDate.getMonth() + 1;
            var strDate = weekDate.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = weekDate.getFullYear() + seperator1 + month + seperator1 + strDate;

            return currentdate;
        }

	});
})();