
var methodKey = "HWL-API-TEST-REQUESTMETHOD";
var requestKey = "HWL-API-TEST-REQUESTPARAMS";


function addReqV2(methodName, requestParams) {
    window.localStorage.setItem(methodKey, methodName);
    window.localStorage.setItem(requestKey, requestParams);
}

var store = {
    getMethod: function () {
        return window.localStorage.getItem(methodKey);
    },
    getRequest: function () {
        return window.localStorage.getItem(requestKey);
    }
}

function addReq(methodName,requestParams) {
    cookie.set(methodKey, methodName);
    cookie.set(requestKey, requestParams);
    //console.log(methodName);
    //console.log(requestParams);
}

var cookie = {

    expiresDays: 7,

    set: function (key, val) {
        var date = new Date();
        date.setTime(date.getTime() + this.expiresDays * 24 * 3600 * 1000);
        document.cookie = key + "=" + val + ";expires=" + date.toGMTString();
    },

    get: function (key) {
        var getCookie = document.cookie.replace(/[ ]/g, ""); 
        var arrCookie = getCookie.split(";") 
        var tips;  
        for (var i = 0; i < arrCookie.length; i++) {   
            var arr = arrCookie[i].split("=");   
            if (key == arr[0]) {  
                tips = arr[1];   
                break;   
            }
        }
        return tips;
    },

    del: function (key) { 
        var date = new Date(); 
        date.setTime(date.getTime() - 10000); 
        document.cookie = key + "=v; expires =" + date.toGMTString();
    }
}
