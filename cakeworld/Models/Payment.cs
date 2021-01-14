using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace cakeworld.Models
{
    public class Payment
    {
        [Key]
        public int PayId { get; set; }
        public int PayDate { get; set; }
        public float PayAmount { get; set; }
        public string PaidCusId { get; set; }
    }
}
