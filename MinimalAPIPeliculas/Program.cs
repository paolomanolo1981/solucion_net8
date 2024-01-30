using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPIPeliculas.endpoints;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;

var builder = WebApplication.CreateBuilder(args);
//var apellido = builder.Configuration.GetValue<string>("apellidos");
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;

//----------------------------------------------------inicio del �rea de los servicios

//=>Inicio habilitaci�n de CORS

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });

    //otra politica
    opciones.AddPolicy("libre", configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});


//=> fin habilitaci�n de CORS

//cach�
builder.Services.AddOutputCache();
//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositorioGeneros, RepositorioGeneros>();
builder.Services.AddScoped<IRepositorioActores, RepositorioActores>();


builder.Services.AddAutoMapper(typeof(Program));
//---------------------------------------------fin del �rea de los servicios

//inicio de middleware
var app = builder.Build();


//uso de swagger para producci�n o desarrollo
if (builder.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//utilizar las pol�ticas de CORS
app.UseCors();
app.UseOutputCache();


app.MapGet("/",[EnableCors(policyName:"libre")]  () => "hola mundo");

//app.MapGet("/otra-cosa", () =>
//{
//   return "Otra cosa";
//});

app.MapGroup("/generos").MapGeneros();
app.MapGroup("/actores").MapActores();




app.Run();
//fin de �rea de middleware



