using Authorication.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Authorisation.Application.Persistance
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            Database.Migrate();
        }
        public DbSet<User> Users { get; set; }
    }

}
