using AsyncAwaitBlazor.Model;
using Microsoft.EntityFrameworkCore;

namespace AsyncAwaitBlazor.Context
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Person { get; set; }

        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
