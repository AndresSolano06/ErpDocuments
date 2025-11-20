using System;

namespace ErpDocuments.Application.Documents.DTOs
{
    public class CreateDocumentRequest
    {
        public Guid CompanyId { get; set; }

        public Guid? RelatedEntityId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string FileName { get; set; } = null!;
    }
}
