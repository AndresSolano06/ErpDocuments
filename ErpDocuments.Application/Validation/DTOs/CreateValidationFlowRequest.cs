using System;

namespace ErpDocuments.Application.Validation.DTOs
{
    public class CreateValidationFlowRequest
    {
        public Guid DocumentId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int StepCount { get; set; }
    }
}
