using desafio.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventHistoryController : Controller
    {
        private readonly IRepositoryEventHistory _repEventHistory;

        public EventHistoryController(IRepositoryEventHistory repositoryEventHistory)
        {
            _repEventHistory = repositoryEventHistory;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEventHistory()
        {
            var result = await _repEventHistory.GetAllEventHistory();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetEventHistoryById(long id)
        {
            var result = await _repEventHistory.GetEventHistoryById(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("subscriptionId/{id}")]
        public async Task<ActionResult> GetEventHistoryBySubscriptionId(long id)
        {
            var result = await _repEventHistory.GetEventHistoryById(id);
            return Ok(result);
        }
    }
}