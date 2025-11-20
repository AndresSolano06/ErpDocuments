# ErpDocuments API – Document Management & Validation Workflow

Este proyecto es la **prueba técnica backend** implementada con **.NET 8** para la gestión de documentos y un flujo de validación asociado.  
Expone una API REST que permite:

- Gestionar **empresas** (Company).
- Cargar y administrar **documentos** asociados a una empresa.
- Gestionar **entidades relacionadas** a un documento (RelatedEntity).
- Definir **flujos de validación** (ValidationFlow) y sus **pasos** (ValidationStep).
- Registrar **acciones de validación** (ValidationAction) sobre cada paso.
- Almacenar archivos de documentos en **Amazon S3**.

---

## Arquitectura del proyecto

Solución compuesta por 4 proyectos:

- **ErpDocuments.Api**  
  - Proyecto Web API (.NET 8).  
  - Contiene los controladores y configuración de Swagger.

- **ErpDocuments.Application**  
  - Capa de aplicación.  
  - DTOs, interfaces de servicios y lógica de negocio de alto nivel.

- **ErpDocuments.Infrastructure**  
  - Capa de infraestructura.  
  - Implementaciones de servicios (EF Core, S3, etc.).  
  - Migrations de Entity Framework Core.

- **ErpDocuments.Domain**  
  - Entidades de dominio y enums (`DocumentStatus`, `DocumentType`, `ValidationFlowStatus`, etc.).

Patrones usados:

- Separación de capas (Api / Application / Infrastructure / Domain).
- Inyección de dependencias para servicios e infraestructura.
- Entity Framework Core como ORM.
- Almacenamiento de archivos en S3 a través de un servicio `IFileStorageService`.

---

## Requisitos

- **.NET 8 SDK**
- **SQL Server** (local o remoto)
- **Amazon Web Services**:
  - Cuenta de AWS
  - Un bucket S3 (por ejemplo: `erpdocuments-dev-bucket`)
- Herramienta `dotnet-ef` instalada globalmente:

```bash
dotnet tool install --global dotnet-ef
```

---

## Configuración

### 1. Clonar el repositorio

```bash
git clone <URL_DEL_REPO>
cd <CARPETA_DEL_REPO>
```

### 2. Configurar la cadena de conexión

En `ErpDocuments.Api/appsettings.Development.json` (o `appsettings.json`), configurar la cadena de conexión:

```jsonc
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ErpDocumentsDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Ajusta el `Server` y las credenciales según tu entorno.

### 3. Configurar AWS S3

En el mismo archivo de configuración agrega la sección (o ajusta la existente):

```jsonc
"AwsOptions": {
  "AccessKey": "TU_ACCESS_KEY",
  "SecretKey": "TU_SECRET_KEY",
  "Region": "us-east-1",
  "BucketName": "erpdocuments-dev-bucket"
}
```

> ⚠️ **No subas tus claves reales a Git.** Usa `appsettings.Development.json` ignorado por git o variables de entorno.

---

## Migraciones y base de datos

Las migraciones se encuentran en **ErpDocuments.Infrastructure**.

Desde la raíz de la solución:

```bash
dotnet ef database update   --startup-project ErpDocuments.Api   --project ErpDocuments.Infrastructure
```

Esto creará (o actualizará) la base de datos `ErpDocumentsDb` con todas las tablas necesarias.

---

## Ejecución del proyecto

```bash
cd ErpDocuments.Api
dotnet run
```

Por defecto Swagger quedará disponible en:

- `https://localhost:7178/swagger` (o el puerto que asigne Kestrel).

---

## Endpoints principales

A continuación un resumen de los grupos de endpoints y ejemplos de bodies.  
Todos los cuerpos están en **JSON**.

### 1. Company

**Crear empresa** – `POST /api/Company`

```json
{
  "name": "Mi Primera Empresa",
  "description": "Empresa de prueba para la técnica"
}
```

**Obtener todas las empresas** – `GET /api/Company`  
**Obtener empresa por Id** – `GET /api/Company/{id}`  
**Actualizar empresa** – `PUT /api/Company`

```json
{
  "id": "06354B94-B220-4144-A647-DB451E32BF89",
  "name": "Empresa actualizada",
  "description": "Descripción actualizada"
}
```

**Eliminar empresa** – `DELETE /api/Company/{id}`

---

### 2. Document

Los documentos se crean subiendo un archivo y asociándolo a una empresa y (opcionalmente) a una entidad relacionada.  
El archivo se almacena en S3 y la ruta se guarda en la base de datos.

**Subir documento** – `POST /api/Document/upload`

- Tipo: `multipart/form-data`
- Partes:
  - `file`: archivo (PDF, imagen, etc.)
  - Resto de campos en el body de tipo formulario:

```json
{
  "companyId": "06354B94-B220-4144-A647-DB451E32BF89",
  "relatedEntityId": "BEDFCB71-A850-4746-8A71-7906782AB1C9",
  "name": "Factura de compra",
  "description": "Factura a legalizar para la compra del pc"
}
```

**Obtener documento por Id** – `GET /api/Document/{id}`  
**Obtener documentos por empresa** – `GET /api/Document/company/{companyId}`  
**Actualizar documento** – `PUT /api/Document`

```json
{
  "id": "709660F1-EA2E-46AE-9149-3042254ADD45",
  "name": "Factura actualizada",
  "description": "Descripción actualizada del documento"
}
```

**Eliminar documento** – `DELETE /api/Document/{id}`

---

### 3. RelatedEntity

Entidad auxiliar para asociar un documento a otro elemento de negocio (cliente, contrato, proyecto, etc).

**Crear entidad relacionada** – `POST /api/RelatedEntity`

```json
{
  "companyId": "06354B94-B220-4144-A647-DB451E32BF89",
  "name": "Proyecto X",
  "description": "Proyecto de ejemplo asociado a la empresa"
}
```

**Obtener todas por empresa** – `GET /api/RelatedEntity/company/{companyId}`  
**Obtener por Id** – `GET /api/RelatedEntity/{id}`  
**Actualizar** – `PUT /api/RelatedEntity`  
**Eliminar** – `DELETE /api/RelatedEntity/{id}`

---

### 4. ValidationFlow

Un **ValidationFlow** representa el flujo de aprobación/validación de un documento.

**Crear flujo** – `POST /api/ValidationFlow`

```json
{
  "documentId": "709660F1-EA2E-46AE-9149-3042254ADD45",
  "name": "Flujo de aprobación estándar",
  "description": "Flujo por defecto para facturas"
}
```

**Obtener flujo por Id** – `GET /api/ValidationFlow/{id}`  
**Obtener flujo por documento** – `GET /api/ValidationFlow/document/{documentId}`

---

### 5. ValidationStep

Cada flujo se compone de uno o varios **ValidationStep** que definen los pasos y responsables.

**Crear paso** – `POST /api/ValidationStep`

```json
{
  "validationFlowId": "3103A514-3550-43E9-B7B3-ABF2DA49D221",
  "order": 1,
  "name": "Revisión contabilidad",
  "approverRoleOrUser": "Admin"
}
```

**Obtener pasos de un flujo** – `GET /api/ValidationStep/flow/{flowId}`

---

### 6. ValidationAction

Registra la acción que un usuario realiza sobre un paso (aprobar/rechazar, comentarios, etc.).

**Registrar acción** – `POST /api/ValidationAction`

```json
{
  "validationStepId": "3FA8F564-5717-4562-B3FC-2C963F66AFA6",
  "actionType": 1,          // por ejemplo: 1 = Aprobado, 2 = Rechazado
  "performedBy": "user@demo.com",
  "comments": "Acción de ejemplo"
}
```

---

## Flujo de uso recomendado

1. **Crear empresa** (`POST /api/Company`).
2. **Crear entidad relacionada** si aplica (`POST /api/RelatedEntity`).
3. **Subir documento** para la empresa (`POST /api/Document/upload`).
4. **Crear flujo de validación** para el documento (`POST /api/ValidationFlow`).
5. **Crear los pasos de validación** del flujo (`POST /api/ValidationStep`).
6. A medida que los responsables actúan, **registrar acciones** sobre cada paso (`POST /api/ValidationAction`).
7. Consultar el documento, su flujo y acciones para ver el estado completo de la validación.

---

## Ejecución de pruebas manuales

La forma más sencilla de probar el sistema es usando **Swagger UI**:

1. Levanta el proyecto con `dotnet run` en `ErpDocuments.Api`.
2. Abre el navegador en `https://localhost:7178/swagger`.
3. Ejecuta los endpoints en el orden sugerido en la sección de **Flujo de uso recomendado**.
4. Verifica en SQL Server las tablas:
   - `Companies`
   - `Documents`
   - `RelatedEntities`
   - `ValidationFlows`
   - `ValidationSteps`
   - `ValidationActions`

---

## Notas finales

- El proyecto está preparado para extender la lógica de validación, agregar estados adicionales y nuevas entidades relacionadas.
- El almacenamiento en S3 está encapsulado en un servicio, por lo que se puede sustituir por otro proveedor de almacenamiento si fuese necesario.

##Usuario para S3
- https://205662215140.signin.aws.amazon.com/console
- User: erpdocuments-reviewer
- Pass: ErpDocs2025!
