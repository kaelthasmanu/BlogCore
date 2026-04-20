var dataTable;

$(document).ready(function () {
    cargarCategorias();
});

function cargarCategorias() {
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/Admin/Categorias/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "8%" },
            { "data": "name", "width": "45%" },
            { "data": "order", "width": "10%" },
            {
                "data": "createdDate",
                "width": "20%",
                "render": function (data, type, row) {
                    // Be resilient: some backends/serializers may return created_at, createdAt or createdDate
                    var dateVal = data || row && (row.created_at || row.createdAt || row.createdDate);
                    if (!dateVal) return '';
                    try {
                        return new Date(dateVal).toLocaleDateString();
                    } catch (e) {
                        return dateVal;
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                    <i class="bi bi-pencil"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick="Delete('/Admin/Categorias/Delete/${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                    <i class="bi bi-trash"></i> Borrar
                                </a>
                            </div>`;
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
        }
    });
}

function Delete(url) {
    // Try using SweetAlert2 if available and working, otherwise fallback to confirm().
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
            // If Swal throws (compatibility issue), fallback below
            console.warn('Swal.fire failed, falling back to confirm()', err);
        }
    }

    // Fallback
    if (confirm('¿Está seguro de borrar? Este contenido no se puede recuperar!')) {
        doAjaxDelete();
    }
}
