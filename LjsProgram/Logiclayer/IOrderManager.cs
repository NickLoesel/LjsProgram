using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IOrderManager
    {
        List<Order> GetOrdersByActive(bool active = true);
        List<Order> GetOrdersByOrderID(int orderID);
        int AddNewOrder(Order newOrder);
        bool AddNewCustomerOrder(int customerID, int orderID);
    }
}
