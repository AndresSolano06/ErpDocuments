using System;
using System.Threading;
using System.Threading.Tasks;
using ErpDocuments.Application.Validation.DTOs;

namespace ErpDocuments.Application.Validation.Interfaces
{
    public interface IValidationActionService
    {
        Task<ValidationActionResponse> RegisterActionAsync(CreateValidationActionRequest request, CancellationToken cancellationToken = default);
    }
}
