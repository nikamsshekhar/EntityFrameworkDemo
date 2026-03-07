using AutoMapper;
using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _unitOfWork.CustomerRespository.GetAllAsync();
            var result = _mapper.Map<List<Models.CustomerResponse>>(customers);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _unitOfWork.CustomerRespository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            var result = _mapper.Map<Models.CustomerResponse>(customer);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Customer customer)
        {
            var domainCustomer = _mapper.Map<EntityFrameworkCore.Domain.Entities.Customer>(customer);
            domainCustomer.CreatedDate = DateTime.UtcNow;
            domainCustomer = await _unitOfWork.CustomerRespository.AddAsync(domainCustomer);

            return Ok(_mapper.Map<Models.CustomerResponse>(domainCustomer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Customer customer)
        {
            var domainCustomer = _mapper.Map<EntityFrameworkCore.Domain.Entities.Customer>(customer);
            domainCustomer = await _unitOfWork.CustomerRespository.UpdateAsync(domainCustomer);
            return Ok(_mapper.Map<Models.CustomerResponse>(domainCustomer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _unitOfWork.CustomerRespository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
