
(function(){
    $(function () {
        var _questionService = abp.services.app.question;
        var _$modal = $('#searchform');
        var _$form = _$modal.find('form'); 
        
        //laydate.render({
        //    elem: '#startTime',
        //    format: 'yyyy-MM-dd',
        //    value: getDateDiff(30),
        //    type:'date',
        //    theme: 'molv',
        //    done: function (value, date, endDate) {
        //    }
        //});
        //laydate.render({
        //    elem: '#endTime',
        //    max: getNowFormatDate(),
        //    value: new Date(),
        //    format: 'yyyy-MM-dd',
        //    type: 'date',
        //    theme: 'molv',
        //    done: function (value, date, endDate) {
        //    }
        //});


        //$('#timeselect').on('change', function (e) {
        //    var selectValue = $('#timeselect').val();
        //    switch (selectValue) {
        //        case '1'://最近一周
        //            getNowFormatDate();
        //            $('#startTime').val(getDateDiff(7));
        //            break;
        //        case '2'://最近一月
        //            $('#startTime').val(getDateDiff(30));
        //            break;
        //        case '3'://最近一年
        //            $('#startTime').val(getDateDiff(365));
        //            break;
        //        case '4'://全部
        //            $('#startTime').val(getDateDiff(365));
        //            break;
        //        default:
        //            break;
        //    }
        //})

        requestData();

        //$('#SearchBtn').click(function () {
        //    var startTime = $('#startTime').val();
        //    var endTime = $('#endTime').val();
        //    requestData();
        //})

         $('#RefreshButton').click(function () {
            refreshSceneList();
        });

        function requestData() {
            //var questionStatisicsDto = _$form.serializeFormToObject();
            //var startTime = questionStatisicsDto.starttime += " 00:00:00";
            //var endTime = questionStatisicsDto.endtime += " 00:00:00";
            abp.ui.setBusy(_$modal);
            //_questionService.getQuestionStatisics({ startTime: startTime, endTime: endTime }).done(function (data) {
            _questionService.getQuestionStatisics().done(function (data) {
                _$modal.modal('hide');
                console.log('统计数据：', data);
                BindData(data);

            }).always(function () {
                abp.ui.clearBusy(_$modal);
                });
        }

        function BindData(data) {
            $('#questionCount').text(data.questionCount);
            $('#onLineQuestions').text(data.onlineQuestionCount);
            $('#offLineQuestions').text(data.offlineQuesitonCount);
            //性别占比设置
            var genderStatistics = {};
            genderStatistics.title = "问题的性别对比";
            genderStatistics.categorys = ["男生", "女生"];
            genderStatistics.data = [{ value: data.maleQuestionCount, name: "男生" }, { value: data.femaleQuestionCount, name: "女生" }];
            setChart("genderChart", genderStatistics);

            //问题关系对比
            var relationStatistics = setData("问题适用关系类别对比", data.questionRelationDegreeList);
            setChart("relationChart", relationStatistics);

            //问题私密度对比
            var privacyStatistics = setData("问题私密度对比", data.questionPrivacieList);
            setChart("privacyChart", privacyStatistics);

            //问题场景占比
            var htmlStr = "";
            for (var i = 0; i < data.questionScenes.length; i++) {
                htmlStr +=  '<div class="progress">' +
                    '<div class="progress-bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width:' + data.questionScenes[i].questionScenePercent + "%" +';">' +
                                '</div>' +
                    '<span style="class="sr">' + data.questionScenes[i].sceneName + "占比:" + data.questionScenes[i].questionScenePercent + "%" + '</span>' +
                            '</div>'
            }
            $('#sceneDatas').html(htmlStr);
        }

        function setData(title,datas) {
            var statistics = {};
            statistics.title = title;
            statistics.categorys = [];
            statistics.data = [];
            for (var i = 0; i < datas.length; i++) {

                statistics.categorys.push(datas[i].questionTagName);
                statistics.data.push({ value: datas[i].questionTagCount, name: datas[i].questionTagName});
            }

            return statistics;
        }

        // 图标配置
        function setChart(id,obj) {
            var myechart = echarts.init(document.getElementById(id));
            var options = {
                title: {
                    text: obj.title,
                    //subtext: '',
                    left: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                legend: {
                    bottom: 10,
                    left: 'center',
                    data: obj.categorys
                },
                series: [
                    {
                        type: 'pie',
                        radius: '65%',
                        center: ['50%', '50%'],
                        selectedMode: 'single',
                        data: obj.data,
                        itemStyle: {
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            }
            myechart.setOption(options);
        }

        //刷新
        function refreshSceneList() {
            location.reload(true); //reload page to see new user!
        }
    
        //格式化日期时间
        function getNowFormatDateTime() {
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
    })

})();