using System;

namespace ErpDocuments.Application.Companies.DTOs
{
    public class CompanySummaryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
