using ErpDocuments.Application.Validation.DTOs;
using ErpDocuments.Application.Validation.Interfaces;
using ErpDocuments.Domain.Entities;
using ErpDocuments.Domain.Enums;
using ErpDocuments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ErpDocuments.Infrastructure.Services.Validation
{
    public class ValidationActionService : IValidationActionService
    {
        private readonly AppDbContext _context;

        public ValidationActionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationActionResponse> RegisterActionAsync(CreateValidationActionRequest request, CancellationToken cancellationToken = default)
        {
            var step = await _context.ValidationSteps
                .FirstOrDefaultAsync(x => x.Id == request.ValidationStepId, cancellationToken);

            if (step == null)
                throw new Exception("Validation step not found.");

            var action = new ValidationAction
            {
                ValidationStepId = request.ValidationStepId,
                ActionType = request.ActionType,
                PerformedBy = request.PerformedBy,
                Comments = request.Comments
            };

            _context.ValidationActions.Add(action);

            // Actualizar estado del paso
            if (request.ActionType == ValidationActionType.Approve)
            {
                step.Status = ValidationStepStatus.Approved;
                step.CompletedAt = DateTime.UtcNow;
            }
            else if (request.ActionType == ValidationActionType.Reject)
            {
                step.Status = ValidationStepStatus.Rejected;
                step.CompletedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new ValidationActionResponse
            {
                Id = action.Id,
                ValidationStepId = action.ValidationStepId,
                ActionType = action.ActionType,
                PerformedBy = action.PerformedBy,
                Comments = action.Comments,
                CreatedAt = action.CreatedAt
            };
        }
    }
}
