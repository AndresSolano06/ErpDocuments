using ErpDocuments.Infrastructure.Persistence;
using ErpDocuments.Application.Documents.Interfaces;
using ErpDocuments.Infrastructure.Services.Documents;
using Microsoft.EntityFrameworkCore;
using ErpDocuments.Application.Companies.Interfaces;
using ErpDocuments.Application.RelatedEntities.Interfaces;
using ErpDocuments.Infrastructure.Services.Companies;
using ErpDocuments.Infrastructure.Services.RelatedEntities;
using ErpDocuments.Application.Validation.Interfaces;
using ErpDocuments.Infrastructure.Services.Validation;
using Amazon.S3;
using ErpDocuments.Infrastructure.Services.Storage;


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
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IRelatedEntityService, RelatedEntityService>();
builder.Services.AddScoped<IValidationFlowService, ValidationFlowService>();
builder.Services.AddScoped<IValidationStepService, ValidationStepService>();
builder.Services.AddScoped<IValidationActionService, ValidationActionService>();
builder.Services.AddScoped<IFileStorageService, S3FileStorageService>();
// AWS & S3
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions("AWS"));
builder.Services.AddAWSService<IAmazonS3>();

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
