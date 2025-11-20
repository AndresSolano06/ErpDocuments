using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Domain.Entities
{
    public class ValidationAction
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ValidationStepId { get; set; }

        public ValidationActionType ActionType { get; set; }

        public string PerformedBy { get; set; } = null!; // Id o username del usuario

        public string? Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
