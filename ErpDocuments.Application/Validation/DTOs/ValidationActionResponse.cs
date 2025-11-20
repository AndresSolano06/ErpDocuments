using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Validation.DTOs
{
    public class ValidationActionResponse
    {
        public Guid Id { get; set; }

        public Guid ValidationStepId { get; set; }

        public ValidationActionType ActionType { get; set; }

        public string PerformedBy { get; set; } = null!;

        public string? Comments { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
