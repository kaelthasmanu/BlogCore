var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblArticulos").DataTable({
        "ajax": {
            // Controller is 'Articles' (plural) under Admin area
            "url": "/Admin/Articles/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "30%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "urlImage",
                "width": "15%",
                "render": function (data, type, row) {
                    if (!data) {
                        return '';
                    }
                    // ensure the URL is correct; if it's relative, leave as-is
                    return `<img src="${data}" width="120" alt="Image" />`;
                }
            },
            {
                // Article model uses CreatedOn -> JSON field will be createdOn
                "data": "createdOn",
                "width": "15%",
                "render": function (data) {
                    if (!data) {
                        return '';
                    }
                    try {
                        return new Date(data).toLocaleDateString("en-US");
                    } catch (e) {
                        return data;
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Articles/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                    <i class="bi bi-pencil"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick="Delete('/Admin/Articles/Delete/${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                    <i class="bi bi-trash"></i> Borrar
                                </a>
                            </div>`;
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
    var useSwal = false;
    try {
        useSwal = typeof Swal !== 'undefined' && typeof Swal.fire === 'function';
    } catch (e) {
        useSwal = false;
    }

    var doAjaxDelete = function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    if (dataTable && dataTable.ajax) dataTable.ajax.reload();
                } else {
                    toastr.error(data.message);
                }
            },
            error: function (xhr, status, err) {
                toastr.error('Ocurrió un error al intentar borrar.');
            }
        });
    };

    if (useSwal) {
        try {
            Swal.fire({
                title: '¿Está seguro de borrar?',
                text: 'Este contenido no se puede recuperar!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Sí, borrar!'
            }).then((result) => {
                if (result && result.isConfirmed) {
                    doAjaxDelete();
                }
            });
            return;
        } catch (err) {
            console.warn('Swal.fire failed, falling back to confirm()', err);
        }
    }

    if (confirm('¿Está seguro de borrar? Este contenido no se puede recuperar!')) {
        doAjaxDelete();
    }
}