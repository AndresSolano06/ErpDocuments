using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErpDocuments.Application.Documents.DTOs;

namespace ErpDocuments.Application.Documents.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentDetailResponse> CreateAsync(
            CreateDocumentRequest request,
            string filePath,
            CancellationToken cancellationToken = default);

        Task<DocumentDetailResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<DocumentDetailResponse>> GetByCompanyAsync(
            Guid companyId,
            CancellationToken cancellationToken = default);

        Task<DocumentDetailResponse?> UpdateAsync(
            UpdateDocumentRequest request,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}
