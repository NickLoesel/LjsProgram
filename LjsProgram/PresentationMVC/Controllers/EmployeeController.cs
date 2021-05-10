using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LjsProgram;
using System.Net;

namespace PresentationMVC.Controllers
{
    [Authorize]
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

        public ActionResult CreateEmployee()
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            List<String> Roles = _employeeManager.RetrieveAllRoles().ToList();
            ViewBag.Roles = Roles;
            return View();
        }

        public ActionResult InactiveEmployees()
        {
            return View(_employeeManager.RetrieveEmployeesByActive(false));
        }
        public ActionResult Reactivate(int? employeeID)
        {
            if (employeeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel oldEmployee = new EmployeeViewModel();
            EmployeeViewModel newEmployee = new EmployeeViewModel();
            bool result = false;
            try
            {
                oldEmployee = _employeeManager.SelectEmployeeByEmployeeID((int)employeeID);
                newEmployee = _employeeManager.SelectEmployeeByEmployeeID((int)employeeID);
                List<string> roles = _employeeManager.RetrieveRolesByEmployeeID((int)employeeID);
                oldEmployee.Roles = roles;
                newEmployee.Roles = roles;
                newEmployee.Active = !newEmployee.Active;
                result = _employeeManager.EditEmployeeProfile(oldEmployee, newEmployee, roles, roles);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            return View("Employees", _employeeManager.RetrieveEmployeesByActive(true));
        }


        public ActionResult Edit(int? employeeID)
        {
            if (employeeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                EmployeeViewModel employee = _employeeManager.SelectEmployeeByEmployeeID((int)employeeID);
                List<String> assignedRoles = _employeeManager.RetrieveRolesByEmployeeID((int)employeeID);
                List<String> unassignedRoles = _employeeManager.RetrieveAllRoles().ToList();
                foreach (var r in assignedRoles)
                {
                    if (employee.Roles == null)
                    {
                        employee.Roles = new List<string>();
                    }
                    employee.Roles.Add(r);
                }
                foreach (var r in unassignedRoles.ToList())
                {
                    if (assignedRoles.Contains(r))
                    {
                        unassignedRoles.Remove(r);
                    }
                }
                if (unassignedRoles.Count == 0)
                {
                    unassignedRoles.Add("no roles");
                }
                if (assignedRoles.Count == 0)
                {
                    assignedRoles.Add("no roles");
                }

                ViewBag.unassignedRoles = unassignedRoles;
                ViewBag.assignedRoles = assignedRoles;
                return View(employee);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Deactivate(int? employeeID)
        {
            if (employeeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel oldEmployee = new EmployeeViewModel();
            EmployeeViewModel newEmployee = new EmployeeViewModel();
            bool result = false;
            try
            {
                oldEmployee = _employeeManager.SelectEmployeeByEmployeeID((int)employeeID);
                newEmployee = _employeeManager.SelectEmployeeByEmployeeID((int)employeeID);
                List<string> roles = _employeeManager.RetrieveRolesByEmployeeID((int)employeeID);
                oldEmployee.Roles = roles;
                newEmployee.Roles = roles;
                newEmployee.Active = !newEmployee.Active;
                result = _employeeManager.EditEmployeeProfile(oldEmployee, newEmployee, roles, roles);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            return View("InactiveEmployees", _employeeManager.RetrieveEmployeesByActive(false));
        }

        [HttpPost]
        public ActionResult EditEmployee(EmployeeViewModel newEmployee)
        {
            try
            {
                if (!newEmployee.Email.IsValidEmail())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newEmployee.Email + "Is not a valid Email (100 characters max)"
                    });
                }
                if (!newEmployee.FirstName.IsValidFirstName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newEmployee.FirstName + "Is not a valid First Name (50 characters max)"
                    });
                }
                if (!newEmployee.LastName.IsValidLastName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newEmployee.LastName + "Is not a last name (100 characters max)"
                    });
                }
                if (!newEmployee.PhoneNumber.IsValidPhoneNumber())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newEmployee.PhoneNumber + "Is not a valid phone number (15 characters max)"
                    });
                }

                EmployeeViewModel oldEmployee = _employeeManager.SelectEmployeeByEmployeeID(newEmployee.EmployeeID);
                List<String> oldUnassignedRoles = _employeeManager.RetrieveRolesByEmployeeID(newEmployee.EmployeeID);
                List<String> newUnassignedRoles = newEmployee.Roles;
                List<String> roles = new List<String>();
                List<String> test = new List<String>();
                foreach (var item in newEmployee.UnassignedRoles)
                {
                    roles.Add((string)item);
                }
                newEmployee.Roles = roles;
                foreach (var item in oldUnassignedRoles)
                {
                    test.Add((string)item);
                }
                oldEmployee.Roles = test;
                _employeeManager.EditEmployeeProfile(oldEmployee, newEmployee, oldUnassignedRoles, newUnassignedRoles);

                return RedirectToAction("Employees");
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }
        [HttpPost]
        public ActionResult CreateEmployee(EmployeeViewModel model)
        {
            try
            {
                if (!model.LastName.IsValidLastName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.LastName + "Is not a valid Last Name (100 characters max)"
                    });
                }
                if (!model.LastName.IsValidFirstName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.FirstName + "Is not a valid Last Name (50 characters max)"
                    });
                }
                var newEmployee = new EmployeeViewModel()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Active = model.Active
                };
                List<string> roles = new List<string>();
                foreach (var item in model.Roles)
                {
                    roles.Add((string)item);
                }
                newEmployee.Roles = roles;
                _employeeManager.AddNewEmployee(newEmployee);
                return RedirectToAction("Employees", "Employee");

            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (ex.InnerException.Message.Contains("duplicate"))
                {
                    error = "Could Not add employee "
                        + model.FirstName;
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }
    }
}