using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cakeworld.Models
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
