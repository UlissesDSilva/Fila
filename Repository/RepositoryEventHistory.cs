using desafio.Data;
using desafio.Models.Entites;
using desafio.Models.Exceptions;
using desafio.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace desafio.Repository
{
    public class RepositoryEventHistory : IRepositoryEventHistory
    {
        private readonly Context _context;

        public RepositoryEventHistory(Context context)
        {
            _context = context;
        }

        public async Task<EventHistory> CreateEventHistory(EventHistory eventHistory)
        {
            _context.EventHistories.Add(eventHistory);
            await _context.SaveChangesAsync();
            return await GetEventHistoryById(eventHistory.Id);
        }

        public async Task<IEnumerable<EventHistory>> GetAllEventHistory()
        {
            return await _context.EventHistories.ToListAsync();
        }

        public async Task<long> GetCountEventHistory()
        {
            return await _context.EventHistories.CountAsync();
        }

        public async Task<EventHistory> GetEventHistoryById(long id)
        {
            var result = await _context.EventHistories.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result != null ? result : throw new NotFoundException("Event not found");
        }

        public async Task<EventHistory> GetEventHistoryBySubscriptionId(long id)
        {
            var result = await _context.EventHistories.Where(x => x.SubscriptionId == id).FirstOrDefaultAsync();
            return result != null ? result : throw new NotFoundException("Event not found");
        }
    }
}