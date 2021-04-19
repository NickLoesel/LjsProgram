using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderAccessor : IOrderAccessor
    {
        public List<Order> SelectOrdersByOrderID(int orderID)
        {
            List<Order> orders = new List<Order>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_order_by_orderID", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OrderID", orderID);

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
                            OrderDate = reader.GetDateTime(1),
                            Active = reader.GetBoolean(2)
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

        public List<Order> SelectOrdersByActive(bool active = true)
        {
            List<Order> orders = new List<Order>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_orders_by_active", conn);

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
                        var order = new Order()
                        {
                            OrderID = reader.GetInt32(0),
                            OrderDate = reader.GetDateTime(1),
                            Active = reader.GetBoolean(2)
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

        public int InsertNewOrder(Order order)
        {
            int orderID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_order", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime);
            
            


            cmd.Parameters["@OrderDate"].Value = order.OrderDate;
            

            try
            {
                conn.Open();
                 orderID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return orderID;
        }

        public int InsertNewCustomerOrder(int customerID, int orderID)
        {
            int rowsReturned = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_add_customer_order", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@OrderID", SqlDbType.Int);




            cmd.Parameters["@CustomerID"].Value = customerID;
            cmd.Parameters["@OrderID"].Value = orderID;


            try
            {
                conn.Open();
                rowsReturned = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsReturned;
        }
    }
}
