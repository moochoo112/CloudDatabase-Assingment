using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserInfo;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
