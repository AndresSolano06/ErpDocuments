using ErpDocuments.Application.Validation.DTOs;
using ErpDocuments.Application.Validation.Interfaces;
using ErpDocuments.Domain.Entities;
using ErpDocuments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ErpDocuments.Infrastructure.Services.Validation
{
    public class ValidationStepService : IValidationStepService
    {
        private readonly AppDbContext _context;

        public ValidationStepService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationStepResponse> CreateStepAsync(
            CreateValidationStepRequest request,
            CancellationToken cancellationToken = default)
        {
            var step = new ValidationStep
            {
                ValidationFlowId = request.ValidationFlowId,
                Order = request.Order,
                Name = request.Name,
                ApproverRoleOrUser = request.ApproverRoleOrUser
            };

            _context.ValidationSteps.Add(step);
            await _context.SaveChangesAsync(cancellationToken);

            return new ValidationStepResponse
            {
                Id = step.Id,
                ValidationFlowId = step.ValidationFlowId,
                Order = step.Order,
                Name = step.Name,
                ApproverRoleOrUser = step.ApproverRoleOrUser,
                Status = step.Status
            };
        }

        public async Task<IEnumerable<ValidationStepResponse>> GetStepsByFlowAsync(Guid flowId, CancellationToken cancellationToken = default)
        {
            return await _context.ValidationSteps
                .Where(x => x.ValidationFlowId == flowId)
                .OrderBy(x => x.Order)
                .Select(x => new ValidationStepResponse
                {
                    Id = x.Id,
                    ValidationFlowId = x.ValidationFlowId,
                    Order = x.Order,
                    Name = x.Name,
                    ApproverRoleOrUser = x.ApproverRoleOrUser,
                    Status = x.Status,
                    CompletedAt = x.CompletedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
