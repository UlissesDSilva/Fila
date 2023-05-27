using desafio.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace desafio.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<EventHistory> EventHistories { get; set; }
    }
}