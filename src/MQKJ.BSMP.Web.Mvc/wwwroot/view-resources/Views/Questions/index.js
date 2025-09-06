(function () {
    $(function () {
        var _questionService = abp.services.app.question;
        var _tagService = abp.services.app.tag;
        var _$modal = $('#QuestionCreateModal');
        var _$form = _$modal.find('form');
        var _$filterModal = $("#QuestionFilterDiv");
        var _$searchForm = _$filterModal.find('form');
        var matchs = null;
        // var backgroundStory =null ;//IniWangEditor();
        IniTinyMCE('textarea.background-story');
        ajaxPage();
        _$form.validate({
            rules: {
                SceneId: "required",
                //ConfirmPassword: {
                //    equalTo: "#Password"
                //}
            }
        });


        $('#RefreshButton').click(function () {
            refreshQuestionList();
        });

        $("#SearchButton").click(function () { searchQuestionList(); });

        //上线
        $(document).on("click", ".online-question", function () {
            var questionId = $(this).attr('data-question-id');
            var questionName = $(this).attr('data-question-name');
            onlineQuestion(questionId, questionName);
        })

        //下线
        $(document).on("click", '.freeze-question', function () {
            var questionId = $(this).attr("data-question-id");
            var questionName = $(this).attr('data-question-name');

            freezeQuestion(questionId, questionName);
        });

        $(document).on("click", '.pass-question', function () {
            var questionId = $(this).attr("data-question-id");
            var questionName = $(this).attr('data-question-name');

            passQuestion(questionId, questionName);
        });

        //场景&场景图片start--
        _$form.closest('div.modal-content').find(".carousel-inner a[data-scene-img-id!='']").click(function () {
            clickSceneImg(this);
        });

        _$form.closest('div.modal-content').find(":input[name='SceneId']").on('changed.bs.select', function (e) {
            changeScene(this);
        });
        //场景&场景图片end--

        $(document).on("click", ".edit-question", function (e) {
            var questionId = $(this).attr("data-question-id");
            var editBackgrounStroyId = '#QuestionEditModal textarea.background-story';
            tinymce.remove(editBackgrounStroyId);
            $('#QuestionEditModal div.modal-content').html("");
            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Questions/EditQuestionModal?questionId=' + questionId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#QuestionEditModal div.modal-content').html(content);
                    IniTinyMCE(editBackgrounStroyId);

                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var question = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            //女生画面-背景故事
            var backgroundStoryFemale = tinyMCE.get('BackgroundStoryFemale-add').getContent().replace("padding-left", "text-indent"); //backgroundStory.female.txt.html();
            question.BackgroundStoryFemale = backgroundStoryFemale;
            //女性选项
            question.Answers = [];
            var _$femaleAnswers = _$form.closest('div.modal-content').find("input[answer-name='AnswerFemale'][value!='']");
            if (_$femaleAnswers) {
                for (var roleIndex = 0; roleIndex < _$femaleAnswers.length; roleIndex++) {
                    var _$answer = { Title: $(_$femaleAnswers[roleIndex]).val(), Sort: roleIndex, QuestionType: 1 }
                    if (_$answer.Title !== "") {
                        question.Answers.push(_$answer);
                    }
                }
            }
            //男生画面-背景故事
            var backgroundStoryMale = tinyMCE.get('BackgroundStoryMale-add').getContent().replace("padding-left", "text-indent"); // backgroundStory.male.txt.html();
            question.BackgroundStoryMale = backgroundStoryMale;
            // debugger
            //男性选项
            var _$maleAnswers = _$form.closest('div.modal-content').find("input[answer-name='AnswerMale']");
            if (_$maleAnswers) {
                for (var maleIndex = 0; maleIndex < _$maleAnswers.length; maleIndex++) {
                    var _$maleAnswer = { Title: $(_$maleAnswers[maleIndex]).val(), sort: maleIndex, QuestionType: 0 }
                    if (_$maleAnswer.Title !== "") {
                        question.Answers.push(_$maleAnswer);
                    }
                }
            }

            //标签
            question.QuestionTags = [];
            var _$tags = _$form.closest('div.modal-content').find(".btn-success[name='tags']");
            if (_$tags) {
                for (var tagIndex = 0; tagIndex < _$tags.length; tagIndex++) {
                    var _$tag = {
                        tagId: $(_$tags[tagIndex]).val(),
                        questionId: 0
                    }
                    question.QuestionTags.push(_$tag);
                }
            }
            if (question.DefaultImgId === "") {
                question.DefaultImgId = null;
            }
            abp.ui.setBusy(_$modal);
            if (question.DefaultImgId === "") {
                question.DefaultImgId = null;
            }
            _questionService.createOrUpdateQuestion(question).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new question!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('select:not([type=hidden]):first').focus();
        });

        function refreshQuestionList() {
            location.reload(true); //reload page to see new question!
            ajaxPage();
        }

        function searchQuestionList(page) {
            //拼接参数
            var query = _$searchForm.serializeFormToObject();
            if (page !== null) {
                query.page = page;
            }
            query.sceneId = $('#scene').val();
            query.questionState = $('#questionState').val();
            query.pursuingGender = $('#pursuingGender').val();
            query.creatorId = $('#creator').val();
            query.tags = new Array();
            var _$tags = _$searchForm.find(":input[select-name='Tags']");
            //query.Tags = $(_$tags).val();
            if (_$tags) {
                for (var tagIndex = 0; tagIndex < _$tags.length; tagIndex++) {
                    var _$tag = $(_$tags[tagIndex]).val();
                    if (_$tag !== "") {
                        query.tags.push(parseInt(_$tag));
                    }
                }
            }
            $.post(abp.appPath + 'Questions', query, function (content) {
                $('#questionList').html(content);
                ajaxPage();
            }, "html");
        }

        function onlineQuestion(id,questionName) {
            abp.message.confirm(
                '确定想去上线：“' + questionName + "“？",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _questionService.setOnline({ id: id }).done(function () {
                            alert("上线成功,刷新页面可看到最新状态");
                        });
                    }
                }
            )
        }

        function freezeQuestion(questionId, questionName) {
            abp.message.confirm(
                '确定想去冻结：“' + questionName + "“？",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _questionService.freezeQuestion({
                            id: questionId
                        }).done(function () {
                            refreshQuestionList();
                        });
                    }
                }
            );
        }

        function passQuestion(questionId, questionName) {
            abp.message.confirm(
                '确定审核通过：“' + questionName + "“？",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _questionService.passQuestion({
                            id: questionId
                        }).done(function () {
                            refreshQuestionList();
                        });
                    }
                }
            );
        }

        function clickSceneImg(obj) {
            var imgId = $(obj).attr("data-scene-img-id");
            _$form.closest('div.modal-content').find("a[data-scene-img-id!='']").removeClass("visible-hidden");
            $(obj).addClass("visible-hidden");
            _$form.closest('div.modal-content').find(".scene-img").hide();
            $(obj).next("i").show();
            _$form.closest('div.modal-content').find("#DefaultImgId").val(imgId);
        }

        function changeScene(obj) {
            var sceneId = $(obj).val();
            //去掉所有被激活的场景
            _$form.closest('div.modal-content').find(".carousel-inner .item").removeClass("active");
            //显示第一个场景图片所在的图片队列
            var sceneImgDivs = _$form.closest('div.modal-content')
                .find("div[data-sceneImg='1'][data-scene-id='" + sceneId + "']");
            sceneImgDivs.first().parent("div").addClass("active");
            //隐藏不属于该场景下的图片
            _$form.closest('div.modal-content').find("div[data-sceneImg='1'][data-scene-id!='" + sceneId + "']").hide();
            //显示属于该场景下的图片
            _$form.closest('div.modal-content').find("div[data-sceneImg='1'][data-scene-id='" + sceneId + "']").show();
            ////如果没有图片，显示默认图片
            if (sceneImgDivs.length > 0) {
                _$form.closest('div.modal-content').find(".carousel-inner .item:first-child").removeClass("active");
            } else {
                _$form.closest('div.modal-content').find(".carousel-inner .item:first-child").addClass("active");
            }
            //如果图片数量大于3，则显示导航条，否则隐藏
            if (_$form.closest('div.modal-content').find("div[data-sceneImg='1'][data-scene-id='" + sceneId + "']")
                .length >
                3) {
                _$form.closest('div.modal-content').find(".controls-top").show();
            } else {
                _$form.closest('div.modal-content').find(".controls-top").hide();

            }
        }

        _$form.closest('div.modal-content').find(".tag-button").click(function () {
            $(this).toggleClass("btn-default");
            $(this).toggleClass("btn-success");
        });

        //删除
        $(document).on("click", '.delete-question', function () {
            var questionId = $(this).attr('data-question-id');
            deleteQuestion(questionId);
        });

        //删除问题
        function deleteQuestion(id) {
            abp.message.confirm(
                "是否删除该问题",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _questionService.deleteQuestion({ id: id }).done(function () {
                            refreshQuestionList();
                        });
                    }

                }
            );

        }

        //分页
        function ajaxPage() {
            $(".pagination a").each(function () {
                $(this).attr("href", "javascript:void(0)");
            });
            $(".pagination a").click(function (obj) {
                var page = $(this).text();
                searchQuestionList(page);
            });

        }

        //function IniWangEditor() {
        //    var story = new Object();
        //    // 富文本编辑器
        //    var E = window.wangEditor;
        //    var defaultConfig = [
        //        'head',  // 标题
        //        'bold',  // 粗体
        //        'fontSize',  // 字号
        //        'fontName',  // 字体
        //        'italic',  // 斜体
        //        'underline',  // 下划线
        //        // 'strikeThrough',  // 删除线
        //        //'foreColor',  // 文字颜色
        //        'backColor',  // 背景颜色
        //        // 'link',  // 插入链接
        //        //'list',  // 列表
        //        'justify',  // 对齐方式
        //        // 'quote',  // 引用
        //        'emoticon',  // 表情
        //        'image',  // 插入图片
        //        // 'table',  // 表格
        //        // 'video',  // 插入视频
        //        //'code',  // 插入代码
        //        'undo',  // 撤销
        //        'redo'  // 重复
        //    ];
        //    var editorFemale = new E('#BackgroundStoryFemale-add');
        //    editorFemale.customConfig.menus = defaultConfig;
        //    editorFemale.create();
        //    story.female = editorFemale;
        //    var editorMale = new E('#BackgroundStoryMale-add');
        //    editorMale.customConfig.menus =defaultConfig;
        //    editorMale.create();
        //    story.male = editorMale;
        //    return story;
        //}


        //标签选择----------------------------------------------------------------------------------
        jQuery.expr[':'].Contains = function (a, i, m) {
            return (a.textContent || a.innerText || "").toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
        };
        jQuery.expr[':'].Equal = function (a, i, m) {
            return (a.textContent || a.innerText || "").toUpperCase() == m[3].toUpperCase();
        };

        function filterList(list) {
            $('#js-groupId').bind('input propertychange', function () {

                var filter = $(this).val();
                if (filter) {
                    var $matches = $(list).find('a:Contains(' + filter + ')').parent();
                    if ($matches.length == 0) {
                        $(list).prepend(`<li data-id="" name="tags" value=""><a href="javascript:void(0)">${filter}</a></li>`);

                        $('li', list).eq(0).slideDown().siblings().hide();
                    } else {
                        var $equalIndex = $(list).find('a:Equal(' + filter + ')').parent();
                        if ($equalIndex.length > 0) {
                            $equalIndex.slideDown();
                            $('li', list).not($matches).slideUp();
                        } else {
                            $matches.slideDown();
                            $('li', list).not($matches).slideUp();
                        }
                    }
                    var matchs = $matches;
                } else {
                    $(list).find("li").slideDown();
                }

            });
        }


        // $('#js-groupId').blur(function() {
        //     console.log("blur" + $(this).val());
        //     if ($(this).val() != "") {
        //         _tagService.createOrUpdateTag({ tagTypeId: 3, tagName: $(this).val() }).done(function() {
        //             console.log('成功');
        //         });
        //     }
        // })
        //监听输入框改变
        filterList($("#groupid"));

        //点击标签
        $('#js-groupId').bind('focus', function () {
            $('#groupid').slideDown();
        }).bind('blur', function () {
            $('#groupid').slideUp();
        })

        $('#groupid').on('click', 'li', function () {
            var newlist = $(this).text();
            $('#js-groupId').val($(this).text())
            $('#groupId').val($(this).data('id'));
            var id = $(this).val();
            $(this).hide();
            if (matchs != null) {
                if (matchs.length <= 0) {
                    console.log($(this).val());
                    _tagService.createOrUpdateTag({ tagTypeId: 3, tagName: $(this).val() }).done(function () {
                        console.log('成功');
                    });

                }
            }

            var htmladd = "";
            htmladd += '<div class="boxlist">';
            htmladd += ' <button type="button" class="btn btn-success tag-button" name="tags" value="' + id + '">'
            htmladd += newlist;
            htmladd += '</button>'
            htmladd += ' <span class="deletebtn"><i class="mdui-icon material-icons">&#xe5cd;</i></span>';
            htmladd += ' </div>';
            $('.selectbox').append(htmladd);
            $('#groupid').slideUp();
            $('#js-groupId').val('');
            $(this).siblings().show();

            $(function () {
                $(".deletebtn").on("click", function () {
                    $(this).parent().remove();
                    var id = $(this).parent(".boxlist").find('button').val();
                    $('#groupid').find('li[value= ' + id + ']').show();
                });
            })


        });

        $(".deletebtn").on("click", function () {
            $(this).parent().remove();
            console.log(11);

        });
    })
    //结束






})();