using System.ComponentModel.DataAnnotations;

namespace AsyncAwaitBlazor.Model
{
    public class Person
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }
    }
}
