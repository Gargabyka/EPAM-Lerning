using System;

namespace Ado.Net.Contracts.Entities
{
    public class CustOrdersDetail
    {
        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string ProductName { get; set; }
        
        /// <summary>
        /// Цена
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Количество 
        /// </summary>
        public Int16 Quantity { get; set; }
        
        /// <summary>
        /// Скидка
        /// </summary>
        public int Discount { get; set; }
        
        /// <summary>
        /// Итоговая цена
        /// </summary>
        public decimal ExtendedPrice { get; set; }
    }
}