var vueapp = new Vue({
    el: '#vueapp',
    data: {
        phonebtn: {
            isdisabled: false,
            timeout: 60,
            currenttime: 60,
            text: '发送验证码'
        },
        agent: {
            username: '',
            password: '',
            idCardNumber: '',
            inviteCode: '',
            code: '',
            phoneNumber: '13701813473'
        },
        api: {
            sendcode: config.baseUrl + 'agents/Users/sendcode',
            user: config.baseUrl + 'agents/users'
        },
        checked: false,
        stat: 0,
        load: false
    },
    created: function () {
        this.getstat();
    },
    methods: {
        sendcode: function () {
            var self = this;
            if (!self.isPhone(self.agent.phoneNumber)) {
                self.alert('手机号不正确');
                return;
            }


            self.$http.post(self.api.sendcode, {
                mobileNo: self.agent.phoneNumber
            }).then(response => {
                var body = response.body;
                if (!body.isError) {
                    self.phonebtn.isdisabled = true;
                    self.timeout();
                    console.log('发送成功');
                } else {
                    console.log(body.errorMessage);
                }

            }, response => {
                console.log('发送失败');
            })
        },
        register: function () {
            var self = this;
            self.load = true;
            if (self.agent.password.length < 6) {
                self.alert('密码长度不能小于6位');
                return;
            }
            if (self.agent.code == '') {
                self.alert('验证码不能为空');
                return;
            }
            if (self.agent.username == '') {
                self.alert('姓名不能为空');
                return;
            }
            if (self.agent.idCardNumber == '') {
                self.alert('身份证不能为空');
                return;
            }
            if (self.agent.inviteCode == '') {
                self.alert('邀请码不能为空');
                return;
            }
            self.$http.post(self.api.user, self.agent).then(response => {
                self.load = false;
                console.log(response);
                var body = response.body.result;
                if (!body.isError) {
                    self.alert('注册成功，等待审核');
                    self.stat = 1;

                } else {
                    console.log(body);
                    self.alert(body.errorMessage);
                }
            });
        },

        isPhone: function (number) {
            var myreg = /^[1][3,4,5,7,8][0-9]{9}$/;
            if (!myreg.test(number)) {
                return false;
            } else {
                return true;
            }
        },
        alert(msg) {
            this.$dialog.alert({
                message: msg
            });
        },
        timeout: function () {
            var self = this;
            self.phonebtn.text = --self.phonebtn.currenttime + 's';
            if (self.phonebtn.currenttime <= 0) {
                self.phonebtn.currenttime = self.phonebtn.timeout;
                self.phonebtn.text = '发送验证码';
                self.phonebtn.isdisabled = false;
            } else {
                self.phonebtn.text = self.phonebtn.currenttime + 's';
                self.phonebtn.isdisabled = true;
                setTimeout(self.timeout, 1000);

            }
        },
        getstat: function () {
            var self = this;
            self.$http.get(self.api.user)
                .then(response => {
                    var body = response.body.result;
                    console.log(body);
                    if (!body.isError){
                        if (body.data != null){
                            self.stat = body.data.state;
                        }
                    }
                });
        }
    }
})