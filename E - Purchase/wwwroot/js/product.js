$(document).ready(function () {
    loaddatatable();
});

function loaddatatable() {

    dataTable = $('#tblTable').DataTable({
        "ajax": { url: '/customer/product/getall' },

        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'description', "width": "10%" },
            { data: 'author', "width": "15%" },
          
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',

                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/customer/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-squar"></i>Edit</a>
                        <a href="/customer/product/delete?id=${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    
                    </div>`
                },

                "width": "10%"
            }
        ]
    });
}