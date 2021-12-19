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
        /// Id клиента
        /// </summary>
        public Customers CustomerId { get; set; }
        
        /// <summary>
        /// Id работника
        /// </summary>
        public Employees EmployeeId { get; set; }
        
        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime? OrderDate { get; set; }
        
        /// <summary>
        /// Требуемая дата
        /// </summary>
        public DateTime? RequiredDate { get; set; }
        
        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime? ShippedDate { get; set; }
        
        /// <summary>
        /// Судно
        /// </summary>
        public Shippers ShipVia { get; set; }
        
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
        /// Регион судна
        /// </summary>
        public string ShipRegion { get; set; }
        
        /// <summary>
        /// Код почтового отправления
        /// </summary>
        public string ShipPostalCode { get; set; }
        
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