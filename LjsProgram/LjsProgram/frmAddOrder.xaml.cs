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
using DataObjects;
using LogicLayer;

namespace LjsProgram
{
    /// <summary>
    /// Interaction logic for frmAddOrder.xaml
    /// </summary>
    public partial class frmAddOrder : Window
    {
        private Order _order;
        private bool _addOrder = false;
        private IOrderManager _orderManager = new OrderManager();
        public frmAddOrder()
        {
            _order = new Order();
            _addOrder = true;
            InitializeComponent();
        }
        public frmAddOrder(Order order)
        {
            _order = new Order();
            _addOrder = true;
            InitializeComponent();
        }


        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgCustomerList.ItemsSource = null;
            try
            {
                var customerManager = new CustomerManager();


                if (dgCustomerList.ItemsSource == null)
                {
                    dgCustomerList.ItemsSource = customerManager.GetCustomersByActive();

                    dgCustomerList.Columns[0].Header = "Customer ID";
                    dgCustomerList.Columns[1].Header = "Business Name";
                    dgCustomerList.Columns[2].Header = "First Name";
                    dgCustomerList.Columns[3].Header = "Last Name";
                    dgCustomerList.Columns[4].Header = "Email";
                    dgCustomerList.Columns[5].Header = "Phone Number";
                    dgCustomerList.Columns[6].Header = "Active";

                    dgCustomerList.Columns.RemoveAt(0);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

        }

        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            _addOrder = true;
            if(_addOrder == true)
            {
                if (orderCalender.SelectedDate.HasValue || (Customer)dgCustomerList.SelectedItem == null)
                {
                    DateTime? dateOrNull = orderCalender.SelectedDate;
                    if (dateOrNull != null)
                    {
                        Customer customer = (Customer)dgCustomerList.SelectedItem;
                        DateTime newSelectedDate = dateOrNull.Value;
                        DateTime timespan = DateTime.Parse(txtBoxTime.Text);
                        TimeSpan enteredDate = timespan.TimeOfDay;
                        DateTime formattedUserDate = newSelectedDate + enteredDate;

                        var newOrder = new Order()
                        {
                            OrderDate = formattedUserDate,
                            Active = true
                        };

                       

                        try
                        {
                            int newOrderID = _orderManager.AddNewOrder(newOrder);
                            _orderManager.AddNewCustomerOrder(customer.CustomerID, newOrderID);
                            this.DialogResult = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Please select a date and a customer.");
                }
            }
            
        }

        private void txtBoxTime_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBoxTime.Text = "";
        }
    }
}
