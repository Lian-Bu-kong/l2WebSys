//  Controller 位址
var _url_Send = 'CoilPDI/Send';

//  控制項元素
var _send;


$(() => {
    _send = $('#btn_send');

    _send.on('click', () => {
        AjaxPost(_url_Send, { msg: 'hello' });
    });
})