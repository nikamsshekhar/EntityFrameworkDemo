using AutoMapper;
using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _unitOfWork.EmployeeRespository.GetAllAsync();
            var result = _mapper.Map<List<Models.EmployeeResponse>>(employees);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _unitOfWork.EmployeeRespository.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            var result = _mapper.Map<Models.EmployeeResponse>(employee);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Employee employee)
        {
            var domainEmployee = _mapper.Map<EntityFrameworkCore.Domain.Entities.Employee>(employee);
            domainEmployee.CreatedDate = DateTime.UtcNow;
            domainEmployee = await _unitOfWork.EmployeeRespository.AddAsync(domainEmployee);

            return Ok(_mapper.Map<Models.EmployeeResponse>(domainEmployee));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Employee employee)
        {
            var domainEmployee = _mapper.Map<EntityFrameworkCore.Domain.Entities.Employee>(employee);
            domainEmployee = await _unitOfWork.EmployeeRespository.UpdateAsync(domainEmployee);
            return Ok(_mapper.Map<Models.EmployeeResponse>(domainEmployee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _unitOfWork.EmployeeRespository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
