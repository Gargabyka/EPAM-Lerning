namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность "Категории"
    /// </summary>
    public class Categories
    {
        /// <summary>
        /// Id категории
        /// </summary>
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
        public object Picture { get; set; }
    }
}