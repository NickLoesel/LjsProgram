using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;
using LjsProgram;
using System.Globalization;
using System.Net;

namespace PresentationMVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductManager _productManager = new ProductManager();
        // GET: Product
        public ActionResult Products()
        {
            ViewBag.Active = true;
            try
            {
                return View(_productManager.GetProductsByActive(true));
            }
            catch (Exception ex)
            {

                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult InactiveProducts()
        {
            return View(_productManager.GetProductsByActive(false));
        }

        public ActionResult Details(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ProductID = productID;
            return View(_productManager.SelectProductById((int)productID));
        }

        public ActionResult Edit(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                return View(_productManager.SelectProductById((int)productID));
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        public ActionResult Reactivate(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product oldproduct = new Product();
            Product newproduct = new Product();
            bool result = false;
            try
            {
                oldproduct = _productManager.SelectProductById((int)productID);
                newproduct = _productManager.SelectProductById((int)productID);
                newproduct.Active = !newproduct.Active;
                result = _productManager.UpdateProduct(oldproduct, newproduct);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            return View("Products", _productManager.GetProductsByActive(true));
        }


        public ActionResult Deactivate(int? productID)
        {
            if (productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product oldproduct = new Product();
            Product newproduct = new Product();
            bool result = false;
            try
            {
                oldproduct = _productManager.SelectProductById((int)productID);
                newproduct = _productManager.SelectProductById((int)productID);
                newproduct.Active = !newproduct.Active;
                result = _productManager.UpdateProduct(oldproduct, newproduct);
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
            return View("InactiveProducts", _productManager.GetProductsByActive(false));
        }

        [HttpPost]
        public ActionResult CreateProduct(Product model)
        {
            try
            {
                if (!model.ProductName.IsValidProductName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.ProductName + "Is not a valid Product Name (100 characters max)"
                    });
                }
                if (!model.Vendor.IsValidVendorName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.Vendor + "Is not a valid vendor name (250 characters max)"
                    });
                }
                if (!model.ProductType.IsValidProductType())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.ProductType + "Is not a valid product type (50 characters max)"
                    });
                }
                if (!model.BuyPrice.ToString(CultureInfo.InvariantCulture).IsValidBuyPrice())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.BuyPrice + "Is not a valid buy price (The decimal should be in the form '00.00')"
                    });
                }
                if (!model.SalePrice.ToString(CultureInfo.InvariantCulture).IsValidSalePrice())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.SalePrice + "Is not a valid sale price (The decimal should be in the form '00.00')"
                    });
                }
                if (!model.Quantity.ToString(CultureInfo.InvariantCulture).IsValidQuantity())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        model.Quantity + "Is not a valid quantity (It should be a number)"
                    });
                }

                Product newProduct = new Product()
                {
                    ProductName = model.ProductName,
                    Vendor = model.Vendor,
                    ProductType = model.ProductType,
                    BuyPrice = model.BuyPrice,
                    SalePrice = model.SalePrice,
                    Quantity = model.Quantity,
                    Active = true
                };

                _productManager.AddNewProduct(newProduct);
                return RedirectToAction("Products", "Product");

            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                if (ex.InnerException.Message.Contains("error"))
                {
                    error = "Could Not add product "
                        + model.ProductName;
                }
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }

        [HttpPost]
        public ActionResult EditProduct(Product newproduct)
        {
            try
            {
                if (!newproduct.ProductName.IsValidProductName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newproduct.ProductName + "Is not a valid Product Name (100 characters max)"
                    });
                }
                if (!newproduct.Vendor.IsValidVendorName())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newproduct.Vendor + "Is not a valid vendor name (250 characters max)"
                    });
                }
                if (!newproduct.ProductType.IsValidProductType())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newproduct.ProductType + "Is not a valid product type (50 characters max)"
                    });
                }
                if (!newproduct.BuyPrice.ToString(CultureInfo.InvariantCulture).IsValidBuyPrice())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newproduct.BuyPrice + "Is not a valid buy price (The decimal should be in the form '00.00')"
                    });
                }
                if (!newproduct.SalePrice.ToString(CultureInfo.InvariantCulture).IsValidSalePrice())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newproduct.SalePrice + "Is not a valid sale price (The decimal should be in the form '00.00')"
                    });
                }
                if (!newproduct.Quantity.ToString(CultureInfo.InvariantCulture).IsValidQuantity())
                {
                    return RedirectToAction("Error", "Home", new
                    {
                        errorMessage =
                        newproduct.Quantity + "Is not a valid quantity (It should be a number)"
                    });
                }
                Product oldProduct = _productManager.SelectProductById(newproduct.ProductID);

                _productManager.UpdateProduct(oldProduct, newproduct);

                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                string error = ex.Message + "\n\n" + ex.InnerException.Message;
                return RedirectToAction("Error", "Home", new { errorMessage = error });
            }
        }
    }
}