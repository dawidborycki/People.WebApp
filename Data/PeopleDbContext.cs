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

            var items = People.ToListAsync().Result;
            
            if(items.Count == 0) {
                People.Add(new Person 
                {
                    Id = "13398c8c-c0f1-4dc8-8d60-712a47e3704f", 
                    FirstName = "John", 
                    LastName = "Doe", 
                    BirthDate = new System.DateTime(1990, 5, 3)
                });

                People.Add(new Person 
                {
                    Id = "8f695c67-07f0-4b34-94a3-b8ec1150b596", 
                    FirstName = "Franco", 
                    LastName = "Gilberto", 
                    BirthDate = new System.DateTime(1989, 4, 3)
                });

                People.Add(new Person 
                {
                    Id = "72f4e65a-43ba-4d0a-b8d7-7b89ee6c6611", 
                    FirstName = "Francesco", 
                    LastName = "Trenco", 
                    BirthDate = new System.DateTime(1992, 4, 4)
                });
                
                this.SaveChangesAsync();
            }
        }

        public DbSet<Person> People { get; set; } = default!;
    }
}
