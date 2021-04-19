using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Product
    {
        public int ProductID { get; set; }
        public  string ProductName { get; set; }
        public string Vendor { get; set; }
        public string ProductType { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SalePrice { get; set; }  
        public int Quantity { get; set; }
        public bool Active { get; set; }
    }
}
