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
    /// Interaction logic for frmAddEditCustomer.xaml
    /// </summary>
    public partial class frmAddEditCustomer : Window
    {
        private Customer _customer;
        private bool _addcustomer = false;
        private ICustomerManager _customerManager = new CustomerManager();
        public frmAddEditCustomer()
        {
            _customer = new Customer();
            _addcustomer = true;

            InitializeComponent();
        }

        public frmAddEditCustomer(Customer customer)
        {
            _customer = customer;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addcustomer)
            {
                txtCustomerID.Text = "Assigned Automatically";
                txtCustomerID.IsEnabled = false;

                txtBusinessName.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtCustomerEmail.Text = "";
                txtCustomerPhoneNumber.Text = "";
                chkBoxActive.IsChecked = true;

                setupEdit();
                chkBoxActive.IsEnabled = false;

            }

            else
            {
                txtCustomerID .Text = _customer.CustomerID.ToString();
                txtBusinessName.Text = _customer.BusinessName;
                txtFirstName.Text = _customer.CustomerFirstName;
                txtLastName.Text = _customer.CustomerLastName;
                txtCustomerEmail.Text = _customer.CustomerEmail;
                txtCustomerPhoneNumber.Text = _customer.CustomerPhoneNumber;
                chkBoxActive.IsChecked = _customer.Active;
            }
        }

        private void btnEditSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (((string)btnEditSaveCustomer.Content) == "Edit")
            {
                setupEdit();
            }
            else
            {
                if (_addcustomer == false)
                {
                    if (!txtBusinessName.Text.IsValidBusinessName())
                    {
                        MessageBox.Show("Invalid Business Name.");
                        txtBusinessName.Focus();
                        txtBusinessName.SelectAll();
                        return;
                    }

                    if (!txtFirstName.Text.IsValidCustomerFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        txtFirstName.Focus();
                        txtFirstName.SelectAll();
                        return;
                    }

                    if (!txtLastName.Text.IsValidCustomerLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        txtLastName.Focus();
                        txtLastName.SelectAll();
                        return;
                    }

                    if (!txtCustomerEmail.Text.IsValidCustomerEmail())
                    {
                        MessageBox.Show("Invalid Email.");
                        txtCustomerEmail.Focus();
                        txtCustomerEmail.SelectAll();
                        return;
                    }

                    if (!txtCustomerPhoneNumber.Text.IsValidCustomerPhoneNumber())
                    {
                        MessageBox.Show("Invalid Phone Number.");
                        txtCustomerPhoneNumber.Focus();
                        txtCustomerPhoneNumber.SelectAll();
                        return;
                    }


                    var newCustomer = new Customer()
                    {
                        BusinessName = txtBusinessName.Text,
                        CustomerFirstName = txtFirstName.Text,
                        CustomerLastName = txtLastName.Text,
                        CustomerEmail = txtCustomerEmail.Text,
                        CustomerPhoneNumber = txtCustomerPhoneNumber.Text,
                        Active = (bool)chkBoxActive.IsChecked
                    };

                    try
                    {
                        _customerManager.UpdateCustomer(_customer, newCustomer);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("deactivated"))
                        {
                            chkBoxActive.IsChecked = true;
                        }
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }

                }
                else
                {
                    if (!txtBusinessName.Text.IsValidBusinessName())
                    {
                        MessageBox.Show("Invalid Business Name.");
                        txtBusinessName.Focus();
                        txtBusinessName.SelectAll();
                        return;
                    }

                    if (!txtFirstName.Text.IsValidCustomerFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        txtFirstName.Focus();
                        txtFirstName.SelectAll();
                        return;
                    }

                    if (!txtLastName.Text.IsValidCustomerLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        txtLastName.Focus();
                        txtLastName.SelectAll();
                        return;
                    }

                    if (!txtCustomerEmail.Text.IsValidCustomerEmail())
                    {
                        MessageBox.Show("Invalid Email.");
                        txtCustomerEmail.Focus();
                        txtCustomerEmail.SelectAll();
                        return;
                    }

                    if (!txtCustomerPhoneNumber.Text.IsValidCustomerPhoneNumber())
                    {
                        MessageBox.Show("Invalid Phone Number.");
                        txtCustomerPhoneNumber.Focus();
                        txtCustomerPhoneNumber.SelectAll();
                        return;
                    }


                    var newCustomer = new Customer()
                    {
                        BusinessName = txtBusinessName.Text,
                        CustomerFirstName = txtFirstName.Text,
                        CustomerLastName = txtLastName.Text,
                        CustomerEmail = txtCustomerEmail.Text,
                        CustomerPhoneNumber = txtCustomerPhoneNumber.Text,
                        Active = (bool)chkBoxActive.IsChecked
                    };

                    try
                    {
                        _customerManager.AddNewCustomer(newCustomer);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
        }

        private void setupEdit()
        {
            btnEditSaveCustomer.Content = "Save";
            txtBusinessName.IsReadOnly = false;
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtCustomerEmail.IsReadOnly = false;
            txtCustomerPhoneNumber.IsReadOnly = false;
            chkBoxActive.IsEnabled = true;
            txtBusinessName.BorderBrush = Brushes.Black;
            txtFirstName.BorderBrush = Brushes.Black;
            txtLastName.BorderBrush = Brushes.Black;
            txtCustomerEmail.BorderBrush = Brushes.Black;
            txtCustomerPhoneNumber.BorderBrush = Brushes.Black;
            txtBusinessName.Focus();
        }

       
    }
}
