using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public int DeactivateEmployee(int employeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_deactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("Employee could not be deactivated.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int DeleteEmployeeRole(int employeeID, string role)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_remove_employeerole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@RoleID"].Value = role;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException(role + " role could not be removed.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int InsertEmployeeRole(int employeeID, string role)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_add_employeerole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@RoleID"].Value = role;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException(role + " role could not be added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int InsertNewEmployee(Employee employee)
        {
            int employeeID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters["@Email"].Value = employee.Email;
            cmd.Parameters["@FirstName"].Value = employee.FirstName;
            cmd.Parameters["@LastName"].Value = employee.LastName;
            cmd.Parameters["@PhoneNumber"].Value = employee.PhoneNumber;

            try
            {
                conn.Open();
                employeeID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employeeID;
        }

        public int ReactivateEmployee(int employeeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();

            
            var conn = DBConnection.GetDBConnection();

            
            var cmd = new SqlCommand("sp_select_all_employee_roles", conn);

            
            cmd.CommandType = CommandType.StoredProcedure;

            
            try
            {
                
                conn.Open();

                
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        public List<EmployeeViewModel> SelectEmployeesByActive(bool active = true)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            
            var conn = DBConnection.GetDBConnection();
            
            var cmd = new SqlCommand("sp_select_employees_by_active", conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new EmployeeViewModel()
                        {
                            EmployeeID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            Roles = null 
                        };
                        employees.Add(employee);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employees;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            
            var conn = DBConnection.GetDBConnection();

            
            var cmd = new SqlCommand("sp_select_user_roles_by_employeeID", conn);

            
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            
            try
            {
                
                conn.Open();

                
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        public int UpdateEmployeeProfile(Employee oldEmployee, Employee newEmployee)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_employee_profile", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters["@EmployeeID"].Value = oldEmployee.EmployeeID;
            cmd.Parameters["@NewEmail"].Value = newEmployee.Email;
            cmd.Parameters["@NewFirstName"].Value = newEmployee.FirstName;
            cmd.Parameters["@NewLastName"].Value = newEmployee.LastName;
            cmd.Parameters["@NewPhoneNumber"].Value = newEmployee.PhoneNumber;
            cmd.Parameters["@OldEmail"].Value = oldEmployee.Email;
            cmd.Parameters["@OldFirstName"].Value = oldEmployee.FirstName;
            cmd.Parameters["@OldLastName"].Value = oldEmployee.LastName;
            cmd.Parameters["@OldPhoneNumber"].Value = oldEmployee.PhoneNumber;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
