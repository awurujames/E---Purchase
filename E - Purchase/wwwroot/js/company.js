var dataTable;
$(document).ready(function () {
    loaddatatable();
});

function loaddatatable() {

    dataTable = $('#tblTable').DataTable({
        "ajax": { url:'/admin/company/getall'},

        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'streetAddress', "width": "15%" },
            { data: 'city', "width": "15%" },
            { data: 'state', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/company/edit?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-squar"></i>Edit</a>
                        <a href="/admin/company/delete?id=${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    
                    </div>`
                },

                "width": "25%"
            }
        ]
    });
}