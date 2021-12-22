using System.Data.Entity;

namespace DBConnect
{
    public class DataBaseConnection: DbContext
    {
        public DataBaseConnection() : base("DefaultConnection")
        {
            
        }
    }
}