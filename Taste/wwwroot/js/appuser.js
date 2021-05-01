var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        'ajax': {
            'url': '/api/user',
            'type': 'GET',
            'dataType': 'json'
        },
        'columns': [
            { 'data': 'fullName', 'width': '25%' },
            { 'data': 'email', 'width': '25%' },
            { 'data': 'phoneNumber', 'width': '25%' },
            {
                'data': { id:'id', lockoutEnd:'lockoutEnd' },
                'render': function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        //Currently user is lock
                        return `<div class="text-center">
                            <a class="btn btn-danger"
                            onclick=LockUnlock('${data.id}')
                            style="cursor:pointer;width:100px;">
                             <i class="fas fa-unlock-alt"></i> Unlock
                            </a></div>`;
                    }
                    else {
                        return `<div class="text-center">
                            <a class="btn btn-success"
                            onclick=LockUnlock('${data.id}')
                            style="cursor:pointer;width:100px;">
                             <i class="fas fa-lock"></i> Lock
                            </a></div>`;
                    }
                }, 'width': '30%'
            }
        ],
        'language': {
            'emptyTable': 'No data has been added yet!'
        },
        'width': '100%'
    });

}

function LockUnlock(id) {

    $.ajax({
        type: 'POST',
        url: '/api/user',
        data: JSON.stringify(id),
        contentType:'application/json',
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
