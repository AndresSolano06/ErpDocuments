using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Documents.DTOs
{
    public class DocumentDetailResponse
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid? RelatedEntityId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string FilePath { get; set; } = null!;

        public DocumentType Type { get; set; }

        public DocumentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
