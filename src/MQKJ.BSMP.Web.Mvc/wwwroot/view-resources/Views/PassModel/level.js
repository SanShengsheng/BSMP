

(function () {
    $(function () {
        var _$modal = $('#searchform');
        var _$form = _$modal.find('form');

           laydate.render({
            elem: '#dateTime',
            format: 'yyyy-MM-dd',
            value: new Date(),
            type:'date',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });

        function searchData() {
            //拼接参数
            var input = _$form.serializeFormToObject();
            $.post(abp.appPath + 'PassModel/LevelStatistics', { dateTime:input.dateTime}, function (content) {
                $('#dataList').html(content);
            }, "html");
        }

        $('#searchBtn').click(function () {
            searchData();
        })
    });
})();