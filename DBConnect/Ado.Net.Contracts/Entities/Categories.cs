using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность "Категории"
    /// </summary>
    public class Categories
    {
        public Categories()
        {
            this.Products = new HashSet<Products>();
        }
        
        /// <summary>
        /// Id категории
        /// </summary>
        [Key]
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Наменование категории
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Описание категории
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Изображение 
        /// </summary>
        public byte[] Picture { get; set; }
        
        public virtual ICollection<Products> Products { get; set; }
    }
}