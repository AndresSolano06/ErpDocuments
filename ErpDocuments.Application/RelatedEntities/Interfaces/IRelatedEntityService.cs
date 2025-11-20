using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErpDocuments.Application.RelatedEntities.DTOs;

namespace ErpDocuments.Application.RelatedEntities.Interfaces
{
    public interface IRelatedEntityService
    {
        Task<RelatedEntityResponse> CreateAsync(CreateRelatedEntityRequest request, CancellationToken cancellationToken = default);

        Task<RelatedEntityResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<RelatedEntityResponse>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        Task<RelatedEntityResponse?> UpdateAsync(UpdateRelatedEntityRequest request, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
