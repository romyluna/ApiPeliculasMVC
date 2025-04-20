using Microsoft.AspNetCore.Authentication.Cookies;
using PeliculasWeb.Repositorio;
using PeliculasWeb.Repositorio.IRepositorio;
using System.Net;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//configurar cliente http : Registrar IHttpClientFactory

builder.Services.AddHttpClient();

//agregamos Autenticacion

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true; //Configura la cookie para ser accesible solo a través de HTTP
        //tiempo de expiracion
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        // Especifica la ruta donde el usuario será redirigido si intenta acceder a una página protegida sin autenticarse previamente
        options.LoginPath = "Home/Login";
        //Define la ruta a la cual se redirigirá al usuario si intenta acceder a recursos para los cuales no tiene permiso
        options.AccessDeniedPath = "Home/AccessDenied";
        //si el usuario esta activo y pasaron los 20 minutos se extiende el uso 20 minutos mas y asi... para que no lo saque de la pagina 
        options.SlidingExpiration = true;
    }

    );

//agregar los servicios como inyeccion de dependencia

builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IAccountRepositorio, AccountRepositorio>();

//agregar sesion (Autenticacion) 
//la información de la sesión se guarda en el servidor.
//El navegador solo almacena una cookie de sesión con un identificador (ID), y el servidor usa ese ID para enlazarlo con los datos específicos del usuario

builder.Services.AddSession(options => //habilita el uso de sesiones en tu aplicación
{
    options.IdleTimeout = TimeSpan.FromSeconds(30); //cuánto tiempo puede permanecer inactiva la sesión antes de que expire.
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication(); //si usas autenticacion (AUTORIZ)

app.UseRouting();


app.UseAuthorization();
app.UseSession(); //HABILITAR LA SESION (AUTORIZ)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
