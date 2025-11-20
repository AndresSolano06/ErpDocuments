using ErpDocuments.Application.RelatedEntities.DTOs;
using ErpDocuments.Application.RelatedEntities.Interfaces;
using ErpDocuments.Domain.Entities;
using ErpDocuments.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ErpDocuments.Infrastructure.Services.RelatedEntities
{
    public class RelatedEntityService : IRelatedEntityService
    {
        private readonly AppDbContext _context;

        public RelatedEntityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RelatedEntityResponse> CreateAsync(CreateRelatedEntityRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new RelatedEntity
            {
                CompanyId = request.CompanyId,
                Name = request.Name,
                Type = request.Type
            };

            _context.RelatedEntities.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new RelatedEntityResponse
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Name = entity.Name,
                Type = entity.Type
            };
        }

        public async Task<RelatedEntityResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.RelatedEntities
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity == null)
                return null;

            return new RelatedEntityResponse
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Name = entity.Name,
                Type = entity.Type
            };
        }

        public async Task<IEnumerable<RelatedEntityResponse>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return await _context.RelatedEntities
                .Where(x => x.CompanyId == companyId)
                .Select(x => new RelatedEntityResponse
                {
                    Id = x.Id,
                    CompanyId = x.CompanyId,
                    Name = x.Name,
                    Type = x.Type
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<RelatedEntityResponse?> UpdateAsync(UpdateRelatedEntityRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.RelatedEntities
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return null;

            entity.Name = request.Name;
            entity.Type = request.Type;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return new RelatedEntityResponse
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Name = entity.Name,
                Type = entity.Type
            };
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.RelatedEntities
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity == null)
                return false;

            _context.RelatedEntities.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
