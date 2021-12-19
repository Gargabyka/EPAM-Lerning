namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность "Клиенты"
    /// </summary>
    public class Customers
    {
        /// <summary>
        /// Id клиента
        /// </summary>
        public string CustomerID { get; set; }
        
        /// <summary>
        /// Имя компании
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
    }
}