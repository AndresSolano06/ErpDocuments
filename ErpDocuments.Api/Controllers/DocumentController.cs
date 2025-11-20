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
        private readonly IWebHostEnvironment _env;

        public DocumentController(IDocumentService documentService, IWebHostEnvironment env)
        {
            _documentService = documentService;
            _env = env;
        }

        // -----------------------------------------
        // 1) Crear documento con archivo
        // -----------------------------------------
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] CreateDocumentRequest request, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Se requiere un archivo.");

            // Crear directorio si no existe
            var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            // Guardar archivo físico
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Guardar documento en base de datos usando el servicio
            var result = await _documentService.CreateAsync(request, $"/uploads/{fileName}");

            return Ok(result);
        }

        // -----------------------------------------
        // 2) Obtener documento por Id
        // -----------------------------------------
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var document = await _documentService.GetByIdAsync(id);

            if (document == null)
                return NotFound();

            return Ok(document);
        }

        // -----------------------------------------
        // 3) Lista de documentos por empresa
        // -----------------------------------------
        [HttpGet("company/{companyId:guid}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var documents = await _documentService.GetByCompanyAsync(companyId);
            return Ok(documents);
        }

        // -----------------------------------------
        // 4) Actualizar documento
        // -----------------------------------------
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDocumentRequest request)
        {
            var document = await _documentService.UpdateAsync(request);

            if (document == null)
                return NotFound();

            return Ok(document);
        }

        // -----------------------------------------
        // 5) Eliminar documento
        // -----------------------------------------
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _documentService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
