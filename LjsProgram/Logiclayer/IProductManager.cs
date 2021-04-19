using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IProductManager
    {
        List<Product> GetProductsByActive(bool active = true);
        bool UpdateProduct(Product oldProduct, Product newProduct);
        bool AddNewProduct(Product newProduct);
    }
}
