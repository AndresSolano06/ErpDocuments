using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Documents.DTOs
{
    public class UpdateDocumentRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        // Si permites cambiar la ruta del archivo en una actualización:
        public string FilePath { get; set; } = null!;

        // 👇 Propiedades que faltaban
        public DocumentStatus Status { get; set; }
        public DocumentType Type { get; set; }
    }
}
