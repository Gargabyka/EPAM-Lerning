using System;
using System.ComponentModel.DataAnnotations;

namespace Ado.Net.Entities
{
    /// <summary>
    /// Сущность "работники"
    /// </summary>
    public class Employees
    {
        /// <summary>
        /// Id работника
        /// </summary>
        [Key]
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Имя 
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Заглавие
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Титул вежливости
        /// </summary>
        public string TitleOfCourtesy { get; set; }
        
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? BirthDate { get; set; }
        
        /// <summary>
        /// Дата начала работы 
        /// </summary>
        public DateTime? HireDate { get; set; }
        
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
        /// Домашний телефон
        /// </summary>
        public string HomePhone { get; set; }
        
        /// <summary>
        /// Расширение 
        /// </summary>
        public string Extension { get; set; }
        
        /// <summary>
        /// Фото
        /// </summary>
        public object Photo { get; set; }
        
        /// <summary>
        /// Записи
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Непосредственный начальник
        /// </summary>
        public Employees ReportsTo { get; set; }
        
        /// <summary>
        /// Путь к фотографии
        /// </summary>
        public string PhotoPath { get; set; }
    }
}