using System.ComponentModel.DataAnnotations;

namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность "Поставщики"
    /// </summary>
    public class Suppliers
    {
        /// <summary>
        /// Id поставщика
        /// </summary>
        [Key]
        public int SupplierID { get; set; }
        
        /// <summary>
        /// Наименование компании
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Контактное лицо
        /// </summary>
        public string ContactName { get; set; }
        
        /// <summary>
        /// Заголовок контакта
        /// </summary>
        public string ContactTitle { get; set; }
        
        /// <summary>
        /// Адресс
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Регион
        /// </summary>
        public string Region { get; set; }
        
        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Факс
        /// </summary>
        public string Fax { get; set; }
        
        /// <summary>
        /// Домашнаяя страница 
        /// </summary>
        public string HomePage { get; set; }
    }
}