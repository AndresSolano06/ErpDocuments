using ErpDocuments.Application.Documents.DTOs;
using ErpDocuments.Application.Documents.Interfaces;
using ErpDocuments.Domain.Entities;
using ErpDocuments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ErpDocuments.Infrastructure.Services.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentDetailResponse> CreateAsync(CreateDocumentRequest request, string filePath, CancellationToken cancellationToken = default)
        {
            var document = new Document
            {
                CompanyId = request.CompanyId,
                RelatedEntityId = request.RelatedEntityId,
                Name = request.Name,
                Description = request.Description,
                FilePath = filePath,
                Type = Domain.Enums.DocumentType.Unknown
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync(cancellationToken);

            return new DocumentDetailResponse
            {
                Id = document.Id,
                CompanyId = document.CompanyId,
                RelatedEntityId = document.RelatedEntityId,
                Name = document.Name,
                Description = document.Description,
                FilePath = document.FilePath,
                Type = document.Type,
                Status = document.Status,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };
        }

        public async Task<DocumentDetailResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (document == null)
                return null;

            return new DocumentDetailResponse
            {
                Id = document.Id,
                CompanyId = document.CompanyId,
                RelatedEntityId = document.RelatedEntityId,
                Name = document.Name,
                Description = document.Description,
                FilePath = document.FilePath,
                Type = document.Type,
                Status = document.Status,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };
        }

        public async Task<IEnumerable<DocumentSummaryResponse>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Documents
                .Where(x => x.CompanyId == companyId)
                .Select(x => new DocumentSummaryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<DocumentDetailResponse?> UpdateAsync(UpdateDocumentRequest request, CancellationToken cancellationToken = default)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (document == null)
                return null;

            document.Name = request.Name;
            document.Description = request.Description;
            document.RelatedEntityId = request.RelatedEntityId;
            document.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new DocumentDetailResponse
            {
                Id = document.Id,
                CompanyId = document.CompanyId,
                RelatedEntityId = document.RelatedEntityId,
                Name = document.Name,
                Description = document.Description,
                FilePath = document.FilePath,
                Type = document.Type,
                Status = document.Status,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var document = await _context.Documents
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (document == null)
                return false;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
