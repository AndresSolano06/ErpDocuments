using ErpDocuments.Application.Companies.DTOs;
using ErpDocuments.Application.Companies.Interfaces;
using ErpDocuments.Domain.Entities;
using ErpDocuments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ErpDocuments.Infrastructure.Services.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;

        public CompanyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyDetailResponse> CreateAsync(CreateCompanyRequest request, CancellationToken cancellationToken = default)
        {
            var company = new Company
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync(cancellationToken);

            return new CompanyDetailResponse
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            };
        }

        public async Task<CompanyDetailResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (company == null)
                return null;

            return new CompanyDetailResponse
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            };
        }

        public async Task<IEnumerable<CompanySummaryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Companies
                .Select(x => new CompanySummaryResponse
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<CompanyDetailResponse?> UpdateAsync(UpdateCompanyRequest request, CancellationToken cancellationToken = default)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (company == null)
                return null;

            company.Name = request.Name;
            company.Description = request.Description;
            company.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new CompanyDetailResponse
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            };
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (company == null)
                return false;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
