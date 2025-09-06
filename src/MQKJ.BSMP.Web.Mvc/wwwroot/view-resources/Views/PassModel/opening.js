

(function () {
    $(function () {

        var _openingService = abp.services.app.passModelStatistics;
        var _$modal = $('#searchform');
        var _$form = _$modal.find('form');
        var data = null;

           laydate.render({
            elem: '#startTime',
            format: 'yyyy-MM-dd',
            value: getDateDiff(30),
            type:'date',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });
        laydate.render({
            elem: '#endTime',
            max: getNowFormatDate(),
            value: new Date(),
            format: 'yyyy-MM-dd',
            type: 'date',
            theme: 'molv',
            done: function (value, date, endDate) {
            }
        });

        requestData();

        $('#timeselect').on('change', function (e) {
            handData(data);
        })

        function requestData() {
            var input = _$form.serializeFormToObject();
            _openingService.getOpeningData({ startTime: input.starttime, endTime: input.endtime }).done(function (result) {
                data = result;
                handData(data);
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        }

        function handData() {
            var x_datas = [];
            var y_datas = [];
            var selectType = parseInt($('#timeselect').val());
            if (selectType == 1) {//新增开局
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        y_datas.push(data[i].count);
                    } else {
                        var diff = data[i].count - data[i - 1].count;
                        if (diff >= 0) {
                            y_datas.push(diff);
                        } else {
                            y_datas.push(0);
                        }
                    }
                    x_datas.push(data[i].dateTime.substring(0, 10));
                }
            } else if (selectType == 2) {//累计闯关
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        y_datas.push(data[i].count);
                    } else {
                        y_datas.push(y_datas[i - 1] + data[i].count);
                    }
                    x_datas.push(data[i].dateTime.substring(0, 10));
                }
            }
            setChart(x_datas, y_datas);
        }

        $('#searchBtn').click(function () {
            requestData();
        })

        function setChart(x_datas,y_datas) {
            //绘制折线图
            var myChart = echarts.init(document.getElementById("openingChart"));
            var option = {
                backgroundColor: '#FBFBFB',
                tooltip: {
                    trigger: 'axis'
                },
                //legend: {
                //    data: ['开局统计']
                //},

                calculable: true,


                xAxis: [
                    {
                        axisLabel: {
                            rotate: 30,//x轴数据旋转
                            interval: 1
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#CECECE'
                            }
                        },
                        type: 'category',
                        boundaryGap: false,//从x轴的0开始
                        data: x_datas
                }
                ],
                yAxis: [
                    {
                        type: 'value',
                        axisLine: {
                            lineStyle: {
                                color: '#CECECE'
                            }
                        }
                    }
                ],
                series: [
                    {
                        name: '累计闯关',
                        type: 'line',
                        //symbol: 'none',//是否有小圆点
                        smooth: 0.2,
                        color: ['#66AEDE'],
                        data: y_datas
                    }
                ]
            };
            myChart.clear();
            myChart.setOption(option);
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