using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo{

        Title="Practica 6",
        Version="v1",
        Description="Mi primera appi con swagger"

    });
});

var app = builder.Build();


if(app.Environment.IsDevelopment()){

    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Practica 6 v1");
        c.RoutePrefix=string.Empty;
    });
}

var Noticias= app.MapGroup("Paso 1").WithTags("Noticias de migracion").WithDescription("Ofrece noticias de migracion necesarias para la practica");

Noticias.MapGet("/Noticia", () => Noticia.Ejecutar());

var Sesion= app.MapGroup("Gestion").WithTags("Registro e inicio de sesion").WithDescription("Permiten registrar incidencias o usuarios, e iniciar sesion");

Sesion.MapPost("/Registro",(usuario user)=>gestion.registro(user)).WithDescription("Permite el registro de usuarios");
Sesion.MapPost("/Sesion",(credencial cred)=>gestion.login(cred)).WithDescription("Permite a los usuarios ya regstrados iniciar sesion");
Sesion.MapPost("/Incidentes",(incidencia inc)=>gestion.reggistroIncidencia(inc)).WithDescription("Permite registrar incidentes");

var climas= app.MapGroup("clima").WithTags("Estado del clima").WithDescription("Permite visualizar el estado del clima en base a unas coodenadas");

climas.MapPost("/Clima",(clima.coordenada coor)=> clima.obtenerclima(coor)).WithDescription("Muestra la informacion climatica de las coordenadas especificadas");



app.Run();
