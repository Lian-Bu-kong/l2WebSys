//  Controller 位址
var _url_Schedul;
var _url_Update;

//  控制項元素
var _refresh;
var _update;
var _partial;


$(() => {
    GetElement();

    //  監聽事件
    _refresh.on('click', () => Refresh());
    _update.on('click', () => Update());
});


//  取得 dom 上所需元素
function GetElement() {
    _url_Schedule = 'CoilSchedule/Schedule';
    _url_Update = 'CoilSchedule/Update';
    _refresh = $('#btn_refresh');
    _update = $('#btn_update');
    _partial = $('#div_partial');
}


//  刷新
function Refresh() {
    $.get(_url_Schedule, (result) => _partial.html(result));
}


//  更新
function Update() {
    var obj = GetChangedRow();

    if (obj.length < 1) return;

    AjaxPost(_url_Update, { jsonStr: JSON.stringify(obj) }, Refresh);
}


//  取得有變動順序的 row
function GetChangedRow() {
    var obj = [];

    $.each(_table.find('tr'), (idx, data) => {
        if (idx < 1) return;
        if (data.children[2].textContent == data.children[4].textContent) return;

        obj.push({
            Id: data.children[0].textContent,
            SeqNo: data.children[1].textContent,
            SortNo: data.children[2].textContent
        });
    });

    return obj;
}