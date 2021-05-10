using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PresentationMVC.Models.LjsDBContext;

namespace PresentationMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private OrderManager _orderManager = new OrderManager();
        private CustomerManager _customerManager = new CustomerManager();
        // GET: Order
        public ActionResult Orders()
        {
            ViewBag.Active = true;
            try
            {
                return View(_orderManager.GetOrdersByActive(true));
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Create()
        {
            List<Customer> Customers = _customerManager.GetCustomersByActive(true);
            List<string> CustomerNames = Customers.Select(r => r.CustomerFirstName).ToList();
            ViewBag.CustomerNames = CustomerNames;
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order model)
        {
            try
            {

                Customer customer = _customerManager.SelectCustomerByName(model.CustomerName);
                int orderNumber = _orderManager.AddNewOrder(model);
                _orderManager.AddNewCustomerOrder(customer.CustomerID, orderNumber);
                return RedirectToAction("Customers", "Customer", null);

            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (error.Contains("null") || error == null)
                {
                    error = "Could Not add order ";
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }
    }
}