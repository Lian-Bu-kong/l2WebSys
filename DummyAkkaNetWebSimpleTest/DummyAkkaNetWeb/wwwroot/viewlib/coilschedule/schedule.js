//  控制項元素
var _table;


$(() => {
    _table = $('#tb_schedule');

    _table.bootstrapTable({
        onReorderRowsDrag: (table, row) => {  //  拖曳開始前執行
        },
        onReorderRowsDrop: (table, row) => {  //  拖曳結束後執行
        },
        onReorderRow: (newTable) => {         //  拖曳執行
            OnReorderRow(newTable);
        }
    });
})


//  拖曳執行
function OnReorderRow(newTable) {
    //  重新計算排序
    $.each(newTable, (idx, data) => {
        var trIdx = idx + 1;

        _table.find('tr:eq(' + trIdx + ') td:eq(2)').html(trIdx);
    });
}