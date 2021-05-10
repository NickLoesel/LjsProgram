using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string CustomerFirstName { get; set; }
        [Required]
        public string CustomerLastName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; } 
        public bool Active { get; set; }
        [NotMapped]
        public DateTime orderDate { get; set; }

    }

    public class CustomerOrderViewModel : Customer
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
