using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface ICustomerManager
    {
        List<Customer> GetCustomersByActive(bool active = true);
        List<Order> GetOrdersByCustomerID(int customerID);

        bool AddNewCustomer(Customer newCustomer);
        bool UpdateCustomer(Customer oldCustomer, Customer newCustomer);
        Customer SelectCustomerByID(int customerID);
        Customer SelectCustomerByName(String customerName);
    }
}
