using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjsProgram
{
    public static class EmployeeValidationHelpers
    {
        public static bool IsValidEmail(this string email)
        {
            bool result = false;

            if (email.Contains("@") && email.Length >= 7 &&
                email.Length <= 100)
            {
                result = true;
            }

            return result;
        }

        public static bool IsValidPassword(this string password)
        {
            bool result = false;

            if (password.Length >= 7)
            {
                result = true;
            }
            return result;
        }

        public static bool IsValidFirstName(this string firstName)
        {
            bool result = false;

            if (firstName.Length >= 1 && firstName.Length <= 50)
            {
                result = true;
            }
            return result;
        }
        public static bool IsValidLastName(this string lastName)
        {
            bool result = false;

            if (lastName.Length >= 1 && lastName.Length <= 50)
            {
                result = true;
            }
            return result;
        }
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            bool result = false;

            if (phoneNumber.Length >= 7 && phoneNumber.Length <= 15)
            {
                result = true;
            }
            return result;
        }

    }
}
