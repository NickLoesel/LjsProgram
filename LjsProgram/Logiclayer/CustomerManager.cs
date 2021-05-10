using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerAccessor _customerAccessor = null;

        public CustomerManager()
        {
            _customerAccessor = new CustomerAccessor();
        }

        public CustomerManager(ICustomerAccessor customerAccessor)
        {
            _customerAccessor = customerAccessor;
        }

        public bool AddNewCustomer(Customer newCustomer)
        {
            bool result = false;
            int newCustomerID = 0;

            try
            {
                newCustomerID = _customerAccessor.InsertNewCustomer(newCustomer);
                if (newCustomerID == 0)
                {
                    throw new ApplicationException("New customer was not added.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add new customer failed.", ex);
            }
            return result;
        }

        public List<Customer> GetCustomersByActive(bool active = true)
        {
            List<Customer> customers = null;
            try
            {
                customers = _customerAccessor.SelectCustomersByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Customer List unavailable.", ex);
            }
            return customers;
        }

        public List<Order> GetOrdersByCustomerID(int customerID)
        {
            List<Order> orders = null;
            try
            {
                orders = _customerAccessor.SelectOrdersByCustomerID(customerID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Order List unavailable.", ex);
            }
            return orders;

        }

        public Customer SelectCustomerByID(int customerID)
        {
            Customer customer = null;

            try
            {
                customer = _customerAccessor.SelectCustomerByID(customerID);
                if (customer == null)
                {
                    throw new ApplicationException("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Customer not found.", ex);
            }

            return customer;
        }
        public Customer SelectCustomerByName(String customerName)
        {
            Customer customer = null;

            try
            {
                customer = _customerAccessor.SelectCustomerByName(customerName);
                if (customer == null)
                {
                    throw new ApplicationException("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Customer not found.", ex);
            }

            return customer;
        }

        public bool UpdateCustomer(Customer oldCustomer, Customer newCustomer)
        {
            bool result = false;

            try
            {
                result = (1 == _customerAccessor.UpdateCustomer(oldCustomer, newCustomer));
                if (result == false)
                {
                    throw new ApplicationException("Customer data not changed.");
                }

                if (oldCustomer.Active != newCustomer.Active)
                {
                    if (newCustomer.Active == true)
                    {
                        _customerAccessor.ReactivateCustomer(oldCustomer.CustomerID);
                    }
                    else
                    {
                        _customerAccessor.DeactivateCustomer(oldCustomer.CustomerID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }
    }
}
