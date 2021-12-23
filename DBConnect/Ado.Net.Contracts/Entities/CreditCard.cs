using System;
using Ado.Net.Entities;

namespace EFCore.Model
{
    public class CreditCard
    {
        public int Id { get; set; }
        
        public int CardNumber { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
        
        public string CardHolder { get; set; }
        
        public int EmployeeId { get; set; }
        
        public virtual Employees Employees { get; set; }
    }
}