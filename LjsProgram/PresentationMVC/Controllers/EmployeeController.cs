using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LjsProgram;

namespace PresentationMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeManager _employeeManager = new EmployeeManager();
        private UserManager _userManager = new UserManager();
        // GET: Employee
        public ActionResult Employees()
        {
            ViewBag.Active = true;
            try
            {
                return View(_employeeManager.RetrieveEmployeesByActive(true));
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee model)
        {
            try
            {
                if (!model.LastName.IsValidLastName())
                {

                }
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (ex.InnerException.Message.Contains("error"))
                {
                    error = "Could Not add employee "
                        + model.FirstName;
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }
    }
}