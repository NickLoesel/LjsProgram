using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface ICustomerAccessor
    {
        List<Customer> SelectCustomersByActive(bool active = true);
        List<Order> SelectOrdersByCustomerID(int customerID);
        int UpdateCustomer(Customer oldCustomer, Customer newCustomer);
        int ReactivateCustomer(int customerID);
        int DeactivateCustomer(int customerID);
        int InsertNewCustomer(Customer customer);
    }
}
