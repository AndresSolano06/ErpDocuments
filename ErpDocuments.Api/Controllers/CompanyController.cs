using ErpDocuments.Application.Companies.DTOs;
using ErpDocuments.Application.Companies.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErpDocuments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyRequest request)
        {
            var result = await _companyService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _companyService.GetByIdAsync(id);

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCompanyRequest request)
        {
            var result = await _companyService.UpdateAsync(request);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _companyService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
