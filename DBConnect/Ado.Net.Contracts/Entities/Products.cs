using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int ProductId { get; set; }
        
        /// <summary>
        /// Наименование товара
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
        /// Цена за единицу товара
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Кол-во товара на складе
        /// </summary>
        public short UnitsInStock { get; set; }
        
        /// <summary>
        /// Кол-во по порядку
        /// </summary>
        public short UnitsOnOrder { get; set; }
        
        /// <summary>
        /// Уровень измерения порядка
        /// </summary>
        public short ReorderLevel { get; set; }
        
        /// <summary>
        /// Прекращенный товар
        /// </summary>
        public bool Discontinued { get; set; }
        
        public virtual Categories Categories { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}