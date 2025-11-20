using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErpDocuments.Application.Validation.DTOs;

namespace ErpDocuments.Application.Validation.Interfaces
{
    public interface IValidationFlowService
    {
        Task<ValidationFlowResponse> CreateFlowAsync(CreateValidationFlowRequest request, CancellationToken cancellationToken = default);

        Task<ValidationFlowResponse?> GetFlowAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<ValidationFlowResponse>> GetFlowsByDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
    }
}
