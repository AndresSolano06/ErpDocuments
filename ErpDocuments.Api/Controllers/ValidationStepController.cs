using ErpDocuments.Application.Validation.DTOs;
using ErpDocuments.Application.Validation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErpDocuments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationStepController : ControllerBase
    {
        private readonly IValidationStepService _stepService;

        public ValidationStepController(IValidationStepService stepService)
        {
            _stepService = stepService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateValidationStepRequest request)
        {
            var result = await _stepService.CreateStepAsync(request);
            return Ok(result);
        }

        [HttpGet("flow/{flowId:guid}")]
        public async Task<IActionResult> GetByFlow(Guid flowId)
        {
            var steps = await _stepService.GetStepsByFlowAsync(flowId);
            return Ok(steps);
        }
    }
}
