using NeoCore.Infrastructure.OutputPoint.Inyecciones;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AgregarAdaptadorPuntoSalida();
var app = builder.Build();
app.Run();


