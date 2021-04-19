using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer; 

namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _employeeAccessor;

        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }
        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }

        public bool AddNewEmployee(EmployeeViewModel newEmployee)
        {
            bool result = false;
            int newEmployeeID = 0; 
            try
            {
                newEmployeeID = _employeeAccessor.InsertNewEmployee(newEmployee);
                if (newEmployeeID == 0)
                {
                    throw new ApplicationException("New employee was not added.");
                }
                foreach (var role in newEmployee.Roles)
                {
                    _employeeAccessor.InsertEmployeeRole(newEmployeeID, role);
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Add Employee Failed.", ex);
            }
            return result;
        }

        public bool EditEmployeeProfile(EmployeeViewModel oldEmployee,
                                        EmployeeViewModel newEmployee,
                                        List<string> oldUnassignedRoles,
                                        List<string> newUnassignedRolesoles)
        {
            bool result = false;
            try
            {
                result = (1 == _employeeAccessor.UpdateEmployeeProfile(oldEmployee, newEmployee));
                if (result == false)
                {
                    throw new ApplicationException("Profile data not changed.");
                }
                foreach (var role in newUnassignedRolesoles)
                {
                    if (!oldUnassignedRoles.Contains((role)))
                    {
                        _employeeAccessor.DeleteEmployeeRole(oldEmployee.EmployeeID, role);
                    }
                }
                foreach (var role in newEmployee.Roles)
                {
                    if (!oldEmployee.Roles.Contains(role))
                    {
                        _employeeAccessor.InsertEmployeeRole(oldEmployee.EmployeeID, role);
                    }
                }


                if (oldEmployee.Active != newEmployee.Active)
                {
                    if (newEmployee.Active == true)
                    {
                        _employeeAccessor.ReactivateEmployee(oldEmployee.EmployeeID);
                    }
                    else
                    {
                        _employeeAccessor.DeactivateEmployee(oldEmployee.EmployeeID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }
            return result;
        }

        public List<string> RetrieveAllRoles()
        {
            List<string> roles = null;

            try
            {
                roles = _employeeAccessor.SelectAllRoles();

                if (roles == null)
                {
                    roles = new List<string>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return roles;
        }

        public List<EmployeeViewModel> RetrieveEmployeesByActive(bool active = true)
        {
            List<EmployeeViewModel> employees = null;

            try
            {
                employees = _employeeAccessor.SelectEmployeesByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User list not available.", ex);
            }

            return employees;
        }

        public List<string> RetrieveRolesByEmployeeID(int employeeID)
        {
            List<string> roles = null;

            try
            {
                roles = _employeeAccessor.SelectRolesByEmployeeID(employeeID);

                if (roles == null)
                {
                    roles = new List<string>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }

            return roles;
        }
    }
}
