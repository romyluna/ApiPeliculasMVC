# 🎬 App de Películas (.NET + API REST)

<a name="app-de-películas-net--api-rest"></a>
✏️Este proyecto es una aplicación web desarrollada con .NET que permite a los usuarios autenticados gestionar una lista de películas mediante un sistema CRUD → Crear, Leer, Actualizar y Eliminar. 
</br>
✏️Cuenta con un sistema de registro y login para proteger el acceso:
</br>
🔓 Los usuarios **no autenticados** pueden visualizar la lista de películas.
</br>
🔐 Los usuarios **logueados** tienen acceso completo para realizar acciones de alta, modificación y eliminación.
</br>
✏️consume una API externa de películas para obtener información.
</br>
✏️Armada como un ejemplo de integración de front-end y back-end en una arquitectura.

---

## ✨ Funcionalidades

✅ Listado de películas desde una API externa
</br>
➕ Alta de nuevas películas manualmente
</br>
✏️ Edición de películas
</br>
❌ Eliminación de películas
</br>
🔎 Búsqueda por título

---
## 📑 Índice

1. [Descripción general](#app-de-películas-net--api-rest)
2. [Funcionalidades](#funcionalidades)
3. [Tecnologías utilizadas](#tecnologías-utilizadas)
4. [Cómo ejecutar el proyecto](#cómo-ejecutar-el-proyecto)
5. [Pantalla principal](#pantalla-principal)
6. [Funcionalidades para usuarios logueados (CRUD)](#funcionalidades-para-usuarios-logueados-crud)
   - [Listado de Categorías](#listado-de-categorías)
   - [Gestión de Películas (CRUD)](#gestión-de-películas-crud)
7. [Búsqueda y Filtros de películas](#búsqueda-y-filtros-de-películas)
8. [Notas finales](#notas-finales)
9. [Contacto](#contacto)

---

## 🧰 Tecnologías utilizadas
<a name="tecnologías-utilizadas"></a>
- ASP.NET Core MVC
- C#
- Entity Framework
- API REST (pública)
- Bootstrap
- HTML/CSS
- JavaScript (básico)

---

## 🚀 Cómo ejecutar el proyecto
<a name="cómo-ejecutar-el-proyecto"></a>
Cloná este repositorio:
   
git clone https://github.com/tu-usuario/ApiPeliculasMVC.git

Abrilo en Visual Studio o Visual Studio Code.

Asegurate de tener configurada la cadena de conexión a la base de datos en appsettings.json.

Ejecutá las migraciones para la base de datos (si estás usando EF Core):

dotnet ef database update
Iniciá la aplicación:
dotnet run

---

### 🖥 Pantalla principal
<a name="pantalla-principal"></a>

![Pantalla principal](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/pantalla_principal1.PNG?raw=true)

### 🔐 Pantalla de Login

Pantalla de registro para un usuario nuevo:

![Login](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/registroNuevo_usuario.png?raw=true)

Pantalla de acceso para un usuario ya registrado:

![Login](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/acceso_usuarios.png?raw=true)

## 👤 Funcionalidades para usuarios logueados (CRUD)
<a name="funcionalidades-para-usuarios-logueados-crud"></a>
📁 Gestión de Categorías

### 📋 Listado de Categorías
<a name="listado-de-categorías"></a>
El usuario puede ver todas las categorías existentes en el sistema.

![Listado de categorías](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/categorias_listado.PNG?raw=true)

---

### ➕ Crear Nueva Categoría

Accediendo al formulario, el usuario puede registrar una nueva categoría personalizada.

![Formulario nueva categoría](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/categorias_altas.PNG?raw=true)

### ✏️ Editar o ❌ Eliminar Categorías

Desde la misma vista de listado, se puede modificar o eliminar una categoría existente.

![Editar o eliminar categoría](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/categorias_borrar.png?raw=true)

---

## 🎬 Gestión de Películas (CRUD)
<a name="gestión-de-películas-crud"></a>

Los usuarios logueados tienen acceso completo para administrar las películas del sistema. Esto incluye:

### 📋 Listado de Películas

Muestra todas las películas registradas, con la opción de editar o eliminar cada una.


![Listado de películas](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/peliculas_listado.PNG?raw=true)

---

### ➕ Alta de Película

Desde el formulario de crear nueva pelicula, se pueden agregar nuevas películas con título, descripción, imagen y categoría.

![Alta de película](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/peliculas_altas.png?raw=true)

---

### ✏️ Edición y ❌ Eliminación

Cada película del listado incluye botones para modificar su información o eliminarla del sistema.
</br>
por ejemplo el editar se ve algo asi:

![Edición o eliminación](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/peliculas_editar.png?raw=true)

---
## 🔍🎬 Búsqueda y Filtros de peliculas
<a name="búsqueda-y-filtros-de-películas"></a>

Desde la pantalla principal (Inicio), el usuario puede:

-Buscar películas por título
-Filtrar por categoría

Estas funciones están disponibles incluso sin iniciar sesión.

![Filtros en la pantalla principal](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/filtros_peliculas.png?raw=true)


---

### 📌 Notas finales
<a name="notas-finales"></a>
✅Este proyecto fue realizado como práctica personal.

---
### 👩‍💻 Contacto
<a name="contacto"></a>

👩‍💻 Romina Olivera Luna
</br>
💌 rominalunaolivera@gmail.com
</br>
🔗 [LinkedIn
](https://www.linkedin.com/in/romina-bluna/)












