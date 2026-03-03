using EntityFrameworkCore.Domain.Entities;
using EntityFrameworkCore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreDemo.Controllers
{
    [Route("api/organizations")]
    public class OrganizationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationController(IUnitOfWork unitOfWork)
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
        public IActionResult Create([FromBody]Organization organization)
        {
            return Ok();
        }

        [Route("/{id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody]Organization organization)
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
