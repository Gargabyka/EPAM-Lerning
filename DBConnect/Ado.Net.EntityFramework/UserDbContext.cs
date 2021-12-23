using System.Data.Entity;
using Ado.Net.Entities;

namespace Ado.Net.EntityFramework
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base("DefaultConnection"){}
        
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
