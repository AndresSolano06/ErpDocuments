using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Documents.DTOs
{
    public class DocumentSummaryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DocumentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
