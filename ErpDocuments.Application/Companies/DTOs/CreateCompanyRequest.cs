namespace ErpDocuments.Application.Companies.DTOs
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
