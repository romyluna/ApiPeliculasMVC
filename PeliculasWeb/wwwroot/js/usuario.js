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
    dataTable = $('#tblUsuarios').DataTable({
        "ajax": {
            //Cuando DataTables se inicializa, envía una solicitud HTTP GET a esa URL.
            "url": "/Usuarios/GetTodosUsuarios",//URL que apunta al metodo del controlador (categoriasController) vcorresponde al método en el controlador GetTodasCategorias
            "type": "GET",
            "dataType": "json" //como va a traernos los datos
        },
        //"data" me indica qué dato del JSON obtenido (a través de AJAX) debe usarse en esa columna
        "columns": [
            { "data": "id", "width": "50%" },
            { "data": "nombre", "width": "25%" },
            { "data": "userName", "width": "25%" }
        ]
    });
    
}

