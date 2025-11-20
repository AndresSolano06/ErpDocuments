using System;
using Microsoft.AspNetCore.Http;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Documents.DTOs
{
    public class CreateDocumentRequest
    {
        public Guid CompanyId { get; set; }
        public Guid? RelatedEntityId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public DocumentType Type { get; set; }
        public IFormFile? File { get; set; }

    }
}
