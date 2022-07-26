using Microsoft.EntityFrameworkCore;
using PostgresqlEntityDemo.Models;

namespace PostgresqlEntityDemo.Data
{
    public class ApplicationDbContext : DbContext   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Students> Students { get; set; }
    }
}
