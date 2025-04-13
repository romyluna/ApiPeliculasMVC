//para traer la lista de categorias de la api.

var dataTable; //puro de js

//como empieza con $ es de jquery.
//permite cargar primero todo el html y despues se cargue la tabla 

$(document).ready(function () {
    loadDataTable();
});

console.log($);

//esta URL LLAMA AL CONTROLADOR CORRESPONDIENTE EN ESTE CASO AL DE CATEGORIAS 
function loadDataTable() {
    dataTable = $('#tblPeliculas').DataTable({
        "ajax": {
            //Cuando DataTables se inicializa, envía una solicitud HTTP GET a esa URL.
            "url": "/Peliculas/GetTodasPeliculas",//URL que apunta al metodo del controlador (categoriasController) vcorresponde al método en el controlador GetTodasCategorias
            "type": "GET",
            "dataType": "json" //como va a traernos los datos
        },
        //"data" me indica qué dato del JSON obtenido (a través de AJAX) debe usarse en esa columna
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "20%" },
            { "data": "clasificacion", "width": "5%" },
            { "data": "duracion", "width": "5%" },
            {
                "data": "fechaCreacion",
                "width": "20%",
                //para formatear la fecha
                "render": function (data) {
                    //uso de la libreria moment.js que esta en la carpeta shared: _layout.
                    return moment(data).format('YYYY/MM/DD')
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/Peliculas/Edit/${data}" class="btn btn-success text-white" style="cursor-pointer;">Editar</a>
                            &nbsp;
                             <a onclick=Delete("/Peliculas/Delete/${data}") class="btn btn-danger text-white" style="cursor-pointer;">Borrar</a>
                            </div>`;
                }, "width": "20%"
            },
        ]
    });
    
}

//va a refrescar con ajax el dataTable
//uso de swal: la biblioteca SweetAlert para mostrar un cuadro de diálogo personalizado con un mensaje de confirmación
//permite eliminar el registro sin recargar la pagina
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
                        dataTable.ajax.reload();//actualiza la tabla una vez que se elimino el registro
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}