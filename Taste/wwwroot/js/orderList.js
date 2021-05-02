var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes('cancelled')) {
        loadList('cancelled');
    }
    else if (url.includes('completed')) {
        loadList('completed');
    }
    else {
        loadList('');
    }
});

function loadList(param) {
    dataTable = $('#DT_load').DataTable({
        'ajax': {
            'url': '/api/order?status=' + param,
            'type': 'GET',
            'dataType':'json'
        },
        'columns': [
            { 'data': 'orderHeader.id', 'width': '30%' },
            { 'data': 'orderHeader.pickupName', 'width': '20%' },
            { 'data': 'orderHeader.orderTotal', 'width': '15%' },
            { 'data': 'orderHeader.applicationUser.email', 'width': '15%' },
            { 'data': 'orderHeader.orderDate', 'width': '20%' },
          
            {
                'data': 'orderHeader.id',
                'render': function (data) {
                    return `<div class="text-center">
                            <a href="/admin/order/orderdetails?id=${data}" class="btn btn-success text-white"
                            style="cursor:pointer;width:100px;">
                             <i class="far fa-edit"></i>&nbsp; Details
                            </a>
                            `;
                },'width':'20%'
            }
        ],
        'language': {
            'emptyTable':'No data has been added yet!'
        },
        'width':'100%'
    });

}
