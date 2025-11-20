using System;

namespace ErpDocuments.Application.RelatedEntities.DTOs
{
    public class CreateRelatedEntityRequest
    {
        public Guid CompanyId { get; set; }

        public string Name { get; set; } = null!;

        public string? Type { get; set; }
    }
}
