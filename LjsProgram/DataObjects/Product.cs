using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Vendor { get; set; }
        [Required]
        public string ProductType { get; set; }
        [Required]
        public decimal BuyPrice { get; set; }
        [Required]
        public decimal SalePrice { get; set; }  
        [Required]
        public int Quantity { get; set; }
        public bool Active { get; set; }
    }
}
