using CineAPI.Repository;
using CineAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde el archivo de configuración
var connectionString = builder.Configuration.GetConnectionString("CineDB");

// Registrar los repositorios con la cadena de conexión
builder.Services.AddScoped<IPeliculaRepository>(provider =>
    new PeliculaRepository(connectionString));  

// Registrar los servicios
builder.Services.AddScoped<IPeliculaService, PeliculaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();