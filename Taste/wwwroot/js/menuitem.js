﻿var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        'ajax': {
            'url': '/api/menuitem',
            'type': 'GET',
            'dataType': 'json'
        },
        'columns': [
            { 'data': 'name', 'width': '25%' },
            { 'data': 'price', 'width': '15%' },
            { 'data': 'category.name', 'width': '15%' },
            { 'data': 'foodType.name', 'width': '15%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `<div class="text-center">
                            <a href="/Admin/menuitem/upsert?id=${data}" class="btn btn-success text-white"
                            style="cursor:pointer;width:100px;">
                             <i class="far fa-edit"></i>Edit
                            </a>
                            <a class="btn btn-danger text-white"
                            style="cursor:pointer;width:100px;" onclick=Delete('/api/menuitem/'+${data})>
                             <i class="far fa-trash-alt"></i>Delete
                            </a>
                            `;
                }, 'width': '30%'
            }
        ],
        'language': {
            'emptyTable': 'No data has been added yet!'
        },
        'width': '100%',
        'order':[[2, 'asc']]
    });

}

function Delete(url) {
    swal({
        title: 'Are you sure you want to Delete?',
        text: 'You will not be able to restored the data!',
        icon: 'warning',
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            });
        }
    });
}
