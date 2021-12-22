using System.Data.Entity;
using EFCore.Model;

namespace EFCore
{
    public class UserContext : NorthwindEntities
    {
        public UserContext() : base()
        { }
        
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}