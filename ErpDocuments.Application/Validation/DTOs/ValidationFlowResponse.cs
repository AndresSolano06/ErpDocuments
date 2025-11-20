using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Validation.DTOs
{
    public class ValidationFlowResponse
    {
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ValidationFlowStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
