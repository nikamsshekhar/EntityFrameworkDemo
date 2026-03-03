using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreDemo.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        [Route("/")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [Route("/")]
        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            return Ok();
        }

        [Route("/{id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody] Employee employee)
        {
            return Ok();
        }

        [Route("/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
