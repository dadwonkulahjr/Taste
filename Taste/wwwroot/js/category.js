var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        'ajax': {
            'url': 'api/category',
            'type': 'GET',
            'dataType':'json'
        },
        'column': [
            {'data':'name', 'width':'40%'},
            { 'data': 'displayOrder', 'width': '30%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `<div class="text-center">
                            <a href="/Admin/category/upsert?id=${data}" class="btn btn-success text-white"
                            style="cursor:pointer;">Edit</a>
                            <a href="/Admin/category/delete class="btn btn-primary">Delete</a>
                            </div>
                            `;
                }
            }
        ]
    });

}
