using AutoMapper;
using EntityFrameworkCore.Domain.Interfaces;
using EntityFrameworkCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrganizationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _unitOfWork.OrganizationRespository.GetAllAsync();
            return Ok(organizations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var organization = await _unitOfWork.OrganizationRespository.GetByIdAsync(id);
            return Ok(organization);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Organization organization)
        {
            var domainOrganization = _mapper.Map<EntityFrameworkCore.Domain.Entities.Organization>(organization);
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                domainOrganization.CreatedDate = DateTime.UtcNow;
                domainOrganization = await _unitOfWork.OrganizationRespository.AddAsync(domainOrganization);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return Ok(_mapper.Map<Models.OrganizationResponse>(domainOrganization));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Organization organization)
        {
            //if (organization.Id != id)
            //    throw new Exception("id from url should match with organizations id in request");
            var domainOrganization = _mapper.Map<EntityFrameworkCore.Domain.Entities.Organization>(organization);
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                domainOrganization = await _unitOfWork.OrganizationRespository.UpdateAsync(domainOrganization);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            return Ok(_mapper.Map<Models.OrganizationResponse>(domainOrganization));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.OrganizationRespository.DeleteAsync(id);
            return Ok();
        }
    }
}
