using NeoCore.Application.Inyecciones;
using NeoCore.Infrastructure.EntryPoint.Inyecciones;
using NeoCore.Infrastructure.OutputPoint.Inyecciones;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AgregarAdaptadorPuntoSalida(builder.Configuration);
builder.Services.AgregarRestInyeccion();
builder.Services.AgregarApplicationInyeccion();
var app = builder.Build();
app.UseExceptionHandler();
app.MapControllers();
app.Run();
