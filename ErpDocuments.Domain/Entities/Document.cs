using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CompanyId { get; set; }

        public Guid? RelatedEntityId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string FilePath { get; set; } = null!;

        public DocumentType Type { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
