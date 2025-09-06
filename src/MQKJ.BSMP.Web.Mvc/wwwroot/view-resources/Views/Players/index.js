

//import { Date } from "core-js";

(function () {
    $(function () {
        var _playerService = abp.services.app.player;
        var _$modal = $('#QuestionFilterDiv');
        var _$form = _$modal.find('form');

        $("#SearchBtn").click(function () { searchPlayerList(); });

        $('.editstate').on('click', function (e) {
            e.preventDefault();
            var id = $(this).attr("data-tag-id");
            var msg = '';
            if ($(this).innerText == '冻结') {
                msg = '是否冻结该用户';
            } else {
                msg = '是否解冻该用户';
            }
            abp.message.confirm(
                msg, function (isConfirmed) {
                    if (isConfirmed) {
                        _playerService.updatePlayerState({ id: id }).done(function (d) {
                            refreshPlayerList();
                        });
                    }

                }
            );
        });

        laydate.render({
            elem: '#starttime',
            format: 'yyyy-MM-dd HH:mm:ss',
            max: getNowFormatDate(),
            //value: '2018-05-09 00:00:00',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });
        laydate.render({
            elem: '#endtime',
            max: getNowFormatDate(),
            //value: new Date(),
            format: 'yyyy-MM-dd HH:mm:ss',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });

        $('#RefreshButton').click(function () {
            refreshPlayerList();
        });

        //刷新
        function refreshPlayerList() {
            location.reload(true); //reload page to see new user!
        }

        //查询
        function searchPlayerList() {
            var query = _$form.serializeFormToObject();
            query.gender = $('#gender').val();
            query.ageRange = $('#ageRange').val();
            $.post(abp.appPath + 'Players', query, function (content) {
                $('#playerList').html(content);
            },"html"); 
        }

        //格式化日期时间
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
            var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
                + " " + date.getHours() + seperator2 + date.getMinutes()
                + seperator2 + date.getSeconds();
            return currentdate;
        }
        
    });

})();