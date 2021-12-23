using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net.EF.Models
{
    public class CreditCards
    {
        [Key]
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string CardHolder { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employees Employee { get; set; }
    }
}
