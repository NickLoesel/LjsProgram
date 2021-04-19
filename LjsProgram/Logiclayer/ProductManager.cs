using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class ProductManager : IProductManager
    {
        private IProductAccessor _productAccessor = null;

        public ProductManager()
        {
            _productAccessor = new ProductAccessor();
        }
        public ProductManager(IProductAccessor productAccessor)
        {
            _productAccessor = productAccessor;
        }

        public List<Product> GetProductsByActive(bool active = true)
        {
            List<Product> products = null;
            try
            {
                products = _productAccessor.SelectProductByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Product List unavailable.", ex);
            }
            return products;
        }

        public bool AddNewProduct(Product newProduct)
        {
            bool result = false;
            int newProductID = 0;

            try
            {
                newProductID = _productAccessor.InsertNewProduct(newProduct);
                if(newProductID == 0)
                {
                    throw new ApplicationException("New product was not added.");
                }
                result = true;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Add new product failed.", ex);
            }
            return result;
        }

        public bool UpdateProduct(Product oldProduct, Product newProduct)
        {
            bool result = false;

            try
            {
                result = (1 == _productAccessor.UpdateProduct(oldProduct, newProduct));
                if (result == false)
                {
                    throw new ApplicationException("Product data not changed.");
                }

                if (oldProduct.Active != newProduct.Active)
                {
                    if (newProduct.Active == true)
                    {
                        _productAccessor.ReactivateProduct(oldProduct.ProductID);
                    }
                    else
                    {
                        _productAccessor.DeactivateProduct(oldProduct.ProductID);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }

    }
}
