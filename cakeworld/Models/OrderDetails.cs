using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace cakeworld.Models
{
    public class OrderDetails
    {
        [Key]
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public string RequiredDate { get; set; }
        public string OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Method { get; set; }
    }
}
