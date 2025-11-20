using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Domain.Entities
{
    public class ValidationFlow
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid DocumentId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ValidationFlowStatus Status { get; set; } = ValidationFlowStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
