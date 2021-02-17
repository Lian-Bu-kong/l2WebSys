//  控制項元素
var _txt_Uncoiler;
var _txt_UncoilerSkid1;
var _txt_UncoilerSkid2;
var _txt_Recoiler;
var _txt_RecoilerSkid1;
var _txt_RecoilerSkid2;
var _txt_ActualRollForce;
var _txt_ActualElongation;
var _txt_SetupRollForce;
var _txt_SetupElongation;
var _btn_input;
var _h_title;
var _canvas;

//  設定繪圖環境
var _cxt;
var _y = 205;

//  Hub
var _connection;

//  Demo index
var _demoIdx = 0;


$(() => {
    _demoIdx = 0;

    GetElement();
    SetHub();
    DrawLine('white', 300, _y, 810, _y);



    //  Demo
    _h_title.on('click', () => {
        SelectDemo();
    });
});


//  取得 dom 上所需元素
function GetElement() {
    _txt_Uncoiler = $('#txt_Uncoiler');
    _txt_UncoilerSkid1 = $('#txt_UncoilerSkid1');
    _txt_UncoilerSkid2 = $('#txt_UncoilerSkid2');
    _txt_Recoiler = $('#txt_Recoiler');
    _txt_RecoilerSkid1 = $('#txt_RecoilerSkid1');
    _txt_RecoilerSkid2 = $('#txt_RecoilerSkid2');
    _txt_ActualRollForce = $('#txt_ActualRollForce');
    _txt_ActualElongation = $('#txt_ActualElongation');
    _txt_SetupRollForce = $('#txt_SetupRollForce');
    _txt_SetupElongation = $('#txt_SetupElongation');
    _btn_input = $('#btn_input');
    _h_title = $('#h_title');
    _canvas = $('canvas');
    _cxt = $('#div_track canvas')[0].getContext('2d');
}


function SetHub() {
    //  建立 hub 連線
    _connection = new signalR.HubConnectionBuilder().withUrl('/trackinghub').build();

    //  接收廣播
    _connection.on('UpdateTrackingMap', (data) => {
        UpdateTrackingMap(data);
    });

    //  開始連線
    _connection.start()
        .then(() => {
            HubStart();
        })
        .catch((err) => {
            HubError(err)
        });
}


//  接收廣播
function UpdateTrackingMap(data) {
    //debugger;

    _txt_Uncoiler.val(data.uncoiler);
    _txt_UncoilerSkid1.val(data.uncoilerSkid1);
    _txt_UncoilerSkid2.val(data.uncoilerSkid2);

    _txt_Recoiler.val(data.recoiler);
    _txt_RecoilerSkid2.val(data.recoilerSkid2);
    _txt_RecoilerSkid1.val(data.recoilerSkid1);

    _txt_ActualRollForce.html(data.actualRollForce);
    _txt_ActualElongation.html(data.actualElongation);
    _txt_SetupRollForce.html(data.setupRollForce);
    _txt_SetupElongation.html(data.setupElongation);
}


//  Hub 啟動時要執行的動作
function HubStart() {
    //  入料
    _btn_input.on('click', (e) => {
        debugger;

        var l1_switch = 1;
        var setup_rollForce = 10;
        var setup_elongation = 10;
        _connection.invoke('Input', l1_switch, setup_rollForce, setup_elongation);
    });
}


//  Hub 報錯時要執行的動作
function HubError(err) {

}


//  畫線
function DrawLine(color, sX, sY, eX, eY) {
    _cxt.beginPath();           //  開啟新路徑
    _cxt.lineWidth = 5;         //  設定畫筆的寬度
    _cxt.strokeStyle = color;   //  設定畫筆的顏色
    _cxt.moveTo(sX, sY);        //  設定筆觸的位置
    _cxt.lineTo(eX, eY);        //  設定移動的方式
    _cxt.stroke();		        //  畫線
    _cxt.closePath();	        //  封閉路徑
}








/* Demo Dummy Use */

function Demo1() {
    DrawLine('red', 310, _y, 600, _y);
}


function Demo2() {
    DrawLine('red', 310, _y, 800, _y);
    _txt_Recoiler.val(_txt_Uncoiler.val().replace('CM', 'CA'));
    _txt_Uncoiler.val(_txt_UncoilerSkid1.val());
    _txt_UncoilerSkid1.val(_txt_UncoilerSkid2.val());
    _txt_UncoilerSkid2.val('CM201230040000');
}


function Demo3() {
    _txt_RecoilerSkid2.val(_txt_Recoiler.val());
    _txt_Recoiler.val(_txt_Uncoiler.val().replace('CM', 'CA'));
    _txt_Uncoiler.val(_txt_UncoilerSkid1.val());
    _txt_UncoilerSkid1.val(_txt_UncoilerSkid2.val());
    _txt_UncoilerSkid2.val('CM201230050000');
}


function Demo4() {
    _txt_RecoilerSkid1.val(_txt_RecoilerSkid2.val());
    _txt_RecoilerSkid2.val(_txt_Recoiler.val());
    _txt_Recoiler.val(_txt_Uncoiler.val().replace('CM', 'CA'));
    _txt_Uncoiler.val(_txt_UncoilerSkid1.val());
    _txt_UncoilerSkid1.val(_txt_UncoilerSkid2.val());
    _txt_UncoilerSkid2.val('CM201230060000');
}


function Default() {
    DrawLine('white', 310, _y, 800, _y);
    $('.col-skid-img input[type=text]').val('');
    //$('#txt_Uncoiler').val('CM201230010000');
    //$('#txt_UncoilerSkid1').val('CM201230020000');
    //$('#txt_UncoilerSkid2').val('CM201230030000');
}


function SelectDemo() {
    _demoIdx = (_demoIdx >= 4) ? 0 : _demoIdx + 1;

    switch (_demoIdx) {
        case 1: Demo1(); break;
        case 2: Demo2(); break;
        case 3: Demo3(); break;
        case 4: Demo4(); break;
        default: Default(); break;
    }
}