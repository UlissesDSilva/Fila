using desafio.Consumers;
using desafio.Data;
using Entity = desafio.Models.Entites;
using desafio.Models.Enums;
using desafio.Models.Exceptions;
using desafio.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using desafio.Models.RequestModels;
using System.Text.Json;

namespace desafio.Repository
{
    public class RepositorySubscription : IRepositorySubscriptions
    {
        private readonly Context _context;
        private readonly IRepositoryStatus _repStatus;
        private readonly IRepositoryEventHistory _repEventHistory;
        private readonly SubscriptionConsumer _consumer;
        private readonly long statusActive;
        private readonly long statusDisabled;

        public RepositorySubscription(
            Context context, 
            IRepositoryStatus repositoryStatus, 
            IRepositoryEventHistory repositoryEventHistory,
            SubscriptionConsumer consumer
        )
        {
            _context = context;
            _repStatus = repositoryStatus;
            _repEventHistory = repositoryEventHistory;
            _consumer = consumer;
            statusActive = 1;
            statusDisabled = 2;
        }

        public async Task<Entity.Subscription> SaveSubscription()
        {
            var consumerResult = await _consumer.Receiver();
            SaveSubscriptionRequestModel request = JsonSerializer.Deserialize<SaveSubscriptionRequestModel>(consumerResult);

            var result = await GetSubscriptionByUserId(request.UserId);
            if (result == null) {
                var subscription = new Entity.Subscription() {
                    Id = await GetCountSubscriptions() + 1,
                    UserId = request.UserId,
                    Status = await _repStatus.GetStatusById(statusActive),
                    CreateAt = DateTime.Now
                };
                _context.Subscriptions.Add(subscription);
                await _context.SaveChangesAsync();
                await CreateEventHistory(subscription.Id, TypeOfNotifications.SUBSCRIPTION_PURCHASED);
                return await GetSubscriptionById(subscription.Id);
            }

            result.UpdateAt = DateTime.Now;
            if (result.StatusId == statusActive) {
                await CreateEventHistory(result.Id, TypeOfNotifications.SUBSCRIPTION_CANCELED);
                result.Status = await _repStatus.GetStatusById(statusDisabled);
            } else {
                await CreateEventHistory(result.Id, TypeOfNotifications.SUBSCRIPTION_RESTARTED);
                result.Status = await _repStatus.GetStatusById(statusActive);
            }
            await _context.SaveChangesAsync();            

            return await GetSubscriptionById(result.Id);
        }

        public async Task<IEnumerable<Entity.Subscription>> GetAllSubscription()
        {
            return await _context.Subscriptions.Include(nameof(Entity.User)).Include(nameof(Entity.Status)).ToListAsync();
        }

        public async Task<Entity.Subscription> GetSubscriptionById(long id)
        {
            var result = await _context.Subscriptions.Where(x => x.Id == id).Include(nameof(Entity.User)).Include(nameof(Entity.Status)).FirstOrDefaultAsync();
            return result != null ? result : throw new NotFoundException("User not found");
        }

        public async Task<Entity.Subscription> GetSubscriptionByUserId(long userId)
        {
            var result = await _context.Subscriptions.Where(x => x.UserId == userId).Include(nameof(Entity.User)).Include(nameof(Entity.Status)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<long> GetCountSubscriptions()
        {
            return await _context.Subscriptions.CountAsync();
        }

        private async Task CreateEventHistory(long subscriptionId, TypeOfNotifications type)
        {
            var eventHistory = new Entity.EventHistory() 
            {
                Id = await _repEventHistory.GetCountEventHistory() + 1,
                SubscriptionId = subscriptionId,
                Type = type,
                CreateAt = DateTime.Now
            };
            _ = await _repEventHistory.CreateEventHistory(eventHistory);
        }
    }
}