using desafio.Models.Entites;

namespace desafio.Repository.IRepository
{
    public interface IRepositoryStatus
    {
        Task<IEnumerable<Status>> GetAllStatus();
        Task<Status> GetStatusById(long id);
    }
}