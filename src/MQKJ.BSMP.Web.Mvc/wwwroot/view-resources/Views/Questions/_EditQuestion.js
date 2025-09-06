(function($) {

    var _questionService = abp.services.app.question;
    var _tagService = abp.services.app.tag;
    var _$modal = $('#QuestionEditModal');
    var _$form = _$modal.find('form');
    //var backgroundStory = null; // IniEditViewWangEditor();

    _$form.validate({
        rules: {
            //Password: "required",
            //ConfirmPassword: {
            //    equalTo: "#Password"
            //}
        }
    });


    //场景&场景图片start--
    _$form.closest('div.modal-content').find(".carousel-inner a[data-scene-img-id!='']").click(function() {
        clickSceneImg(this);
    });

    _$form.closest('div.modal-content').find(":input[name='SceneId']").on('changed.bs.select', function(e) {
        changeScene(this);
    });
    //场景&场景图片end--
    //初始化下拉框
    $('.selectpicker').selectpicker({});

    function save() {
        if (!_$form.valid()) {
            return;
        }
        var question = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        //女生画面-背景故事
        var backgroundStoryFemale = tinyMCE.get('BackgroundStoryFemale-Edit').getContent().replace("padding-left", "text-indent");
        question.BackgroundStoryFemale = backgroundStoryFemale;
        //男生画面-背景故事
        var backgroundStoryMale = tinyMCE.get('BackgroundStoryMale-Edit').getContent().replace("padding-left", "text-indent");
        question.BackgroundStoryMale = backgroundStoryMale;
        //女性选项
        question.Answers = [];
        var _$femaleAnswers = _$form.closest('div.modal-content').find("input[answer-name='AnswerFemale']");
        if (_$femaleAnswers) {
            for (var roleIndex = 0; roleIndex < _$femaleAnswers.length; roleIndex++) {
                var _$answer = { Title: $(_$femaleAnswers[roleIndex]).val(), Sort: roleIndex, QuestionType: 1, QuestionID: parseInt(question.Id), Id: parseInt($(_$femaleAnswers[roleIndex]).attr("optionId")) }
                if (_$answer.Title !== "") {
                    question.Answers.push(_$answer);
                }
            }
        }
        //男性选项
        var _$maleAnswers = _$form.closest('div.modal-content').find("input[answer-name='AnswerMale']");
        if (_$maleAnswers) {
            for (var maleIndex = 0; maleIndex < _$maleAnswers.length; maleIndex++) {
                var _$maleAnswer = { Title: $(_$maleAnswers[maleIndex]).val(), sort: maleIndex, QuestionType: 0, QuestionID: parseInt(question.Id), ID: parseInt($(_$maleAnswers[maleIndex]).attr("optionId")) }
                if (_$maleAnswer.Title !== "") {
                    question.Answers.push(_$maleAnswer);
                }
            }
        }

        //标签
        question.QuestionTags = [];
        var _$tags = _$form.closest('div.modal-content').find(".btn-success[name='tags'],button[name='tags'][questionTagsId!='0']");
        if (_$tags) {
            for (var tagIndex = 0; tagIndex < _$tags.length; tagIndex++) {
                var _$tag = {
                    id: $(_$tags[tagIndex]).attr("questionTagsId"),
                    tagId: $(_$tags[tagIndex]).val(),
                    questionId: parseInt(question.Id),
                    isDeleted: $(_$tags[tagIndex]).attr("questionTagsId") !== "0" && !$(_$tags[tagIndex]).hasClass("btn-success")
                }
                question.QuestionTags.push(_$tag);
            }
        }
        abp.ui.setBusy(_$modal);
        _questionService.createOrUpdateQuestion(question).done(function() {
            _$modal.modal('hide');
            // location.reload(true); //reload page to see edited tag!

        }).always(function() {
            abp.ui.clearBusy(_$modal);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function(e) {
        e.preventDefault();
        save();
    });

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


    _$form.closest('div.modal-content').find(".tag-button").click(function() {
        console.log("click");
        $(this).toggleClass("btn-default").toggleClass("btn-success");
    });


    $.AdminBSB.input.activate(_$form);

    _$modal.on('shown.bs.modal',
        function() {
            _$form.find('input[type=text]:first').focus();
        });


    //标签选择
    jQuery.expr[':'].Contains = function(a, i, m) {
        return (a.textContent || a.innerText || "").toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
    };
    jQuery.expr[':'].Equal = function(a, i, m) {
        return (a.textContent || a.innerText || "").toUpperCase() == m[3].toUpperCase();
    };

    function filterList(list) {
        $('#js-groupId2').bind('input propertychange', function() {

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
               return $matches;
            } else {
                $(list).find("li").slideDown();
            }

        });
    }

    $(function() {
     var  matchs = filterList($("#groupid2"));
        $('#js-groupId2').bind('focus', function() {
            $('#groupid2').slideDown();
        }).bind('blur', function() {
            $('#groupid2').slideUp();
        })

        $('#groupid2').on('click', 'li', function() {
            var newlist = $(this).text();
            $('#js-groupId2').val($(this).text())
            $('#groupId2').val($(this).data('id'));
            var id = $(this).val();
            $(this).hide();
            if (matchs != null) {
                if (matchs.length <= 0) {
                    console.log($(this).val());
                    _tagService.createOrUpdateTag({ tagTypeId: 3, tagName: $(this).val() }).done(function() {
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
            $('#groupid2').slideUp();
            $('#js-groupId2').val('');
            $(this).siblings().show();

            //删除
            $(function() {
                $(".deletebtn").on("click", function() {
                    $(this).parent().remove();
                    var id = $(this).parent(".boxlist").find('button').val();
                    $('#groupid2').find('li[value= ' + id + ']').show();
                });
            })
        });
    });
    $(function() {
            $(".deletebtn").on("click", function() {
                $(this).parent().remove();
            });
        })
        //结束
})(jQuery)