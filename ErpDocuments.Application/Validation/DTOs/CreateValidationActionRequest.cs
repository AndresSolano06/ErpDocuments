using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Validation.DTOs
{
    public class CreateValidationActionRequest
    {
        public Guid ValidationStepId { get; set; }

        public ValidationActionType ActionType { get; set; }

        public string PerformedBy { get; set; } = null!;

        public string? Comments { get; set; }
    }
}
