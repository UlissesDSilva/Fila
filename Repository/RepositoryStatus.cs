using desafio.Data;
using desafio.Models.Entites;
using desafio.Models.Exceptions;
using desafio.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace desafio.Repository
{
    public class RepositoryStatus : IRepositoryStatus
    {
        private readonly Context _context;

        public RepositoryStatus(Context context)
        {
            _context = context;
        } 
        public async Task<IEnumerable<Status>> GetAllStatus()
        {
            return await _context.Status.ToListAsync();
        }

        public async Task<Status> GetStatusById(long id)
        {
            var result = await _context.Status.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result != null ? result : throw new  NotFoundException("Status not found");
        }
    }
}