using System.Configuration;
using Ado.Net.Dal.Implementations;
using NUnit.Framework;

namespace DAL.Test
{
    [TestFixture]
    public class Tests
    {
        private string _connectionString;
        public NorthwindDal _dal;
        
        [SetUp]
        public void Setup()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _dal = new NorthwindDal();
        }

        [Test]
        public void GetOrders()
        {
            var orders = _dal.GetOrders();
            
            Assert.IsNotNull(orders);
            Assert.IsNotEmpty(orders);
        }
    }
}