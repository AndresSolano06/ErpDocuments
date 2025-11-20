using System;

namespace ErpDocuments.Application.Documents.DTOs
{
    public class UpdateDocumentRequest
    {
        public Guid Id { get; set; }

        public Guid? RelatedEntityId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
