using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErpDocuments.Application.Companies.DTOs;

namespace ErpDocuments.Application.Companies.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyDetailResponse> CreateAsync(CreateCompanyRequest request, CancellationToken cancellationToken = default);

        Task<CompanyDetailResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<CompanySummaryResponse>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<CompanyDetailResponse?> UpdateAsync(UpdateCompanyRequest request, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
