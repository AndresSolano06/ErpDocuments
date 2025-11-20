using ErpDocuments.Infrastructure.Persistence;
using ErpDocuments.Application.Documents.Interfaces;
using ErpDocuments.Infrastructure.Services.Documents;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de EF Core y DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Servicios de aplicación
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Configuración del pipeline HTTP

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Archivos estáticos (para servir los uploads)
app.UseStaticFiles();

app.MapControllers();

app.Run();
