using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class CustomerAccessor : ICustomerAccessor
    {
        public int DeactivateCustomer(int customerID)
        {
            int result = 0;


            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("Product could not be deactivated.");
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

        public int InsertNewCustomer(Customer customer)
        {
            int customerID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BusinessName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@CustomerFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@CustomerLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@CustomerPhoneNumber", SqlDbType.NVarChar, 15);


            cmd.Parameters["@BusinessName"].Value = customer.BusinessName;
            cmd.Parameters["@CustomerFirstName"].Value = customer.CustomerFirstName;
            cmd.Parameters["@CustomerLastName"].Value = customer.CustomerLastName;
            cmd.Parameters["@CustomerEmail"].Value = customer.CustomerEmail;
            cmd.Parameters["@CustomerPhoneNumber"].Value = customer.CustomerPhoneNumber;


            try
            {
                conn.Open();
                customerID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return customerID;
        }

        public int ReactivateCustomer(int customerID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("Product could not be deactivated.");
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

        public Customer SelectCustomerByID(int customerID)
        {
            Customer customer = new Customer();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_customer_by_customer_ID", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        customer.CustomerID = reader.GetInt32(0);
                        customer.BusinessName = reader.GetString(1);
                        customer.CustomerFirstName = reader.GetString(2);
                        customer.CustomerLastName = reader.GetString(3);
                        customer.CustomerEmail = reader.GetString(4);
                        customer.CustomerPhoneNumber = reader.GetString(5);
                        customer.Active = reader.GetBoolean(6);
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
            return customer;
        }

        public Customer SelectCustomerByName(string customerName)
        {
            Customer customer = new Customer();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_customer_by_customer_Name", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@CustomerFirstName"].Value = customerName;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        customer.CustomerID = reader.GetInt32(0);
                        customer.BusinessName = reader.GetString(1);
                        customer.CustomerFirstName = reader.GetString(2);
                        customer.CustomerLastName = reader.GetString(3);
                        customer.CustomerEmail = reader.GetString(4);
                        customer.CustomerPhoneNumber = reader.GetString(5);
                        customer.Active = reader.GetBoolean(6);
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
            return customer;
        }

        public List<Customer> SelectCustomersByActive(bool active = true)
        {
            List<Customer> customers = new List<Customer>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_customers_by_active", conn);

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
                        
                        var customer = new Customer()
                        {
                            
                            CustomerID = reader.GetInt32(0),
                            BusinessName = reader.GetString(1),
                            CustomerFirstName = reader.GetString(2),
                            CustomerLastName = reader.GetString(3),
                            CustomerEmail = reader.GetString(4),
                            CustomerPhoneNumber = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        };
                        customers.Add(customer);
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
            
            return customers;
        }

        public List<Order> SelectOrdersByCustomerID(int customerID)
        {
            
            List<Order> orders = new List<Order>();

            
            var conn = DBConnection.GetDBConnection();

            
            var cmd = new SqlCommand("sp_select_orders_from_customerID", conn);

            
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                
                conn.Open();

                
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        var order = new Order()
                        {
                            
                            OrderID = reader.GetInt32(0),
                        };
                        orders.Add(order);
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
            
            return orders;

        }

        public int UpdateCustomer(Customer oldCustomer, Customer newCustomer)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@NewBusinessName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewCustomerFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewCustomerLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewCustomerEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewCustomerPhoneNumber", SqlDbType.NVarChar, 15);


            cmd.Parameters.Add("@OldCustomerBusinessName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldCustomerFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldCustomerLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldCustomerEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldCustomerPhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters["@CustomerID"].Value = oldCustomer.CustomerID;
            cmd.Parameters["@NewBusinessName"].Value = newCustomer.BusinessName;
            cmd.Parameters["@NewCustomerFirstName"].Value = newCustomer.CustomerFirstName;
            cmd.Parameters["@NewCustomerLastName"].Value = newCustomer.CustomerLastName;
            cmd.Parameters["@NewCustomerEmail"].Value = newCustomer.CustomerEmail;
            cmd.Parameters["@NewCustomerPhoneNumber"].Value = newCustomer.CustomerPhoneNumber;
            

            cmd.Parameters["@OldCustomerBusinessName"].Value = oldCustomer.BusinessName;
            cmd.Parameters["@OldCustomerFirstName"].Value = oldCustomer.CustomerFirstName;
            cmd.Parameters["@OldCustomerLastName"].Value = oldCustomer.CustomerLastName;
            cmd.Parameters["@OldCustomerEmail"].Value = oldCustomer.CustomerEmail;
            cmd.Parameters["@OldCustomerPhoneNumber"].Value = oldCustomer.CustomerPhoneNumber;
            

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
