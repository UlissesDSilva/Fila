using AutoMapper;
using desafio.Models.Entites;
using desafio.Models.RequestModels;
using desafio.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IMapper _mapper;
        public UsersController(IRepositoryUser repositoryUser, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers() 
        {
            var result = await _repositoryUser.GetAllUsers();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetUserById(long id)
        {
            var result = await _repositoryUser.GetUserById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] AddUserRequestModel request)
        {
            var result = await _repositoryUser.CreateUser(_mapper.Map<User>(request));
            return Ok(result);
        }
    }
}