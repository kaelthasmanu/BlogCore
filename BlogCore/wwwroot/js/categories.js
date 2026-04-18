var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/Admin/Categorias/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "40%" },
            { "data": "order", "width": "10%" },
            { "data": "createdDate", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                <i class="bi bi-pencil"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Categorias/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                <i class="bi bi-trash"></i> Borrar
                                </a>
                          </div>
                         `;
                }, "width": "40%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function Delete(url) {
    Swal.fire({
        title: '¿Está seguro de borrar?',
        text: 'Este contenido no se puede recuperar!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Sí, borrar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, err) {
                    toastr.error('Ocurrió un error al intentar borrar.');
                }
            });
        }
    });
}