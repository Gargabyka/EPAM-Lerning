using System.Threading.Tasks;
using Ado.Net.Entities;

namespace Ado.Net.Services
{
    /// <summary>
    /// Сервис для запросов к сущности <see cref="Orders"/>
    /// </summary>
    public class OrdersService
    {
        /// <summary>
        /// Получить информацию о заказах
        /// </summary>
        public async Task<Orders> GetInformationOrdersAsync()
        {
            var query = $@"
                SELECT 
                    o.OrderId    
                FROM dbo.Orders o";
            return await 
        }
     }
}