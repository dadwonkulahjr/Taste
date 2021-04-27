var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        'ajax': {
            'url': 'api/category',
            'type': 'GET',
            'dataType': 'json'
        },
        'column': [
            { 'data': 'name', 'width': '40%' },
            { 'data': 'displayOrder', 'width': '40%' },
            {
                'data': 'id',
                'render': function (data) {
                    return ` <div class="text-center">
                    <a href="/Admin/category/upsert?id=${data}" class="btn btn-btn-success text-white"
                    style="cursor:pointer;width:100px">
                        <i class="far fa-edit"></i>Edit 
                    </a>
                    <a class="btn btn-btn-danger text-white"
                    style="cursor:pointer;width:100px">
                        <i class="far fa-trash-alt"></i>Delete 
                    </a>
                    </div>
                    `;
                },
                'width':'30%'
            }
        ],
        'language': [
            { 'emptyTable': 'no data found' }
        ],
        'width':'100%'

    });
}
