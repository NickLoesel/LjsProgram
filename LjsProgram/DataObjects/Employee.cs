using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
    }

    public class EmployeeViewModel : Employee
    {
        public List<string> Roles { get; set; }
        [NotMapped]
        public List<string> UnassignedRoles { get; set; }
    }
}

