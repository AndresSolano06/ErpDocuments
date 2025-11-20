using System;

namespace ErpDocuments.Application.Validation.DTOs
{
    public class CreateValidationFlowRequest
    {
        public Guid DocumentId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        // Cantidad de pasos que tendrá el flujo
        public int StepCount { get; set; }
    }
}
