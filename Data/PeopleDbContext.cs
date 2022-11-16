using Microsoft.EntityFrameworkCore;
using People.WebApp.Models;

namespace People.WebApp.Data
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> People { get; set; } = default!;
    }
}
