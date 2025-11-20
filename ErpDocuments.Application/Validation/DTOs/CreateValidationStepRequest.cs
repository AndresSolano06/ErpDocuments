using System;

namespace ErpDocuments.Application.Validation.DTOs
{
    public class CreateValidationStepRequest
    {
        public Guid ValidationFlowId { get; set; }

        public int Order { get; set; }

        public string Name { get; set; } = null!;

        public string ApproverRoleOrUser { get; set; } = null!;
    }
}
