using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjsProgram
{
    public static class ProductValidationHelpers
    {
        public static bool IsValidProductName(this string productName)
        {
            bool result = false;

            if(productName.Length < 100 && productName.Length > 1)
            {
                result = true;
            }

            return result;
        }
        public static bool IsValidVendorName(this string vendor)
        {
            bool result = false;

            if(vendor.Length < 250 && vendor.Length > 0)
            {
                result = true;
            }

            return result;
        }
        public static bool IsValidProductType(this string productType)
        {
            bool result = false;

            if(productType.Length < 50 && productType.Length > 1)
            {
                result = true;
            }

            return result;
        }
        public static bool IsValidBuyPrice(this string buyPrice)
        {
            bool result = false;

            if (buyPrice.Contains("."))
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidSalePrice(this string salePrice)
        {
            bool result = false;

            if (salePrice.Contains("."))
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidQuantity(this string quantity)
        {
            bool result = false;

            if(quantity.Length > 0)
            {
                result = true;
            }

            return result;
        }
    }
}
