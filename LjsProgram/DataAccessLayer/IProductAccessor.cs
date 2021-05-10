using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IProductAccessor
    {
        int InsertNewProduct(Product product);
        List<Product> SelectProductByActive(bool active = true);
        int UpdateProduct(Product oldProduct, Product newProduct);
        int ReactivateProduct(int productID);
        int DeactivateProduct(int productID);
        Product SelectProductById(int ProductID);
    }
}
