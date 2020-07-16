function AjaxPost(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'post',
        data: data,
        success: (result) => {
            console.log(result);
            TryCallback(successCallback);
        },
        error: (err) => {
            console.log('Error : ' + err.statusText);
            TryCallback(errorCallback);
        }
    });
}

function TryCallback(callback) {
    try {
        callback();
    } catch (err) {
        console.log('Callback error : ' + err.statusText);
    }
}

//function Ajax() {
//    $.ajax({
//        url: '@Url.Action("Update", "CoilSchedule")',
//        type: 'post',
//        //dataType: 'json',
//        data: { jsonStr: JSON.stringify(obj) },
//        //contentType: 'application/json charset=utf-8',
//        //contentType: 'application/x-www-form-urlencoded charset=utf-8',
//        beforeSend: function (xhr) { },
//        error: function (err) {
//            console.log('Error : ' + err.statusText);
//        },
//        success: function (result) {
//            console.log(result);
//            Refresh();
//        }
//    });
//}