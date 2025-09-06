function gotoPay(coinId) {
    window.location.href = "./Alipay/Detail" + window.location.search + "&" + "&coinId=" + coinId + "&ran=" + Math.round(Math.random() * 1000);;
}