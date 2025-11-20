using System;

namespace ErpDocuments.Application.Companies.DTOs
{
    public class UpdateCompanyRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
