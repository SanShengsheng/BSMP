/// <reference path="../../../lib/angularjs/angular.js" />

; (function (ng,abp) {
    var app = ng.module('modifyGrownApp', []); 
    var babiesUri = '/debugmodel/modifygrown/getbabies';
    var babiesSetUpTheLastEventGroupUri = '/debugmodel/modifygrown/SetUpTheLastEventGroup';
    var babiesSetUpAdultUri = '/debugmodel/modifygrown/SetUpAdult';
    app.filter('isAdultFilter', function () {
        return function (val) {
            return val === true? '已成年':'未成年';
        }
    });
    var modifyGrownCtrl = function ($http, $httpParamSerializer) {
        var vm = this;
        vm.babies = [];
        vm.isLoaded = false;
        vm.queryStrings = {
            babyId: '',
            babyName: '',
            familyId: '',
            mother: '',
            father: '',
            fatherNickName: '',
            motherNickName: '',
            pageIndex: 1,
            pageSize:10
        };
        var refreshBabies = function () {
            $http.get(babiesUri + '?' + $httpParamSerializer(vm.queryStrings)).then(function (result) {
                if (result.data.result && result.data.result.length > 0) {
                    Array.prototype.push.apply(vm.babies, result.data.result);
                } else {
                    vm.isLoaded = true;
                }
            }, function () {
                    abp.notify.error("加载失败");
                });
        }
        refreshBabies();
        vm.loadBabies = function () {
            vm.queryStrings.pageIndex = 1;
            vm.isLoaded = false;
            vm.babies = [];
            refreshBabies();
            
        }
        vm.refreshBabiesWithEmpty = function () {
            vm.babies = [];
            refreshBabies();    
        }
        vm.loadMores = function () {
            vm.queryStrings.pageIndex = vm.queryStrings.pageIndex + 1;
            refreshBabies();
        }
        vm.setUpLastEventOrAdult = function (type, baby) {
            console.log(baby);
            var currentActionUri = type === 'event' ? babiesSetUpTheLastEventGroupUri : babiesSetUpAdultUri;         
            var promise =$http.post(currentActionUri, {
                babyId: baby.babyId
            });
            abp.ui.setBusy('#modifyGrownId', promise);
            promise.then(function (response) {
                vm.refreshBabiesWithEmpty();
            }, function () {
                    abp.notify.error("设置失败");
            });
        }
    };
    modifyGrownCtrl.$inject = ['$http','$httpParamSerializer'];
    app.controller('modifyGrownCtrl',modifyGrownCtrl);
})(angular,abp);