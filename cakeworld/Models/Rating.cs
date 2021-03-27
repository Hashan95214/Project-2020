using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cakeworld.Models
{
    public class Rating
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public int Star { get; set; }

        public string BuyerFirstName { get; set; }

        public string BuyerLastName { get; set; }

        public string Comment { get; set; }
    }
}
