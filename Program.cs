var builder = WebApplication.CreateBuilder(args);

// Configurar el puerto
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001);
});

// Agregar servicios al contenedor
builder.Services.AddControllers(); // Los controladores se registran automáticamente
builder.Services.AddScoped<TicketsController>();
builder.Services.AddScoped<SalasController>();

// Configurar autorización
builder.Services.AddAuthorization();

// Habilitar Swagger para la documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();