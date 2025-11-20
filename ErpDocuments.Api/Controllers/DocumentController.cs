using ErpDocuments.Application.Documents.DTOs;
using ErpDocuments.Application.Documents.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErpDocuments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IFileStorageService _fileStorageService;

        public DocumentController(
            IDocumentService documentService,
            IFileStorageService fileStorageService)
        {
            _documentService = documentService;
            _fileStorageService = fileStorageService;
        }

        // -----------------------------------------
        // 1) Crear documento subiendo archivo a S3
        // -----------------------------------------
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] CreateDocumentRequest request,
            IFormFile file,
            CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Se requiere un archivo.");

            // 1. Subir el archivo a S3 dentro de la carpeta "documents"
            await using var stream = file.OpenReadStream();

            var key = await _fileStorageService.UploadAsync(
                stream,              // Stream (primer parámetro)
                file.FileName,       // Nombre original del archivo
                "documents",         // Carpeta dentro del bucket
                cancellationToken);  // CancellationToken

            // 2. Guardar el documento en base de datos usando la ruta/key de S3
            var result = await _documentService.CreateAsync(
                request,
                key,
                cancellationToken);

            return Ok(result);
        }

        // -----------------------------------------
        // 2) Obtener documento por Id
        // -----------------------------------------
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var document = await _documentService.GetByIdAsync(id, cancellationToken);

            if (document == null)
                return NotFound();

            return Ok(document);
        }

        // -----------------------------------------
        // 3) Lista de documentos por empresa
        // -----------------------------------------
        [HttpGet("company/{companyId:guid}")]
        public async Task<IActionResult> GetByCompany(Guid companyId, CancellationToken cancellationToken)
        {
            var documents = await _documentService.GetByCompanyAsync(companyId, cancellationToken);
            return Ok(documents);
        }

        // -----------------------------------------
        // 4) Actualizar documento
        // -----------------------------------------
        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateDocumentRequest request,
            CancellationToken cancellationToken)
        {
            var document = await _documentService.UpdateAsync(request, cancellationToken);

            if (document == null)
                return NotFound();

            return Ok(document);
        }

        // -----------------------------------------
        // 5) Eliminar documento
        // -----------------------------------------
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _documentService.DeleteAsync(id, cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
