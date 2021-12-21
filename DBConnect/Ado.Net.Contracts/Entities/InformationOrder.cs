using System;

namespace Ado.Net.Contracts.Entities
{
    /// <summary>
    /// DTO для метода GetFullInformationOrders
    /// </summary>
    public class InformationOrder
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        public int OrderId { get; set; }
        
        /// <summary>
        /// Id товара
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Кол-во товара
        /// </summary>
        public Int16 Quantity { get; set; }

        /// <summary>
        /// Наменование продукта
        /// </summary>
        public string ProductName { get; set; }
        
        /// <summary>
        /// Id поставщика
        /// </summary>
        public int SupplierID { get; set; }
        
        /// <summary>
        /// Id категории
        /// </summary>
        public int CategoryID { get; set; }
        
        /// <summary>
        /// Количественная единица измерения
        /// </summary>
        public string QuantityPerUnit { get; set; }
        
        /// <summary>
        /// Кол-во товара на складе
        /// </summary>
        public Int16 UnitsInStock { get; set; }
        
        /// <summary>
        /// Кол-во по порядку
        /// </summary>
        public Int16 UnitsOnOrder { get; set; }
        
        /// <summary>
        /// Уровень измерения порядка
        /// </summary>
        public Int16 ReorderLevel { get; set; }
    }
}