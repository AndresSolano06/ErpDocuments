# 📄 ErpDocuments – Sistema de Gestión de Documentos y Flujos de Validación

Permite gestionar empresas, entidades asociadas, documentos con archivos adjuntos y flujos de validación en varios niveles.

Implementado usando **.NET 8**, **Entity Framework Core**, **SQL Server** y arquitectura **Clean Architecture**.

---

## 🏗️ Arquitectura

```
ErpDocuments.sln
 ├── ErpDocuments.Domain
 ├── ErpDocuments.Application
 ├── ErpDocuments.Infrastructure
 └── ErpDocuments.Api
```

---

## 🚀 Requisitos

- .NET 8 SDK  
- SQL Server  
- Git  

---

## 📥 Instalación y ejecución

### 1. Clonar repo

```bash
git clone https://github.com/AndresSolano06/ErpDocuments.git
cd ErpDocuments
```

### 2. Configurar connection string

Archivo: `ErpDocuments.Api/appsettings.Development.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-TFDQGKK;Database=ErpDocumentsDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Crear base de datos

```bash
dotnet ef database update --project ErpDocuments.Infrastructure --startup-project ErpDocuments.Api
```

### 4. Ejecutar API

```bash
dotnet run --project ErpDocuments.Api
```

Abrir Swagger:

```
https://localhost:7178/swagger
```

---

## 📚 Endpoints principales

### Company
- POST `/api/company`
- GET `/api/company`
- GET `/api/company/{id}`
- PUT `/api/company`
- DELETE `/api/company/{id}`

### RelatedEntity
- POST `/api/relatedentity`
- GET `/api/relatedentity/{id}`
- GET `/api/relatedentity/company/{companyId}`
- PUT `/api/relatedentity`
- DELETE `/api/relatedentity/{id}`

### Documentos
- POST `/api/document/upload` (multipart/form-data)
- GET `/api/document/{id}`
- GET `/api/document/company/{companyId}`
- PUT `/api/document`
- DELETE `/api/document/{id}`

### Validation Flow
- POST `/api/validationflow`
- GET `/api/validationflow/{id}`
- GET `/api/validationflow/document/{documentId}`

### Validation Step
- POST `/api/validationstep`
- GET `/api/validationstep/flow/{flowId}`

### Validation Action
- POST `/api/validationaction`

---

## 🔄 Flujo completo sugerido para pruebas

1. Crear empresa  
2. Crear entidad asociada  
3. Subir documento con archivo  
4. Crear flujo de validación  
5. Registrar pasos del flujo  
6. Registrar acciones: aprobar / rechazar / comentar  
7. Consultar estado final del flujo y pasos  

---

## 🛠️ Tecnologías usadas

- ASP.NET Web API  
- EF Core  
- SQL Server  
- Swagger  
- Clean Architecture  

---

## 👤 Autor

**Andrés Solano**  
Backend Developer
