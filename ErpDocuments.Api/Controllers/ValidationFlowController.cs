using ErpDocuments.Application.Validation.DTOs;
using ErpDocuments.Application.Validation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErpDocuments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationFlowController : ControllerBase
    {
        private readonly IValidationFlowService _flowService;

        public ValidationFlowController(IValidationFlowService flowService)
        {
            _flowService = flowService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateValidationFlowRequest request)
        {
            var result = await _flowService.CreateFlowAsync(request);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var flow = await _flowService.GetFlowAsync(id);

            if (flow == null)
                return NotFound();

            return Ok(flow);
        }

        [HttpGet("document/{documentId:guid}")]
        public async Task<IActionResult> GetByDocument(Guid documentId)
        {
            var flows = await _flowService.GetFlowsByDocumentAsync(documentId);
            return Ok(flows);
        }
    }
}
