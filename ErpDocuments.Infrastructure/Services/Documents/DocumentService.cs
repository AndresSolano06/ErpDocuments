using ErpDocuments.Application.Documents.DTOs;
using ErpDocuments.Application.Documents.Interfaces;
using ErpDocuments.Domain.Entities;
using ErpDocuments.Domain.Enums;
using ErpDocuments.Infrastructure.Persistence;

namespace ErpDocuments.Infrastructure.Services.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public DocumentService(AppDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<DocumentDetailResponse> CreateAsync(
            CreateDocumentRequest request,
            string? ignored,
            CancellationToken cancellationToken = default)
        {
            // 1. Subir archivo a S3
            string key;

            using (var stream = request.File.OpenReadStream())
            {
                key = await _fileStorageService.UploadAsync(
                    stream,
                    request.File.FileName,
                    "documents",
                    cancellationToken
                );
            }

            // 2. Crear entidad Document
            var document = new Document
            {
                CompanyId = request.CompanyId,
                RelatedEntityId = request.RelatedEntityId,
                Name = request.Name,
                Description = request.Description,
                FilePath = key,
                Type = DocumentType.Unknown,
                Status = DocumentStatus.PendingValidation,
                CreatedAt = DateTime.UtcNow
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync(cancellationToken);

            return new DocumentDetailResponse(document);
        }

        public async Task<DocumentDetailResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doc = await _context.Documents.FindAsync(new object?[] { id }, cancellationToken);
            return doc == null ? null : new DocumentDetailResponse(doc);
        }

        public async Task<IReadOnlyList<DocumentDetailResponse>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return _context.Documents
                .Where(x => x.CompanyId == companyId)
                .Select(x => new DocumentDetailResponse(x))
                .ToList();
        }

        public async Task<DocumentDetailResponse?> UpdateAsync(UpdateDocumentRequest request, CancellationToken cancellationToken = default)
        {
            var document = await _context.Documents.FindAsync(new object?[] { request.Id }, cancellationToken);
            if (document == null) return null;

            document.Name = request.Name;
            document.Description = request.Description;
            document.Status = request.Status;
            document.Type = request.Type;
            document.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return new DocumentDetailResponse(document);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var document = await _context.Documents.FindAsync(new object?[] { id }, cancellationToken);
            if (document == null) return false;

            await _fileStorageService.DeleteAsync(document.FilePath, cancellationToken);

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
