using desafio.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : Controller
    {
        private readonly IRepositoryStatus _repository;

        public StatusController(IRepositoryStatus repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStatus() 
        {
            var result = await _repository.GetAllStatus();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetStatusById(long id)
        {
            var result = await _repository.GetStatusById(id);
            return Ok(result);
        }
    }
}