using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class OrderManager : IOrderManager
    {
        private IOrderAccessor _orderAccessor = null;

        public OrderManager()
        {
            _orderAccessor = new OrderAccessor();
        }

        public OrderManager(IOrderAccessor orderAccessor)
        {
            _orderAccessor = orderAccessor;
        }

        public bool AddNewCustomerOrder(int customerID, int orderID)
        {
            bool result = false;
            int rowsReturned = 0;

            try
            {
                rowsReturned = _orderAccessor.InsertNewCustomerOrder(customerID, orderID);
                if (rowsReturned != 0)
                {
                    throw new ApplicationException("New product was not added.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add new product failed.", ex);
            }
            return result;
        }

        public int AddNewOrder(Order newOrder)
        {
            int newOrderID = 0;

            try
            {
                newOrderID = _orderAccessor.InsertNewOrder(newOrder);
                if (newOrderID == 0)
                {
                    throw new ApplicationException("New Order was not added.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add new Order failed.", ex);
            }
            return newOrderID;
        }

        public List<Order> GetOrdersByActive(bool active = true)
        {
            List<Order> orders = null;
            try
            {
                orders = _orderAccessor.SelectOrdersByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Order List unavailable.", ex);
            }
            return orders;
        }

        public List<Order> GetOrdersByOrderID(int orderID)
        {
            List<Order> orders = null;
            try
            {
                orders = _orderAccessor.SelectOrdersByOrderID(orderID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Order List unavailable.", ex);
            }
            return orders;
        }
    }
}
