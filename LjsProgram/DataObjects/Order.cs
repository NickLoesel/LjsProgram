using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Order
    {
        public int OrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public bool Active { get; set; }
        [NotMapped]
        public String CustomerName { get; set; }
    }

}
