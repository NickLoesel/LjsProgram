using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LjsProgram
{
    /// <summary>
    /// Interaction logic for frmUpdatePassword.xaml
    /// </summary>
    public partial class frmUpdatePassword : Window
    {
        private User _user;
        private IUserManager _userManager;
        private bool _isNewUser;

        public frmUpdatePassword(User user, IUserManager userManager,
            bool isNewUser = false)
        {
            _user = user;
            _userManager = userManager;
            _isNewUser = isNewUser;

            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!txtEmail.Text.IsValidEmail()
                || txtEmail.Text != _user.Email)
            {
                MessageBox.Show("Invalid Email Address.");
                txtEmail.Clear();
                txtEmail.Focus();
                return;
            }
            if (!pwdOldPassword.Password.IsValidPassword())
            {
                MessageBox.Show("Invalid Password.");
                pwdOldPassword.Clear();
                pwdOldPassword.Focus();
                return;
            }

            if (!pwdNewPassword.Password.IsValidPassword()
                || pwdNewPassword.Password == "newuser")
            {
                MessageBox.Show("Invalid Password.");
                pwdNewPassword.Clear();
                pwdNewPassword.Focus();
                return;
            }
            
            if (!((string)pwdNewPassword.Password ==
                (string)pwdRetypePassword.Password))
            {
                MessageBox.Show("Passwords must match.");
                pwdRetypePassword.Clear();
                pwdRetypePassword.Focus();
                return;
            }

            try
            {
                
                if (_userManager.UpdatePassword(_user, pwdOldPassword.Password,
                    pwdNewPassword.Password))
                {
                    
                    MessageBox.Show("Password Changed.", "Profile Updated",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }
                else
                {
                    
                    throw new ApplicationException("Password not changed.");
                }
            }
            catch (Exception ex)
            {
                pwdNewPassword.Clear();
                pwdRetypePassword.Clear();
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                pwdNewPassword.Focus();
            }




        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isNewUser)
            {
                tbkMessage.Text = "On First Login " + tbkMessage.Text;
                txtEmail.Text = _user.Email;
                txtEmail.IsEnabled = false;
                pwdOldPassword.Password = "newuser";
                pwdOldPassword.IsEnabled = false;
                pwdNewPassword.Focus();
            }

        }
    }
}
