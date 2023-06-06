using desafio.Data;
using desafio.Models.Entites;
using desafio.Models.Exceptions;
using desafio.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace desafio.Repository
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly Context _context;

        public RepositoryUser(Context context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            user.Create_at = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await GetUserById(user.Id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            var result = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null) {
                throw new NotFoundException("User not found");
            }
            return result;
        }
    }
}