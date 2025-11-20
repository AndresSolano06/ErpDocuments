using System;

namespace ErpDocuments.Application.RelatedEntities.DTOs
{
    public class UpdateRelatedEntityRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Type { get; set; }
    }
}
