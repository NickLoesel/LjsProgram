using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataObjects;

namespace PresentationMVC.Models
{
    public class LjsDBContext : DbContext
    {
        public LjsDBContext()
                    : base("EnterpriseConnection")
        {
        }
        public DbSet<Product> Product { get; set; }
    }
}