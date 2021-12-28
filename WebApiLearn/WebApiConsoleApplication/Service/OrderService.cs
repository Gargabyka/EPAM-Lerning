using Newtonsoft.Json;
using WebApi.Models;
using WebApiConsoleApplication.Interfaces;

namespace WebApiConsoleApplication;

public class OrderService : IOrderService
{
    private const string WebApiPath = "https://localhost:5001";

    /// <summary>
    /// Получение всех заказов 
    /// </summary>
    public List<Orders> GetOrders()
    {
        const string ordersApiPath = "/api/orders/";

        var orders = new List<Orders>();

        try
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(WebApiPath + ordersApiPath).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                orders = JsonConvert.DeserializeObject<List<Orders>>(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return orders;
    }

    /// <summary>
    /// Получение конкретного заказа
    /// </summary>
    public Orders GetOrder(int orderId)
    {
        const string ordersApiPath = "/api/orders/";
        
        var orders = new Orders();

        try
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(WebApiPath + ordersApiPath + orderId).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                orders = JsonConvert.DeserializeObject<Orders>(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return orders;
    }
    
    // <summary>
    /// Удаление конкретного заказа
    /// </summary>
    public async Task DeleteOrder(int orderId)
    {
        const string ordersApiPath = "/api/orders/";

        try
        {
            using (var client = new HttpClient())
            { 
                string request = WebApiPath + ordersApiPath + orderId;
                var response =  client.DeleteAsync(request);
                response.Result.EnsureSuccessStatusCode();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    
    // <summary>
    /// Добавление заказа
    /// </summary>
    public string PostOrder(string shipName, string shipCity, string shipCountry)
    {
        const string ordersApiPath = "/api/orders/";

        var newOrder = new
        {
            shipName = shipName,
            shipCity = shipCity,
            shipCountry = shipCountry
        };
            
        try
        {
            using (var client = new HttpClient())
            { 
                string request = WebApiPath + ordersApiPath;
                var response = client.PostAsJsonAsync(request, newOrder).Result;
                return response.StatusCode.ToString();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}