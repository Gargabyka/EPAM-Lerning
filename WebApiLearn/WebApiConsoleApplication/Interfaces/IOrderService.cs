using WebApi.Models;

namespace WebApiConsoleApplication.Interfaces;

public interface IOrderService
{
    List<Orders> GetOrders();

    Orders GetOrder(int orderId);

    Task DeleteOrder(int orderId);

    string PostOrder(string shipName, string shipCity, string shipCountry);
}