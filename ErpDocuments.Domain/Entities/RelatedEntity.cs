using System;

namespace ErpDocuments.Domain.Entities
{
    public class RelatedEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CompanyId { get; set; }

        public string Name { get; set; } = null!;

        public string? Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
