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
    public class ProductAccessor : IProductAccessor
    {
        public int DeactivateProduct(int productID)
        {
            int result = 0;

            
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_product", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", productID);

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

        public int InsertNewProduct(Product product)
        {
            int productID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_insert_new_product", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Vendor", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@ProductType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal,19);
            cmd.Parameters["@BuyPrice"].Precision = 19;
            cmd.Parameters["@BuyPrice"].Scale = 4;
            cmd.Parameters.Add("@SalePrice", SqlDbType.Decimal, 19);
            cmd.Parameters["@SalePrice"].Precision = 19;
            cmd.Parameters["@SalePrice"].Scale = 4;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int);


            cmd.Parameters["@ProductName"].Value = product.ProductName;
            cmd.Parameters["@Vendor"].Value = product.Vendor;
            cmd.Parameters["@ProductType"].Value = product.ProductType;
            cmd.Parameters["@BuyPrice"].Value = product.BuyPrice;
            cmd.Parameters["@SalePrice"].Value = product.SalePrice;
            cmd.Parameters["@Quantity"].Value = product.Quantity;

            try
            {
                conn.Open();
                productID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return productID;
        }
        public int UpdateProduct(Product oldProduct, Product newProduct)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_product", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProductID", SqlDbType.Int);
            cmd.Parameters.Add("@NewProductName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewVendor", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewProductType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewBuyPrice", SqlDbType.Decimal, 19);
            cmd.Parameters["@NewBuyPrice"].Precision = 19;
            cmd.Parameters["@NewBuyPrice"].Scale = 4;
            cmd.Parameters.Add("@NewSalePrice", SqlDbType.Decimal, 19);
            cmd.Parameters["@NewSalePrice"].Precision = 19;
            cmd.Parameters["@NewSalePrice"].Scale = 4;
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);

            cmd.Parameters.Add("@OldProductName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldVendor", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldProductType", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldBuyPrice", SqlDbType.Decimal, 19);
            cmd.Parameters["@OldBuyPrice"].Precision = 19;
            cmd.Parameters["@OldBuyPrice"].Scale = 4;
            cmd.Parameters.Add("@OldSalePrice", SqlDbType.Decimal, 19);
            cmd.Parameters["@OldSalePrice"].Precision = 19;
            cmd.Parameters["@OldSalePrice"].Scale = 4;
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);

            cmd.Parameters["@ProductID"].Value = oldProduct.ProductID;
            cmd.Parameters["@NewProductName"].Value = newProduct.ProductName;
            cmd.Parameters["@NewVendor"].Value = newProduct.Vendor;
            cmd.Parameters["@NewProductType"].Value = newProduct.ProductType;
            cmd.Parameters["@NewBuyPrice"].Value = newProduct.BuyPrice;
            cmd.Parameters["@NewSalePrice"].Value = newProduct.SalePrice;
            cmd.Parameters["@NewQuantity"].Value = newProduct.Quantity;

            cmd.Parameters["@OldProductName"].Value = oldProduct.ProductName;
            cmd.Parameters["@OldVendor"].Value = oldProduct.Vendor;
            cmd.Parameters["@OldProductType"].Value = oldProduct.ProductType;
            cmd.Parameters["@OldBuyPrice"].Value = oldProduct.BuyPrice;
            cmd.Parameters["@OldSalePrice"].Value = oldProduct.SalePrice;
            cmd.Parameters["@OldQuantity"].Value = oldProduct.Quantity;

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

        public int ReactivateProduct(int productID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_product", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", productID);

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

        public List<Product> SelectProductByActive(bool active = true)
        {
            List<Product> products = new List<Product>();
            
            var conn = DBConnection.GetDBConnection();
            
            var cmd = new SqlCommand("sp_select_products_by_active", conn);
            
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
                        var product = new Product()
                        {
                            ProductID = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Vendor = reader.GetString(2),
                            ProductType = reader.GetString(3),
                            BuyPrice = reader.GetDecimal(4),
                            SalePrice = reader.GetDecimal(5),
                            Quantity = reader.GetInt32(6),
                            Active = reader.GetBoolean(7)
                        };
                        products.Add(product);
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
            return products;
        }

        public Product SelectProductById(int ProductID)
        {
            Product product = new Product();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_selectProductByProductID", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductID", ProductID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        product.ProductID = reader.GetInt32(0);
                        product.ProductName = reader.GetString(1);
                        product.Vendor = reader.GetString(2);
                        product.ProductType = reader.GetString(3);
                        product.BuyPrice = reader.GetDecimal(4);
                        product.SalePrice = reader.GetDecimal(5);
                        product.Quantity = reader.GetInt32(6);
                        product.Active = reader.GetBoolean(7);
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
            return product;
        }
    }
}
