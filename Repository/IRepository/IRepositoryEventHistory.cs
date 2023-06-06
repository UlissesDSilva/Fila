using desafio.Models.Entites;

namespace desafio.Repository.IRepository
{
    public interface IRepositoryEventHistory
    {
        Task<IEnumerable<EventHistory>> GetAllEventHistory();
        Task<EventHistory> GetEventHistoryById(long id);
        Task<EventHistory> GetEventHistoryBySubscriptionId(long id);
        Task<long> GetCountEventHistory();
        Task<EventHistory> CreateEventHistory(EventHistory eventHistory);
    }
}