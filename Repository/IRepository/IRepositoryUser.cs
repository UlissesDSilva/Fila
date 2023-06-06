using desafio.Models.Entites;

namespace desafio.Repository.IRepository
{
    public interface IRepositoryUser
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(long id);
        Task<User> CreateUser(User user);
    }
}