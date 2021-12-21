using Ado.Net.Dal.Implementations;

namespace DBConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            var northwindDal = new NorthwindDal();
            var list = northwindDal.CustOrdersDetail(10250);
        }
    }
}
