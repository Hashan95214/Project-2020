using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cakeworld.Models
{
    public class Payments
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string CardNo { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvv { get; set; }
        public string BillDate { get; set; }
        public string Email { get; set; }
        public int TotalPrice { get; set; }
        public int BuyerId { get; set; }
        public string OrderNo { get; set; }
    }
}
