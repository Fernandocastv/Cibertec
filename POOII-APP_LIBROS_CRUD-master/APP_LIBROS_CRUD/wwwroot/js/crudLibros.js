// Modelo Libro
const _modeloLibro = {
    IDLibro: 0,
    Titulo: "",
    IDAutor: 0,
    NroPaginas: 0,
    IDEditorial: 0
}

// Función para mostrar la lista de libros
function ListarLibros() {
    fetch("/Home/listarLibros").then(response => {
        return response.ok ? response.json() : Promise.reject(response)
    }).then(responseJson => {
        if (responseJson.length > 0) {
            $("#tablaLibros tbody").html("");
            responseJson.forEach((libro) => {
                $("#tablaLibros tbody").append(
                    $("<tr>").append(
                        $("<th>").text(libro.idLibro),
                        $("<td>").text(libro.titulo),
                        $("<td>").text(libro.autor.nombre_Autor),
                        $("<td>").text(libro.nroPaginas),
                        $("<td>").text(libro.editorial.nombreEditorial),
                        $("<td>").append(
                            $("<button>").addClass("btn btn-outline-primary btn-update-libro")
                            .text("Editar").data("dataLibro", libro)
                        ),
                        $("<td>").append(
                            $("<button>").addClass("btn btn-outline-danger btn-delete-libro")
                            .text("Eliminar").data("dataLibro", libro)
                        )
                    )
                )
            })
        }
    })
}

function cargarAutores() {
    fetch("/Home/listarAutores").then(response => {
        return response.ok ? response.json() : Promise.reject(response)
    }).then(responseJson => {
        if (responseJson.length > 0) {
            responseJson.forEach((item) => {
                $("#cboAutor").append(
                    $("<option>").val(item.idAutor).text(item.nombre_Autor)
                )
            })
        }
    })
}

function cargarEditoriales() {
    fetch("/Home/listarEditoriales").then(response => {
        return response.ok ? response.json() : Promise.reject(response)
    }).then(responseJson => {
        if (responseJson.length > 0) {
            responseJson.forEach((item) => {
                $("#cboEditorial").append(
                    $("<option>").val(item.idEditorial).text(item.nombreEditorial)
                )
            })
        }
    })
}

document.addEventListener("DOMContentLoaded", function () {
    ListarLibros();
    cargarAutores();
    cargarEditoriales();
}, false)

function MostrarModal() {
    $("#txtTitulo").val(_modeloLibro.Titulo);
    $("#cboAutor").val(_modeloLibro.IDAutor == 0 ? $("#cboAutor option:first").val() : _modeloLibro.IDAutor);
    $("#txtNroPaginas").val(_modeloLibro.NroPaginas);
    $("#cboEditorial").val(_modeloLibro.IDEditorial == 0 ? $("#cboEditorial option:first").val() : _modeloLibro.IDEditorial);

    $("#modalLibro").modal("show");
}

function MostrarModalEliminar() {
    $("#txtIDLibro").val(_modeloLibro.IDLibro);

    $("#modalDeleteLibro").modal("show");
}

$(document).on("click", ".btn-delete-libro", function () {
    const _libro = $(this).data("dataLibro");

    _modeloLibro.IDLibro = _libro.idLibro;

    MostrarModalEliminar(_modeloLibro.IDLibro);
})

$(document).on("click", ".btn-new-libro", function () {
    _modeloLibro.IDLibro = 0;
    _modeloLibro.Titulo = "";
    _modeloLibro.IDAutor = 0;
    _modeloLibro.NroPaginas = 0;
    _modeloLibro.IDEditorial = 0;

    MostrarModal();
})

$(document).on("click", ".btn-update-libro", function () {
    const _libro = $(this).data("dataLibro");

    _modeloLibro.IDLibro = _libro.idLibro;
    _modeloLibro.Titulo = _libro.titulo;
    _modeloLibro.IDAutor = _libro.autor.idAutor;
    _modeloLibro.NroPaginas = _libro.nroPaginas;
    _modeloLibro.IDEditorial = _libro.editorial.idEditorial;

    MostrarModal();
})

$(document).on("click", ".btn-eliminar-libro", function () {
    const idLibro = $("#txtIDLibro").val();

    fetch(`/Home/eliminarLibro/${idLibro}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json;charset=utf-8" },
    }).then(response => {
        if (response.ok) {
            $("#modalDeleteLibro").modal("hide");
            Swal.fire("Listo!", "Libro eliminado", "success");
            ListarLibros();
        } else {
            Swal.fire("Lo sentimos!", "El libro no pudo ser eliminado", "danger");
        }
    })
})

$(document).on("click", ".btn-save-libro", function () {
    const modelo = {
        IDLibro: _modeloLibro.IDLibro,
        Titulo: $("#txtTitulo").val(),
        autor: {
            IDAutor: $("#cboAutor").val()
        },
        NroPaginas: $("#txtNroPaginas").val(),
        editorial: {
            IDEditorial: $("#cboEditorial").val()
        }
    }

    if (_modeloLibro.IDLibro == 0) {
        fetch("/Home/guardarLibro", {
            method: "POST",
            headers: { "Content-Type": "application/json;charset=utf-8" },
            body: JSON.stringify(modelo)
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then(responseJson => {
            if (responseJson.valor) {
                $("#modalLibro").modal("hide");
                Swal.fire("Listo!", "Libro registrado", "success");
                ListarLibros();
            } else {
                Swal.fire("Lo sentimos!", "No se pudo registrar el Libro", "error");
            }
        })
    } else {
        fetch("/Home/actualizarLibro", {
            method: "PUT",
            headers: { "Content-Type": "application/json;charset=utf-8" },
            body: JSON.stringify(modelo)
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then(responseJson => {
            if (responseJson.valor) {
                $("#modalLibro").modal("hide");
                Swal.fire("Listo!", "Libro fue actualizado", "success");
                ListarLibros();
            } else {
                Swal.fire("Lo sentimos!", "No se pudo actualizar el Libro", "error")
            }
        })
    }
})