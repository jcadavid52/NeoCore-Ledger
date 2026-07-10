using NeoCore.Infrastructure.OutputPoint.Inyecciones;
using NeoCore.Infrastructure.EntryPoint.Inyecciones;
using NeoCore.Application.Inyecciones;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AgregarAdaptadorPuntoSalida(builder.Configuration);
builder.Services.AgregarRestInyeccion();
builder.Services.AgregarApplicationInyeccion();
var app = builder.Build();
app.MapControllers();
app.Run();
