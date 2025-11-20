using ErpDocuments.Application.Validation.DTOs;
using ErpDocuments.Application.Validation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErpDocuments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationActionController : ControllerBase
    {
        private readonly IValidationActionService _actionService;

        public ValidationActionController(IValidationActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateValidationActionRequest request)
        {
            var result = await _actionService.RegisterActionAsync(request);
            return Ok(result);
        }
    }
}
