using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjsProgram
{
    public static class CustomerValidationHelpers
    {
        public static bool IsValidBusinessName(this string businessName)
        {
            bool result = false;

            if (businessName.Length <= 50)
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidCustomerFirstName(this string customerFirstName)
        {
            bool result = false;

            if (customerFirstName.Length <= 50)
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidCustomerLastName(this string customerLastName)
        {
            bool result = false;

            if (customerLastName.Length <= 50)
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidCustomerEmail(this string customerEmail)
        {
            bool result = false;

            if (customerEmail.Contains("@") && customerEmail.Length >= 7 &&
                customerEmail.Length <= 100)
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidCustomerPhoneNumber(this string customerPhoneNumber)
        {
            bool result = false;

            if (customerPhoneNumber.Length >= 7 && customerPhoneNumber.Length <= 15)
            {
                result = true;
            }
            return result;
        }
    }
}
