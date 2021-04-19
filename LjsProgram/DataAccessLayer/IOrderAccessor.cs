using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IOrderAccessor
    {
        List<Order> SelectOrdersByActive(bool active = true);
        List<Order> SelectOrdersByOrderID(int orderID);
        int InsertNewOrder(Order order);
        int InsertNewCustomerOrder(int customerID, int orderID);

    }
}
