using System;
using Ado.Net.Enum;

namespace Ado.Net.Entities
{
    public class Orders
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public string OrderDate { get; set; } 

        /// <summary>
        /// Дата отправки
        /// </summary>
        public string ShippedDate { get; set; }
        
        
        /// <summary>
        /// Груз
        /// </summary>
        public decimal Freight { get; set; }
        
        /// <summary>
        /// Имя судна
        /// </summary>
        public string ShipName { get; set; }
        
        /// <summary>
        /// Адрес судна
        /// </summary>
        public string ShipAddress { get; set; }
        
        /// <summary>
        /// Город судна
        /// </summary>
        public string ShipCity { get; set; }

        /// <summary>
        /// Страна судна
        /// </summary>
        public string ShipCountry { get; set; }
        
        /// <summary>
        /// Статус заказа
        /// </summary>
        public StateOrder StateOrder { get; set; }
    }
}