using desafio.Models.Entites;

namespace desafio.Repository.IRepository
{
    public interface IRepositorySubscriptions
    {
        Task<IEnumerable<Subscription>> GetAllSubscription();
        Task<Subscription> GetSubscriptionById(long id);
        Task<Subscription> GetSubscriptionByUserId(long userId);
        Task<Subscription> SaveSubscription();
        Task<long> GetCountSubscriptions();
    }
}