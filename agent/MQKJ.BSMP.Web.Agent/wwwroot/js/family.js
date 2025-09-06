var vueapp = new Vue({
  el: '#vueapp',
  data: {
    ages: [{
        id: 0,
        value: '所有岁数'
      },
      {
        id: 1,
        value: '1岁',
        minAge: 0,
        maxAge: 1
      },
      {
        id: 2,
        value: '2岁',
        minAge: 1,
        maxAge: 2
      },
      {
        id: 3,
        value: '3岁',
        minAge: 2,
        maxAge: 3
      },
      {
        id: 4,
        value: '4岁',
        minAge: 3,
        maxAge: 4
      },
      {
        id: 5,
        value: '5岁',
        minAge: 4,
        maxAge: 5
      },
      {
        id: 6,
        value: '6岁',
        minAge: 5,
        maxAge: 6
      },
    ],
    moneys: [{
        id: 0,
        text: '所有金币'
      },
      {
        id: 1,
        minMoney: 0,
        maxMoney: 300000,
        text: '贫困家庭'
      },
      {
        id: 2,
        minMoney: 300000,
        maxMoney: 3000000,
        text: '小康家庭'
      },
      {
        id: 3,
        minMoney: 3000000,
        text: '富豪家庭'
      }
    ],
    status: [{
        id: -1,
        text: '所有状态'
      },
      {
        id: 0,
        text: '未运行'
      },
      {
        id: 1,
        text: '运行中'
      },
      {
        id: 99,
        text: '隐藏中'
      },

    ],
    search: {
      minAge: '',
      maxAge: '',
      minMoney: '',
      maxMoney: '',
      nickname: '',
      status: '',
      pageIndex: 0,
      pageSize: 10,
      moneyIndex: 0,
      ageIndex: 0,
      statusIndex: 0
    },
    count: 0,
    isLoading: false,
    listLoading: false,
    items: [],
    finished: false,
    api: {
      family: config.baseUrl + 'agents/Famliy',
      start: config.baseUrl + 'agents/Famliy/start',
      stop: config.baseUrl + 'agents/Famliy/stop',
      hide: config.baseUrl + 'agents/Famliy/hide',
      note: config.baseUrl + 'agents/Famliy/note',
      moneyurl: config.baseUrl + 'baby/CoinRecharge/GetCoinRecharges',

    },
    total: 0,
    loading: null,
    dialog: {
      show: false,
      note: '',
      familyId: 0
    },
    pop: {
      show: false,
      familyId: 0,
      moneys: []
    }
  },
  created: function () {
    this.getBuyMoneys();
  },
  methods: {
    get: function () {
      var self = this;
      self.showLoading('加载中');
      console.log(self.search.pageIndex);
      self.$http.get(self.api.family, {
          params: self.search
        })
        .then(response => {
          self.listLoading = false;
          self.isLoading = false;
          self.hidLoading();
          var body = response.body.result;
          if (body.isError) {
            //console.log(self.listLoading);
            self.alert(body.errorMessage);
            return;
          } else {
            console.log(self.listLoading);
            self.total = body.data.totalCount;
            var newItems = body.data.items;
            if (newItems.length > 0) {
              for (let index = 0; index < newItems.length; index++) {
                const item = newItems[index];
                self.items.push(item);
              }
            } else {
              self.finished = true;
            }
            self.listLoading = false;
          }
        });
    },
    onRefresh() {
      this.searchEvent();
    },
    alert(msg) {
      this.$dialog.alert({
        message: msg
      });
    },
    onLoad: function () {
      var self = this;
      if (!self.finished) {
        if (self.total != 0 && self.items.length >= self.total) {
          self.finished = true;
          self.listLoading = false;
        } else {
          self.search.pageIndex = self.search.pageIndex + 1;
          self.get();
        }

      }
    },
    searchEvent: function () {
      var self = this;
      if (self.search.ageIndex > 0) {
        var age = self.ages[self.search.ageIndex];

        if (age) {
          self.search.minAge = age.minAge;
          self.search.maxAge = age.maxAge;
        }
      } else {
        self.search.minAge = self.search.maxAge = '';
      }

      if (self.search.moneyIndex > 0) {
        var money = self.moneys[self.search.moneyIndex];
        console.log(money);
        if (money) {
          self.search.minMoney = money.minMoney;
          self.search.maxMoney = money.maxMoney;
        }
      } else {
        self.search.minMoney = self.search.maxMoney = '';
      }

      if (self.search.statusIndex > 0) {
        var status = self.status[self.search.statusIndex];
        if (status) {
          self.search.status = status.id;
        }
      } else {
        self.search.status = '';
      }
      self.search.pageIndex = 1;
      self.items = [];
      self.get();
    },
    showLoading: function (text) {
      var self = this;
      self.loading = self.$loading({
        lock: true,
        text: text == '' ? 'Loading' : text,
        spinner: 'el-icon-loading',
        background: 'rgba(0, 0, 0, 0.7)'
      })
    },
    hidLoading: function () {
      this.loading.close();
    },
    startAuto: function (id) {
      this.showLoading('开启中');
      this.updateStat(this.api.start, id);
    },
    stopAuto: function (id) {
      this.showLoading('停止中');
      this.updateStat(this.api.stop, id);
    },
    hideAuto: function (id) {
      this.showLoading('隐藏中');
      this.updateStat(this.api.hide, id);
    },
    updateStat: function (url, id) {
      var self = this;
      self.search.pageIndex = 1;
      self.$http.put(url, {
        FamilyId: id
      }).then(response => {
        var result = response.body.result;
        if (result.isError) {
          self.alert(result.errorMessage);
          self.hidLoading();
          return;
        }
        self.searchEvent();
      });
    },
    showNoteDialog: function (id) {
      var self = this;
      self.dialog.show = true;
      self.dialog.familyId = id;
      self.dialog.note = '';
    },
    dialogCancel: function () {
      this.dialog.show = false;
      this.dialog.familyId = 0;
    },
    savenote: function () {
      var self = this;
      if (self.dialog.note == '') {
        self.alert('请输入备注');
        return;
      }
      self.search.pageIndex = 1;
      var self = this;
      self.$http.put(self.api.note, {
        FamilyId: self.dialog.familyId,
        note: self.dialog.note
      }).then(response => {
        var result = response.body.result;
        if (result.isError) {
          self.alert(result.errorMessage);
          self.hidLoading();
          return;
        }
        self.searchEvent();
      });
    },
    showBuyMoney: function (familyId) {
      this.pop.show = true;
      this.pop.familyId = familyId;
    },
    hideBuyMoney: function () {
      this.pop.show = false;
      this.pop.familyId = 0;
    },
    getBuyMoneys: function () {
      var self = this;
      self.$http.get(self.api.moneyurl, {
        params: {
          pageIndex: 1,
          pageSize: 10000,
          sorting: 'coinCount desc'
        }
      }).then(response => {
        var result = response.body.result;
        if (!result.isError) {
          self.pop.moneys = result.data.items;
        }
      });
    }
  }
});