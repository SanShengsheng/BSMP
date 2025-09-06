/// <reference path="../../../lib/angularjs/angular.js" />
(function (win, ng, abp) {
    var allTypesUri = 'ChineseBabies/ImportData/Types';
    var uploadsUri = 'ChineseBabies/ImportData/DoPost';
    var uploadTaskUri = 'ChineseBabies/ImportData/TaskList';
    var app = ng.module('importApp', []);
    var urlJoin = function (seg) {
        return abp.appPath + seg;
    }
    app.filter('uploadStateFilter', function () {
        return function (state) {
            var _result;
            switch (state) {
                case 0:
                    _result = '失败';
                    break
                case 1:
                    _result = '完成';
                    break;
                case 2:
                    _result = '导入中';
                    break;
                default:
                    _result = '未知';
            }
            return _result;
        }
    });
    app.service('uploadService', function () {
        function progressHandlingFunction(callback) {
            return function (e) {
                var curr = e.loaded;
                var total = e.total;
                var process = curr / total * 80;
                callback(process);
            }
        }
        this.doAjax = function (data, callback, final) {
            var ajaxOpts = {
                contentType: false,
                processData: false,
                url: urlJoin(uploadsUri),
                type: "POST",
                data: data,
                xhr: function () {
                    var myXhr = $.ajaxSettings.xhr();
                    if (myXhr.upload) {
                        myXhr.upload.addEventListener('progress', progressHandlingFunction(callback), false)
                    }
                    return myXhr;
                }
            };
            var defe = $.ajax(ajaxOpts);
            final && defe.always(final);
            return defe;
        }
    });

    var importCtrl = function ($scope, $http, uploadService, $interval) {
        var vm = this;
        vm.uploadTasks = [];
        vm.backGroundTasks = [];
        vm.options = {
            maxTask: 5,
            isUploads: true,
            refreshTime: 3
        }
        vm.each = function (arr, callback) {
            if (Object.prototype.toString.call(arr) !== "[object Array]") return;
            if (!callback) return;
            for (var i = 0; i < arr.length; i++) {
                if (callback(arr[i], i) === false) break;
            }
        };
        vm.validateSuffix = function (suff) {
            var validSuffixs = ['xlsx'];
            var result = false;
            vm.each(validSuffixs, function (item, idx) {
                result = (suff === item);
            });
            return result;
        };
        vm.validateInput = function () {
            var result = true;
            vm.each(vm.uploadTasks, function (item, idx) {
                result = (item.selectType.value !== -1) && item.status === 0 && item.fileName !== '' && vm.validateSuffix(item.suffix);
                return result;
            });
            return result;
        };
        $http.get(urlJoin(allTypesUri)).then(function (result) {
            vm.allTypes = result.data.result;
        });
        vm.refreshBaskgroudTasks = function () {
            $http.get(urlJoin(uploadTaskUri)).then(function (result) {
                vm.backGroundTasks = result.data.result;
            });
        }
        vm.refreshBaskgroudTasks();
        $interval(vm.refreshBaskgroudTasks, vm.options.refreshTime * 1000);
        vm.pushTask = function () {
            vm.uploadTasks.push({
                selectType: {
                    description: '选择类型',
                    value: -1
                },
                fileName: '',
                $elem: '',
                status: 0,
                suffix: '',
                hasError: false
            });
        }
        vm.pushTask();
        vm.onSelectTpye = function (idx, task) {
            task.selectType = vm.allTypes[idx];
        }
        vm.removeTask = function ($event, taskidx) {
            Array.prototype.splice.call(vm.uploadTasks, taskidx, 1);
            $($event.target).closest('li').find("input").val("");
        }
        vm.toUploads = function () {
            if (vm.uploadTasks.length === 0) {
                abp.notify.error("没有添加上传任务");
                return;
            }
            if (!vm.validateInput()) {
                abp.notify.error("请核对待上传的任务参数");
                return;
            }
            abp.ui.setBusy("#importAppContainer");
            vm.taskCount = 0;
            vm.each(vm.uploadTasks, function (item, idx) {
                vm.taskCount += 1;
                var formData = new FormData();
                formData.append("tableDataType", item.selectType.value);
                formData.append("formFile", item.$elem[0].files[0]);
                uploadService.doAjax(formData, function (currProcess) {
                    $scope.$apply(function () {
                        item.status = currProcess;
                    });
                }, function () {
                    if (--vm.taskCount === 0) {
                        abp.ui.clearBusy("#importAppContainer");
                        vm.uploadTasks = [];
                    }
                });
            });
        }
        ng.element(win.document).ready(function () {
            $('#importForm').on('change', 'input[type="file"]', function (e) {
                var $self = $(this);
                var file = $self.val();
                var fileName = String.prototype.substring.call(file, file.lastIndexOf("\\") + 1);
                var ext = fileName.substr(fileName.lastIndexOf(".") + 1);
                var idx = $self.data("identity");
                $scope.$apply(function () {
                    vm.uploadTasks[idx].fileName = fileName;
                    vm.uploadTasks[idx].$elem = $self;
                    vm.uploadTasks[idx].suffix = ext;
                });
            });
        });
    }

    importCtrl.$inject = ['$scope', '$http', 'uploadService', '$interval'];
    app.controller('importCtrl', importCtrl);
})(window, angular, abp);

