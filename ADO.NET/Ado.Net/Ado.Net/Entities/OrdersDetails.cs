using System;

namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность "Детали заказа"
    /// </summary>
    public class OrdersDetails
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        public int OrderId { get; set; }
        
        /// <summary>
        /// Id товара
        /// </summary>
        public Products ProductId { get; set; }
        
        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Скидка
        /// </summary>
        public Single Discount { get; set; }
    }
}