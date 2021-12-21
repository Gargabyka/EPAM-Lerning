using System.Collections.Generic;
using Ado.Net.Contracts.Entities;

namespace Ado.Net.Dal.Interfaces
{
    public interface INorthwindDal
    {
        public List<Orders> GetOrders();

        public List<InformationOrder> GetFullInformationOrders(int orderID);

        public void AddRow(int employeeId, string shipName, string shipCity);

        public void DeleteOrders();

        public List<CustOrderHist> CustOrderHist(string customerID);

        public List<CustOrdersDetail> CustOrdersDetail(int orderId);
    }

}
