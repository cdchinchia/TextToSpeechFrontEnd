var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblAudios').DataTable({ 

        "ajax": {
            "url": "/TextToSpeech/GetTodosAudios",
            "type": "GET",
            "datatype": "json"
        },
        "columns":[
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "10%" },
            { "data": "creationDate", "width": "10%" },
            { "data": "text", "width": "20%" },
            { "data": "language", "width": "10%" },
            { "data": "gender", "width": "10%" },
            {
                "data": "rutaAudio",
                "render": function (data) {
                    return `<div class="text-center">                            
                             <a class="btn btn-outline-primary" href="${data}" target="_blank" role="button">Audio</a>
</div>
                            </div>`;
                }, "width": "10%"
            },
            

            { "data": "descripcion", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">                            
                             <a onclick=Delete("/TextToSpeech/Delete/${data}") class="btn btn-danger text-white" style="cursor-pointer;">Borrar</a>
                            </div>`;
                }, "width": "10%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de querer borrar el registro?",
        text: "Esta acción no puede ser revertida!",
        icon: "warning",
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