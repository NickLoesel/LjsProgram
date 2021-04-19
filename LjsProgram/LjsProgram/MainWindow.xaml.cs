using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LjsProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserManager _userManager = new UserManager();
        private IProductManager _productManager = new ProductManager();
        private IOrderManager _orderManager = new OrderManager();
        private User _user = null;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnLogin.Content == "Login")
            {
                try
                {
                    _user = _userManager.AuthenticateUser(txtUserName.Text, pwdPassword.Password);

                    btnLogin.Content = "Logout";
                    txtUserName.Text = "";

                    if (pwdPassword.Password == "newuser")
                    {
                        var updatePassword = new frmUpdatePassword(_user, _userManager, true);

                        if (!updatePassword.ShowDialog() == true)
                        {
                            resetWindow();
                            _user = null;
                            return;
                        }
                    }

                    btnLogin.IsDefault = false;
                    pwdPassword.Password = "";
                    txtUserName.Visibility = Visibility.Hidden;
                    lblUserName.Visibility = Visibility.Hidden;
                    pwdPassword.Visibility = Visibility.Hidden;
                    lblPassword.Visibility = Visibility.Hidden;
                    sbarItemMessage.Content = "";


                    mnuMain.IsEnabled = true;
                    showUserTabs();
                    dgEmployeeList.ItemsSource = null;
                    try
                    {
                        var employeeManager = new EmployeeManager();

                        if (dgEmployeeList.ItemsSource == null)
                        {
                            dgEmployeeList.ItemsSource = employeeManager.RetrieveEmployeesByActive();

                            dgEmployeeList.Columns.Remove(dgEmployeeList.Columns[0]);
                            dgEmployeeList.Columns[0].Header = "Employee ID";
                            dgEmployeeList.Columns[1].Header = "Email Address";
                            dgEmployeeList.Columns[2].Header = "First Name";
                            dgEmployeeList.Columns[3].Header = "Last Name";
                            dgEmployeeList.Columns[4].Header = "Phone Number";
                            dgEmployeeList.Columns[5].Header = "Active";

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }



                    sbarItemMessage.Content = "Welcome back, " + _user.FirstName;
                    if (_user.Roles.Count > 0)
                    {
                        var roleString = _user.Roles[0];
                        for (int i = 1; i < _user.Roles.Count; i++)
                        {
                            roleString += ", " + _user.Roles[i];
                        }

                        sbarItemMessage.Content = "You are logged in as: " + roleString;
                    }
                    
                    else
                    {
                        sbarItemMessage.Content = "You have not yet been assigned a role.";
                    }
                }
                catch (Exception ex)
                {
                    pwdPassword.Clear();
                    txtUserName.Clear();
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    txtUserName.Focus();
                }
            }
            else
            {
                _user = null;

                resetWindow();
                btnLogin.IsDefault = true;


            }
        }
        private void hideAllTabs()
        {
            foreach (TabItem t in tabSetMain.Items)
            {
                t.Visibility = Visibility.Collapsed;
            }
            grdUserAdmin.Visibility = Visibility.Hidden;
            grdInventory.Visibility = Visibility.Hidden;
            grdCustomers.Visibility = Visibility.Hidden;
            grdOrders.Visibility = Visibility.Hidden;

        }
        private void showUserTabs()
        {
            
            foreach (var r in _user.Roles)
            {
                switch (r)
                {
                    case "Admin":
                        grdOrders.Visibility = Visibility.Visible;
                        tabOrders.Visibility = Visibility.Visible;
                        grdUserAdmin.Visibility = Visibility.Visible;
                        tabUserAdmin.Visibility = Visibility.Visible;
                        grdInventory.Visibility = Visibility.Visible;
                        tabInventory.Visibility = Visibility.Visible;
                        grdCustomers.Visibility = Visibility.Visible;
                        tabCustomers.Visibility = Visibility.Visible;
                        
                        tabUserAdmin.IsSelected = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private void resetWindow()
        {
            btnLogin.IsDefault = true;
            hideAllTabs();
            mnuMain.IsEnabled = false;
            txtUserName.Text = "";
            pwdPassword.Password = "";
            btnLogin.Content = "Login";
            sbarItemMessage.Content = "Greetings.";
            sbarItemMessage.Content = "You are not logged in.";
            txtUserName.Visibility = Visibility.Visible;
            lblUserName.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            sbarItemMessage.Content = "Please login to continue.";

            dgEmployeeList.ItemsSource = null;
            dgCustomerList.ItemsSource = null;
            dgOrderList.ItemsSource = null;
            dgInventoryList.ItemsSource = null;

            txtUserName.Focus();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            resetWindow();
        }
        private void tabUserAdmin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    var employeeManager = new EmployeeManager();

                    if (dgEmployeeList.ItemsSource == null)
                    {
                        dgEmployeeList.ItemsSource = employeeManager.RetrieveEmployeesByActive();

                        dgEmployeeList.Columns.Remove(dgEmployeeList.Columns[0]);
                        dgEmployeeList.Columns[0].Header = "Employee ID";           
                        dgEmployeeList.Columns[1].Header = "Email Address";
                        dgEmployeeList.Columns[2].Header = "First Name";
                        dgEmployeeList.Columns[3].Header = "Last Name";
                        dgEmployeeList.Columns[4].Header = "Phone Number";
                        dgEmployeeList.Columns[5].Header = "Active";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
        private void dgEmployeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editEmployee();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addEditWindow = new frmEmployeeAddEdit();
            if (addEditWindow.ShowDialog() == true)
            {
                var employeeManager = new EmployeeManager();
                dgEmployeeList.ItemsSource =
                    employeeManager.RetrieveEmployeesByActive((bool)chkShowActive.IsChecked);

                dgEmployeeList.Columns.Remove(dgEmployeeList.Columns[0]);
                dgEmployeeList.Columns[0].Header = "Employee ID";
                dgEmployeeList.Columns[1].Header = "Email Address";
                dgEmployeeList.Columns[2].Header = "First Name";
                dgEmployeeList.Columns[3].Header = "Last Name";
                dgEmployeeList.Columns[4].Header = "Phone Number";
                dgEmployeeList.Columns[5].Header = "Active";
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (EmployeeViewModel)dgEmployeeList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select an employee to edit!", "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            editEmployee();
        }

        private void editEmployee()
        {
            var selectedItem = (EmployeeViewModel)dgEmployeeList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }

            var addEditWindow = new frmEmployeeAddEdit(selectedItem);
            if (addEditWindow.ShowDialog() == true)
            {
                var employeeManager = new EmployeeManager();
                dgEmployeeList.ItemsSource =
                    employeeManager.RetrieveEmployeesByActive((bool)chkShowActive.IsChecked);


                dgEmployeeList.Columns.Remove(dgEmployeeList.Columns[0]);
                dgEmployeeList.Columns[0].Header = "Employee ID";
                dgEmployeeList.Columns[1].Header = "Email Address";
                dgEmployeeList.Columns[2].Header = "First Name";
                dgEmployeeList.Columns[3].Header = "Last Name";
                dgEmployeeList.Columns[4].Header = "Phone Number";
                dgEmployeeList.Columns[5].Header = "Active";
            }
        }

        private void tabInventory_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    var productManager = new ProductManager();

                    if (dgInventoryList.ItemsSource == null)
                    {
                        dgInventoryList.ItemsSource = productManager.GetProductsByActive();

                        dgInventoryList.Columns[0].Header = "Product ID";
                        dgInventoryList.Columns[1].Header = "Product Name";
                        dgInventoryList.Columns[2].Header = "Vendor";
                        dgInventoryList.Columns[3].Header = "Product Type";
                        dgInventoryList.Columns[4].Header = "Buy Price";
                        dgInventoryList.Columns[5].Header = "Sale Price";
                        dgInventoryList.Columns[6].Header = "Quantity";
                        dgInventoryList.Columns[7].Header = "Active";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
        private void dgInventoryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editProduct();
        }

        private void editProduct()
        {
            var selectedItem = (Product)dgInventoryList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }



            var frmAddEditProduct = new frmAddEditProduct (selectedItem);
            if (frmAddEditProduct.ShowDialog() == true)
            {
                var productManager = new ProductManager();
                dgInventoryList.ItemsSource =
                    productManager.GetProductsByActive((bool)chkShowActive.IsChecked);


                dgInventoryList.Columns[0].Header = "Product ID";
                dgInventoryList.Columns[1].Header = "Product Name";
                dgInventoryList.Columns[2].Header = "Vendor";
                dgInventoryList.Columns[3].Header = "Product Type";
                dgInventoryList.Columns[4].Header = "Buy Price";
                dgInventoryList.Columns[5].Header = "Sale Price";
                dgInventoryList.Columns[6].Header = "Quantity";
                dgInventoryList.Columns[7].Header = "Active";
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var frmAddEditProduct = new frmAddEditProduct();
            if (frmAddEditProduct.ShowDialog() == true)
            {
                var productManager = new ProductManager();

                dgInventoryList.ItemsSource = productManager.GetProductsByActive((bool)chkShowActiveProducts.IsChecked);

                dgInventoryList.Columns[0].Header = "Product ID";
                dgInventoryList.Columns[1].Header = "Product Name";
                dgInventoryList.Columns[2].Header = "Vendor";
                dgInventoryList.Columns[3].Header = "Product Type";
                dgInventoryList.Columns[4].Header = "Buy Price";
                dgInventoryList.Columns[5].Header = "Sale Price";
                dgInventoryList.Columns[6].Header = "Quantity";
                dgInventoryList.Columns[7].Header = "Active";
            }
        }

        private void chkShowActiveProducts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var productManager = new ProductManager();
                dgInventoryList.ItemsSource = productManager.GetProductsByActive((bool)chkShowActiveProducts.IsChecked);

                dgInventoryList.Columns[0].Header = "Product ID";
                dgInventoryList.Columns[1].Header = "Product Name";
                dgInventoryList.Columns[2].Header = "Vendor";
                dgInventoryList.Columns[3].Header = "Product Type";
                dgInventoryList.Columns[4].Header = "Buy Price";
                dgInventoryList.Columns[5].Header = "Sale Price";
                dgInventoryList.Columns[6].Header = "Quantity";
                dgInventoryList.Columns[7].Header = "Active";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void chkShowActive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employeeManager = new EmployeeManager();
                dgEmployeeList.ItemsSource =
                    employeeManager.RetrieveEmployeesByActive((bool)chkShowActive.IsChecked);

                dgEmployeeList.Columns.Remove(dgEmployeeList.Columns[0]);
                dgEmployeeList.Columns[0].Header = "Employee ID";
                dgEmployeeList.Columns[1].Header = "Email Address";
                dgEmployeeList.Columns[2].Header = "First Name";
                dgEmployeeList.Columns[3].Header = "Last Name";
                dgEmployeeList.Columns[4].Header = "Phone Number";
                dgEmployeeList.Columns[5].Header = "Active";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Product)dgInventoryList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select a Product to edit!", "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            editProduct();
        }
        

        private void tabOrders_GotFocus(object sender, RoutedEventArgs e)
        {
            // if the user is allowed to see the tab
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    // try and instantiate an order manager
                    var orderManager = new OrderManager();

                    // if the item source is null
                    if (dgOrderList.ItemsSource == null)
                    {
                        // assign the items source to list of active orders
                        dgOrderList.ItemsSource = orderManager.GetOrdersByActive();

                        dgOrderList.Columns[0].Header = "Order ID";
                        dgOrderList.Columns[1].Header = "Order Date";
                        dgOrderList.Columns[2].Header = "Active";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void tabCustomers_GotFocus(object sender, RoutedEventArgs e)
        {
            // if the user is allowed to see the tab
            if (((TabItem)sender).Visibility == Visibility.Visible)
            {
                try
                {
                    // try and instantiate a customer manager
                    var customerManager = new CustomerManager();

                    // if the item source is null
                    if (dgCustomerList.ItemsSource == null)
                    {
                        // assign the items source to list of active customers
                        dgCustomerList.ItemsSource = customerManager.GetCustomersByActive();

                        // assign the columns to the appropriate headers
                        dgCustomerList.Columns[0].Header = "Customer ID";
                        dgCustomerList.Columns[1].Header = "Business Name";
                        dgCustomerList.Columns[2].Header = "First Name";
                        dgCustomerList.Columns[3].Header = "Last Name";
                        dgCustomerList.Columns[4].Header = "Email";
                        dgCustomerList.Columns[5].Header = "Phone Number";
                        dgCustomerList.Columns[6].Header = "Active";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void dgCustomerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            displayConnectedOrders();
        }

        private void displayConnectedOrders()
        {
            // instantiate new customer manager and order manager
            var customerManager = new CustomerManager();
            var orderManager = new OrderManager();
            
            // store the selected item of the user
            Customer customer = (Customer)dgCustomerList.SelectedItem;
         
                // store the customers name
                string customerName = (customer.CustomerFirstName + " " + customer.CustomerLastName);

                // get the connected order by the customer ID
                List<Order> customerOrder = customerManager.GetOrdersByCustomerID(customer.CustomerID);

                
                if (customerOrder.Count == 0)
                {
                MessageBox.Show("There are no orders for " + customerName);
                }

                // for each order in the customer order loop through and get the full order by the order id
                foreach (var i in customerOrder)
                {
                    // add that order to the order list
                    List<Order> FullCustomerOrder = orderManager.GetOrdersByOrderID(i.OrderID);
                    foreach (var order in FullCustomerOrder)
                    {
                        // for each order show the user the customer has an order for the retrieved date
                        MessageBox.Show(customerName +  " has an order for " + order.OrderDate.ToString());
                    
                    }
                }

            
            
            
        }

        private void mnuItemLogout_Click(object sender, RoutedEventArgs e)
        {
            

            if (_user != null)
            {
                // store the user message
                string message = "Are you sure you want to logout?";

                // ask the user if they're sure they want to logout
                MessageBoxResult result = MessageBox.Show(message, "Log Out",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // log the user out
                    resetWindow();
                }
                else
                {
                    // close the window
                    return;
                }

            }
            else
            {
                // if null show that the users not logged in
                MessageBox.Show("You are not logged in.");
            }   
        }

        private void mnuItemUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            // checks to see if the user is not null
            if (_user != null)
            {
                // if not opens the update password window
                var updatePassword = new frmUpdatePassword(_user, _userManager);
                updatePassword.ShowDialog();
            }
            else
            {
                // if null show that the users not logged in
                MessageBox.Show("You are not logged in.");
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var frmAddOrder = new frmAddOrder();

            if (frmAddOrder.ShowDialog() == true)
            {
                MessageBox.Show("Order Added Succsessfully.");

                
            }

        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var frmcustomerAddEdit = new frmAddEditCustomer();
            if (frmcustomerAddEdit.ShowDialog() == true)
            {
                var customerManager = new CustomerManager();
                dgCustomerList.ItemsSource =
                    customerManager.GetCustomersByActive((bool)chkShowActive.IsChecked);

                dgCustomerList.Columns[0].Header = "Customer ID";
                dgCustomerList.Columns[1].Header = "Business Name";
                dgCustomerList.Columns[2].Header = "First Name";
                dgCustomerList.Columns[3].Header = "Last Name";
                dgCustomerList.Columns[4].Header = "Email";
                dgCustomerList.Columns[5].Header = "Phone Number";
                dgCustomerList.Columns[6].Header = "Active";
            }
        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Customer)dgCustomerList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select a Customer to edit!", "Invalid Operation",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            editCustomer();
        }

        private void editCustomer()
        {
            var selectedItem = (Customer)dgCustomerList.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }



            var frmAddEditCustomer = new frmAddEditCustomer(selectedItem);
            if (frmAddEditCustomer.ShowDialog() == true)
            {
                var productManager = new CustomerManager();
                dgCustomerList.ItemsSource =
                    productManager.GetCustomersByActive((bool)chkShowActive.IsChecked);


                dgCustomerList.Columns[0].Header = "Customer ID";
                dgCustomerList.Columns[1].Header = "Business Name";
                dgCustomerList.Columns[2].Header = "First Name";
                dgCustomerList.Columns[3].Header = "Last Name";
                dgCustomerList.Columns[4].Header = "Email";
                dgCustomerList.Columns[5].Header = "Phone Number";
                dgCustomerList.Columns[6].Header = "Active";
            }
        }
    }
}
