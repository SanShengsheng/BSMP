

(function () {
    $(function () {

        var _openingService = abp.services.app.passModelStatistics;
        var _$modal = $('#searchform');
        var _$form = _$modal.find('form');
        var data = null;
        var pageSize = 5;

        laydate.render({
            elem: '#startTime',
            format: 'yyyy-MM-dd',
            value: getDateDiff(30),
            type: 'date',
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
            _openingService.getAnswerQuestionData({ startTime: input.starttime, endTime: input.endtime }).done(function (result) {
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
                        y_datas.push(data[i].totalCount);
                    } else {
                        var diff = data[i].totalCount - data[i - 1].totalCount;
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
                        y_datas.push(data[i].totalCount);
                    } else {
                        y_datas.push(y_datas[i - 1] + data[i].totalCount);
                    }
                    x_datas.push(data[i].dateTime.substring(0, 10));
                }
            }
            setChart(x_datas, y_datas);

            setTaleData(data);
        }

        $('#searchBtn').click(function () {
            requestData();
        })

        function setTaleData(data) {
            $('#pageLimit').bootstrapPaginator({
                currentPage: 1,//当前的请求页面。
                totalPages: data.length / 5,//一共多少页。
                size: "normal",//应该是页眉的大小。
                bootstrapMajorVersion: 3,//bootstrap的版本要求。
                alignment: "right",
                numberOfPages: pageSize,//一页列出多少数据。
                itemTexts: function (type, page, current) {//如下的代码是将页眉显示的中文显示我们自定义的中文。
                    switch (type) {
                        case "first": return "首页";
                        case "prev": return "上一页";
                        case "next": return "下一页";
                        case "last": return "末页";
                        case "page": return page;
                    }
                },
                onPageClicked: function (event, originalEvent, type, page) {//给每个页眉绑定一个事件，其实就是ajax请求，其中page变量为当前点击的页上的数字。
                    var htmlStr = "";
                    var pageDatas = data.slice(page * pageSize, pageSize + page * pageSize);
                    for (var i = 0; i < pageDatas.length; i++) {
                        htmlStr += '<tr>' +
                            '<td>' + pageDatas[i].dateTime.substring(0, 10) + '</td>' +
                            '<td>' + pageDatas[i].totalCount + '</td>' +
                            '<td>' + pageDatas[i].rightCount + '</td>' +
                            '<td>' + pageDatas[i].realCount + '</td>' +
                            '<td>' + pageDatas[i].cheatCount + '</td>' +
                            '</tr>';
                    }
                    $('#answerquestions').html(htmlStr);
                }
            });


            //默认显示第一页数据
            var htmlStr = "";
            var defaultPages = data.slice(0, 5);
            for (var i = 0; i < defaultPages.length; i++) {
                htmlStr += '<tr>' +
                                '<td>' + defaultPages[i].dateTime.substring(0,10) + '</td>' +
                                '<td>' + defaultPages[i].totalCount + '</td>' +
                                '<td>' + defaultPages[i].rightCount + '</td>' +
                                '<td>' + defaultPages[i].realCount + '</td>' +
                                '<td>' + defaultPages[i].cheatCount + '</td>' +
                            '</tr>';
            }
            $('#answerquestions').html(htmlStr);
        }

        function setChart(x_datas, y_datas) {
            //绘制折线图
            var myChart = echarts.init(document.getElementById("answerQuestionChart"));
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