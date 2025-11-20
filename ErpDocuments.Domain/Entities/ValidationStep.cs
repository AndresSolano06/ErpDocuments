using System;
using ErpDocuments.Domain.Enums;

namespace ErpDocuments.Domain.Entities
{
    public class ValidationStep
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ValidationFlowId { get; set; }

        public int Order { get; set; }

        public string Name { get; set; } = null!;

        // Puede ser rol, grupo o usuario específico
        public string ApproverRoleOrUser { get; set; } = null!;

        public ValidationStepStatus Status { get; set; } = ValidationStepStatus.Pending;

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
