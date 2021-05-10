using DataObjects;
using LjsProgram;
using LogicLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PresentationMVC.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private CustomerManager _customerManager = new CustomerManager();
        private OrderManager _orderManager = new OrderManager();
        // GET: Customer
        public ActionResult Customers()
        {
            try
            {
                return View(_customerManager.GetCustomersByActive(true));
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Edit(int? customerID)
        {
            if (customerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                return View(_customerManager.SelectCustomerByID((int)customerID));
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Details(int? customerID)
        {
            if (customerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CustomerID = customerID;
            return View(_customerManager.SelectCustomerByID((int)customerID));
        }

        public ActionResult ViewOrders(int? customerID)
        {
            if (customerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Order> customerOrder = _customerManager.GetOrdersByCustomerID((int)customerID);
            Customer customer = _customerManager.SelectCustomerByID((int)customerID);
            List<Customer> customerList = new List<Customer>(); 
            if (customerOrder.Count == 0)
            {
                ViewBag.Orders = "There are no orders for" + customer.CustomerFirstName;
            }
            else
            {
                foreach (var item in customerOrder)
                {
                    List<Order> FullCustomerOrder = _orderManager.GetOrdersByOrderID(item.OrderID);

                    foreach (var order in FullCustomerOrder)
                    {
                        var test = _customerManager.SelectCustomerByID((int)customerID);
                        var orderDate = order.OrderDate;
                        test.orderDate = orderDate;
                        customerList.Add(test);
                        

                    }
                }
            }
            IEnumerable<Customer> Customer =
                        customerList.Cast<Customer>();
            return View(customerList);
        }

        public ActionResult InactiveCustomers()
        {
            return View(_customerManager.GetCustomersByActive(false));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer model)
        {
            try
            {
                if (!model.BusinessName.IsValidBusinessName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.BusinessName + "Is not a valid Business Name (50 characters max)"
                    });
                }
                if (!model.CustomerEmail.IsValidCustomerEmail())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.CustomerEmail + "Is not a valid email (100 characters max)"
                    });
                }
                if (!model.CustomerFirstName.IsValidCustomerFirstName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.CustomerFirstName + "Is not a first name (50 characters max)"
                    });
                }
                if (!model.CustomerLastName.IsValidCustomerLastName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.CustomerLastName + "Is not a valid last name (50 characters max)"
                    });
                }
                if (!model.CustomerPhoneNumber.IsValidCustomerPhoneNumber())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.CustomerPhoneNumber + "Is not a valid phone number (It should be in the form '111-111-1111')"
                    });
                }

                Customer newCustomer = new Customer()
                {
                    BusinessName = model.BusinessName,
                    CustomerFirstName = model.CustomerFirstName,
                    CustomerLastName = model.CustomerLastName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhoneNumber = model.CustomerPhoneNumber
                };

                _customerManager.AddNewCustomer(newCustomer);
                return RedirectToAction("Customers", "Customer");

            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (error.Contains("null") || error == null)
                {
                    error = "Could Not add customer "
                        + model.CustomerFirstName;
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Reactivate(int? customerID)
        {
            if (customerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer oldCustomer = new Customer();
            Customer newCustomer = new Customer();
            bool result = false;
            try
            {
                oldCustomer = _customerManager.SelectCustomerByID((int)customerID);
                newCustomer = _customerManager.SelectCustomerByID((int)customerID);
                newCustomer.Active = !newCustomer.Active;
                result = _customerManager.UpdateCustomer(oldCustomer, newCustomer);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            return View("Customers", _customerManager.GetCustomersByActive(true));
        }
        public ActionResult Deactivate(int? customerID)
        {
            if (customerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer oldCustomer = new Customer();
            Customer newCustomer = new Customer();
            bool result = false;
            try
            {
                oldCustomer = _customerManager.SelectCustomerByID((int)customerID);
                newCustomer = _customerManager.SelectCustomerByID((int)customerID);
                newCustomer.Active = !newCustomer.Active;
                result = _customerManager.UpdateCustomer(oldCustomer, newCustomer);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            return View("InactiveCustomers", _customerManager.GetCustomersByActive(false));
        }


        [HttpPost]
        public ActionResult EditCustomer(Customer newCustomer)
        {
            try
            {
                if (!newCustomer.BusinessName.IsValidBusinessName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newCustomer.BusinessName + "Is not a valid Business Name (50 characters max)"
                    });
                }
                if (!newCustomer.CustomerEmail.IsValidCustomerEmail())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newCustomer.CustomerEmail + "Is not a valid email (100 characters max)"
                    });
                }
                if (!newCustomer.CustomerFirstName.IsValidCustomerFirstName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newCustomer.CustomerFirstName + "Is not a first name (50 characters max)"
                    });
                }
                if (!newCustomer.CustomerLastName.IsValidCustomerLastName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newCustomer.CustomerLastName + "Is not a valid last name (50 characters max)"
                    });
                }
                if (!newCustomer.CustomerPhoneNumber.IsValidCustomerPhoneNumber())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newCustomer.CustomerPhoneNumber + "Is not a valid phone number (It should be in the form '111-111-1111')"
                    });
                }

                Customer oldCustomer = _customerManager.SelectCustomerByID(newCustomer.CustomerID);

                _customerManager.UpdateCustomer(oldCustomer, newCustomer);

                return RedirectToAction("Customers");
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }
    }
}