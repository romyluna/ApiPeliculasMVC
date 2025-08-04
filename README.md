# 🎬 App de Películas (.NET + API REST)

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
➕ Alta de nuevas películas manualmente
✏️ Edición de películas
❌ Eliminación de películas
🔎 Búsqueda por título

---

## 🧰 Tecnologías utilizadas

- ASP.NET Core MVC
- C#
- Entity Framework
- API REST (pública)
- Bootstrap
- HTML/CSS
- JavaScript (básico)

---

## 🚀 Cómo ejecutar el proyecto

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

![Pantalla principal](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/pantalla_principal1.PNG?raw=true)

### 🔐 Pantalla de Login

Pantalla de registro para un usuario nuevo:

![Login](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/registroNuevo_usuario.png?raw=true)

Pantalla de acceso para un usuario ya registrado:

![Login](https://github.com/romyluna/ApiPeliculasMVC/blob/master/screenshots/acceso_usuarios.png?raw=true)



---
📚 Notas:


Este proyecto fue realizado como práctica personal.

La API utilizada puede ser reemplazada por otra si se desea extender la funcionalidad.

Ideal para quienes están aprendiendo consumo de APIs y lógica de ABM con .NET.

Romina Olivera Luna
</br>
💌 rominalunaolivera@gmail.com
</br>
🔗 [LinkedIn
](https://www.linkedin.com/in/romina-bluna/)












