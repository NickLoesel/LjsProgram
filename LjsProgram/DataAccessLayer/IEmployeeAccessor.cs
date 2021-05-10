using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer   
{
    public interface IEmployeeAccessor
    {
        List<EmployeeViewModel> SelectEmployeesByActive(bool active = true);
        List<string> SelectRolesByEmployeeID(int employeeID);
        List<string> SelectAllRoles();
        int UpdateEmployeeProfile(Employee oldEmployee, Employee newEmployee);
        int DeactivateEmployee(int employeeID);
        int ReactivateEmployee(int employeeID);
        int DeleteEmployeeRole(int employeeID, string role);
        int InsertEmployeeRole(int employeeID, string role);
        int InsertNewEmployee(Employee employee);
        EmployeeViewModel SelectEmployeesByID(int employeeID);
    }
}
