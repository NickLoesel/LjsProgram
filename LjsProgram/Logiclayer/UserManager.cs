using DataAccessLayer;
using DataObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        
        private IUserAccessor userAccessor;
        public UserManager()
        {
            userAccessor = new UserAccessor();
        }
        public UserManager(IUserAccessor suppliedUserAccessor)
        {
            userAccessor = suppliedUserAccessor;
        }

        public User AuthenticateUser(string email, string password)
        {
            
            User user = null;

            
            password = hashSHA256(password);

            bool isNewUser = (password == "newuser");

            
            try
            {
                
                if (1 == userAccessor.VerifyUserNameAndPassword(email, password))
                {
                    
                    user = userAccessor.SelectUserByEmail(email);
                }
                else
                {
                    throw new ApplicationException("Bad Username or Password");
                }
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login Failed.", ex);
            }
            return user;
        }

        public bool UpdatePassword(User user, string oldPassword, string newPassword)
        {
            bool result = false;

            oldPassword = oldPassword.SHA256Value();
            newPassword = newPassword.SHA256Value();

            try
            {
                result = (1 == userAccessor.UpdatePasswordHash(
                    user.Email, newPassword, oldPassword));

                if (result == false)
                {
                    throw new ApplicationException("Update Failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Bad username or password.", ex);
            }

            return result;
        }

        
        private string hashSHA256(string source)
        {
            string result = "";
            byte[] data;

            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();

            return result;
        }
    }
}


