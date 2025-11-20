using System;
using ErpDocuments.Domain.Enums;
using ErpDocuments.Domain.Entities;

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

        public DocumentDetailResponse(Document document)
        {
            Id = document.Id;
            CompanyId = document.CompanyId;
            RelatedEntityId = document.RelatedEntityId;
            Name = document.Name;
            Description = document.Description;
            FilePath = document.FilePath;
            Type = document.Type;
            Status = document.Status;
            CreatedAt = document.CreatedAt;
            UpdatedAt = document.UpdatedAt;
        }

        public DocumentDetailResponse()
        {
        }
    }
}
