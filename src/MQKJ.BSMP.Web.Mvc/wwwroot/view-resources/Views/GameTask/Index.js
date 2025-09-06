(function(){
    $(function(){
        var _gameTaskService = abp.services.app.gameTasks; 
        var _$modal = $('#searchform');
        var _$form = _$modal.find('form'); 
        
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

        $('#SearchBtn').on('click',function(){searchGameTaskList()});
        
        function searchGameTaskList(){
             //拼接参数
            var query = _$form.serializeFormToObject();
            query.taskType = $('#taskType').val();
            query.taskState = $('#taskState').val();
            query.seekType = $('#seekType').val();
            query.relationType = $('#relationType').val();
            $.post(abp.appPath + 'GameTasks', query, function (content) {
                $('#taskList').html(content);
            }, "html");
        }

         $('#RefreshButton').click(function () {
            refreshSceneList();
        });

        //刷新
        function refreshSceneList() {
            location.reload(true); //reload page to see new user!
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
    })

})();