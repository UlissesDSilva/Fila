using System.Text.Json;
using System.Text;
using AutoMapper;
using desafio.Consumers;
using desafio.Models.Entites;
using desafio.Models.RequestModels;
using desafio.Repository.IRepository;
using desafio.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsController : Controller
    {
        private readonly IRepositorySubscriptions _repository;
        private readonly ISubscriptionService _service;
        private readonly IMapper _mapper;
        private readonly SubscriptionConsumer _consumer;

        public SubscriptionsController(IRepositorySubscriptions repository, IMapper mapper, ISubscriptionService service, SubscriptionConsumer consumer)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
            _consumer = consumer;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSubscription()
        {
            var result = await _repository.GetAllSubscription();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetSubscriptionById(long id)
        {
            var result = await _repository.GetSubscriptionById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> SaveSubscription([FromBody] SaveSubscriptionRequestModel request)  
        {
            await _service.SendingMessage<SaveSubscriptionRequestModel>(request);
            return Ok();
        }

        [HttpGet]
        [Route("fila-teste")]
        public async Task<ActionResult> Teste()
        {
            var result = await _consumer.Receiver();
            SaveSubscriptionRequestModel j = JsonSerializer.Deserialize<SaveSubscriptionRequestModel>(result);
            return Ok(j.UserId);
        }
    }
}