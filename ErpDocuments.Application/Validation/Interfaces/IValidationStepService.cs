using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ErpDocuments.Application.Validation.DTOs;

namespace ErpDocuments.Application.Validation.Interfaces
{
    public interface IValidationStepService
    {
        Task<ValidationStepResponse> CreateStepAsync(CreateValidationStepRequest request, CancellationToken cancellationToken = default);

        Task<IEnumerable<ValidationStepResponse>> GetStepsByFlowAsync(Guid flowId, CancellationToken cancellationToken = default);
    }
}
