(function () {
    $(function () {
        var _$modal = $('#modalloading');
        //var browser = {
        //    versions: function () {
        //        var u = navigator.userAgent, app = navigator.appVersion;
        //        return { //移动终端浏览器版本信息 
        //            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端 
        //            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或uc浏览器 
        //            iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器 
        //            iPad: u.indexOf('iPad') > -1, //是否iPad 
        //        };
        //    }(),
        //}
        //if (browser.versions.iPhone || browser.versions.iPad || browser.versions.ios) {
        //    window.location.href = "http://www.asiayak.cn";
        //    alert("苹果手机" + browser);
        //}
        //if (browser.versions.android) {
        //    window.location.href = "http://www.qq.com";
        //    alert("安卓" + browser);
        //}

        // 唤起支付
        //$(".bsmp-h5-alipay").click(function () {
        //    abp.ui.setBusy(_$modal);
        //    var coinId = $(this).attr('data-id');
        //    getForm(coinId);
        //});
        setTimeout(function () {
            abp.ui.setBusy(_$modal);
            //var coinId = $(this).attr('data-id');
            getForm();
        }, 1000);
        /// 获取url参数
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
        /// 获取支付宝返回的支付表单
        function getForm(coinId) {
            // 获取支付表单
            abp.ajax({
                url: '/ChineseBabies/AliPay/BuyCoins?id=' + getUrlParam("coinId") + '&familyId=' + getUrlParam("familyId") + '&playerId=' + getUrlParam("playerId") + '&clientType=3',
                type: 'Get',
                headers: {
                    "Abp-TenantId": 295
                }
            }).done(function (data) {
                if (data.errorMessage != '' && data.errorMessage != null) {
                    abp.message.error(data.errorMessage);
                } else {
                    //$(document.body).append(data.data.formTableString);
                    $("#alipay-container").append(data.data.formTableString);
                }
                setTimeout(function () {
                    abp.ui.clearBusy(_$modal);
                }, 8000);

            });
        }
    });

})();