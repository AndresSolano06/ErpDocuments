using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Application.Validation.DTOs
{
    public class ValidationStepResponse
    {
        public Guid Id { get; set; }

        public Guid ValidationFlowId { get; set; }

        public int Order { get; set; }

        public string Name { get; set; } = null!;

        public string ApproverRoleOrUser { get; set; } = null!;

        public ValidationStepStatus Status { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
