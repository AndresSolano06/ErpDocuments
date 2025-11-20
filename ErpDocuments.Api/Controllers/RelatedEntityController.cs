using ErpDocuments.Application.RelatedEntities.DTOs;
using ErpDocuments.Application.RelatedEntities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErpDocuments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatedEntityController : ControllerBase
    {
        private readonly IRelatedEntityService _relatedEntityService;

        public RelatedEntityController(IRelatedEntityService relatedEntityService)
        {
            _relatedEntityService = relatedEntityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRelatedEntityRequest request)
        {
            var result = await _relatedEntityService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _relatedEntityService.GetByIdAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpGet("company/{companyId:guid}")]
        public async Task<IActionResult> GetByCompany(Guid companyId)
        {
            var entities = await _relatedEntityService.GetByCompanyAsync(companyId);
            return Ok(entities);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRelatedEntityRequest request)
        {
            var result = await _relatedEntityService.UpdateAsync(request);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _relatedEntityService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
