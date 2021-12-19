namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность судна
    /// </summary>
    public class Shippers
    {
        /// <summary>
        /// Id судна
        /// </summary>
        public int ShipperId { get; set; }
        
        /// <summary>
        /// Название компании
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
    }
}