using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IEmployeeManager
    {
        List<EmployeeViewModel> RetrieveEmployeesByActive(bool active = true);
        List<string> RetrieveRolesByEmployeeID(int employeeID);
        List<string> RetrieveAllRoles();
        bool EditEmployeeProfile(EmployeeViewModel oldEmployee,
                                 EmployeeViewModel newEmployee,
                                 List<string> oldUnassignedRoles,
                                 List<string> newUnassignedRoles);
        bool AddNewEmployee(EmployeeViewModel newEmployee);

        EmployeeViewModel SelectEmployeeByEmployeeID(int employeeID);
    }
}
