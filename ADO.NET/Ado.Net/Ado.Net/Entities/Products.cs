namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность "Товары"
    /// </summary>
    public class Products
    {
        /// <summary>
        /// Id товара
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { get; set; }
        
        /// <summary>
        /// Id поставщика
        /// </summary>
        public Suppliers SupplierId { get; set; }
        
        /// <summary>
        /// Id категории 
        /// </summary>
        public Categories CategoriesId { get; set; }
        
        /// <summary>
        /// Количественная единица измерения
        /// </summary>
        public string QuantityPerUnit { get; set; }
        
        /// <summary>
        /// Цена за единицу товара
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Кол-во товара на складе
        /// </summary>
        public int UnitsInStock { get; set; }
        
        /// <summary>
        /// Кол-во по порядку
        /// </summary>
        public int UnitsOnOrder { get; set; }
        
        /// <summary>
        /// Уровень измерения порядка
        /// </summary>
        public int ReorderLevel { get; set; }
        
        /// <summary>
        /// Прекращенный товар
        /// </summary>
        public int Discontinued { get; set; }
    }
}