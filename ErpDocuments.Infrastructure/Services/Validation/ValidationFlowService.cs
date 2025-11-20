using ErpDocuments.Application.Validation.DTOs;
using ErpDocuments.Application.Validation.Interfaces;
using ErpDocuments.Domain.Entities;
using ErpDocuments.Domain.Enums;
using ErpDocuments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ErpDocuments.Infrastructure.Services.Validation
{
    public class ValidationFlowService : IValidationFlowService
    {
        private readonly AppDbContext _context;

        public ValidationFlowService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationFlowResponse> CreateFlowAsync(CreateValidationFlowRequest request, CancellationToken cancellationToken = default)
        {
            var flow = new ValidationFlow
            {
                DocumentId = request.DocumentId,
                Name = request.Name,
                Description = request.Description,
                Status = ValidationFlowStatus.Pending
            };

            _context.ValidationFlows.Add(flow);
            await _context.SaveChangesAsync(cancellationToken);

            return new ValidationFlowResponse
            {
                Id = flow.Id,
                DocumentId = flow.DocumentId,
                Name = flow.Name,
                Description = flow.Description,
                Status = flow.Status,
                CreatedAt = flow.CreatedAt
            };
        }

        public async Task<ValidationFlowResponse?> GetFlowAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var flow = await _context.ValidationFlows
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (flow == null)
                return null;

            return new ValidationFlowResponse
            {
                Id = flow.Id,
                DocumentId = flow.DocumentId,
                Name = flow.Name,
                Description = flow.Description,
                Status = flow.Status,
                CreatedAt = flow.CreatedAt,
                StartedAt = flow.StartedAt,
                CompletedAt = flow.CompletedAt
            };
        }

        public async Task<IEnumerable<ValidationFlowResponse>> GetFlowsByDocumentAsync(
            Guid documentId,
            CancellationToken cancellationToken = default)
        {
            return await _context.ValidationFlows
                .Where(x => x.DocumentId == documentId)
                .Select(x => new ValidationFlowResponse
                {
                    Id = x.Id,
                    DocumentId = x.DocumentId,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    StartedAt = x.StartedAt,
                    CompletedAt = x.CompletedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
