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
    /// Interaction logic for frmEmployeeAddEdit.xaml
    /// </summary>
    public partial class frmEmployeeAddEdit : Window
    {
        private EmployeeViewModel _employee;
        private List<string> _originalUnassignedRoles = new List<string>();
        private bool _addUser = false;
        private IEmployeeManager _employeeManager = new EmployeeManager();
        private List<string> _assignedRoles;
        private List<string> _unassignedRoles;

        public frmEmployeeAddEdit()
        {
            _employee = new EmployeeViewModel();
            _addUser = true;

            InitializeComponent();
        }
        public frmEmployeeAddEdit(EmployeeViewModel employee)
        {
            _employee = employee;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addUser)
            {
                txtEmployeeID.Text = "Assigned Automatically";
                txtEmployeeID.IsEnabled = false;

                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";
                txtPhoneNumber.Text = "";
                chkActive.IsChecked = true;

                try    
                {
                    _assignedRoles = new List<string>();
                    _unassignedRoles = _employeeManager.RetrieveAllRoles();
                    lstAssignedRoles.ItemsSource = _assignedRoles;
                    lstUnassignedRoles.ItemsSource = _unassignedRoles;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                setupEdit();
                chkActive.IsEnabled = false;

            }
            else 
            {
                txtEmployeeID.Text = _employee.EmployeeID.ToString();
                txtFirstName.Text = _employee.FirstName;
                txtLastName.Text = _employee.LastName;
                txtEmail.Text = _employee.Email;
                txtPhoneNumber.Text = _employee.PhoneNumber;
                chkActive.IsChecked = _employee.Active;

                resetRoles();
            }
        }

        private void resetRoles()
        {
            try
            {
                _assignedRoles = _employeeManager.RetrieveRolesByEmployeeID(_employee.EmployeeID);
                _employee.Roles = new List<string>();
                foreach (var r in _assignedRoles)
                {
                    _employee.Roles.Add(r);
                }
                _unassignedRoles = _employeeManager.RetrieveAllRoles();
                foreach (var role in _assignedRoles)
                {
                    _unassignedRoles.Remove(role);
                }
                foreach (var r in _unassignedRoles)
                {
                    _originalUnassignedRoles.Add(r);
                }

                lstAssignedRoles.ItemsSource = _assignedRoles;
                lstUnassignedRoles.ItemsSource = _unassignedRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (((string)btnEditSave.Content) == "Edit")
            {
                setupEdit();
            }
            else
            {
                if (_addUser == false)
                {

                    if (!txtFirstName.Text.IsValidFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        txtFirstName.Focus();
                        txtFirstName.SelectAll();
                        return;
                    }
                    if (!txtLastName.Text.IsValidLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        txtLastName.Focus();
                        txtLastName.SelectAll();
                        return;
                    }
                    if (!txtEmail.Text.IsValidEmail())
                    {
                        MessageBox.Show("Bad email address.");
                        txtEmail.Focus();
                        txtEmail.SelectAll();
                        return;
                    }
                    if (!txtPhoneNumber.Text.IsValidPhoneNumber())
                    {
                        MessageBox.Show("Invalid Phone Number.");
                        txtPhoneNumber.Focus();
                        txtPhoneNumber.SelectAll();
                        return;
                    }

                    var newEmployee = new EmployeeViewModel()
                    {
                        Email = txtEmail.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        PhoneNumber = txtPhoneNumber.Text,
                        Active = (bool)chkActive.IsChecked
                    };
                    List<string> roles = new List<string>();
                    foreach (var item in lstAssignedRoles.Items)
                    {
                        roles.Add((string)item);
                    }
                    newEmployee.Roles = roles;

                    try
                    {
                        _employeeManager.EditEmployeeProfile(_employee, newEmployee,
                            _originalUnassignedRoles, _unassignedRoles);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        resetRoles();
                        if (ex.InnerException.Message.Contains("deactivated"))
                        {
                            chkActive.IsChecked = true;
                        }
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    if (!txtFirstName.Text.IsValidFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        txtFirstName.Focus();
                        txtFirstName.SelectAll();
                        return;
                    }
                    if (!txtLastName.Text.IsValidLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        txtLastName.Focus();
                        txtLastName.SelectAll();
                        return;
                    }
                    if (!txtEmail.Text.IsValidEmail())
                    {
                        MessageBox.Show("Bad email address.");
                        txtEmail.Focus();
                        txtEmail.SelectAll();
                        return;
                    }
                    if (!txtPhoneNumber.Text.IsValidPhoneNumber())
                    {
                        MessageBox.Show("Invalid Phone Number.");
                        txtPhoneNumber.Focus();
                        txtPhoneNumber.SelectAll();
                        return;
                    }

                    var newEmployee = new EmployeeViewModel()
                    {
                        Email = txtEmail.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        PhoneNumber = txtPhoneNumber.Text,
                        Active = (bool)chkActive.IsChecked
                    };
                    List<string> roles = new List<string>();
                    foreach (var item in lstAssignedRoles.Items)
                    {
                        roles.Add((string)item);
                    }
                    newEmployee.Roles = roles;

                    try
                    {
                        _employeeManager.AddNewEmployee(newEmployee);
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
            btnEditSave.Content = "Save";
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtPhoneNumber.IsReadOnly = false;
            chkActive.IsEnabled = true;
            lstAssignedRoles.IsEnabled = true;
            lstUnassignedRoles.IsEnabled = true;
            txtFirstName.BorderBrush = Brushes.Black;
            txtLastName.BorderBrush = Brushes.Black;
            txtEmail.BorderBrush = Brushes.Black;
            txtPhoneNumber.BorderBrush = Brushes.Black;
            txtFirstName.Focus();
        }

        private void lstAssignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstAssignedRoles.SelectedItem;
            _assignedRoles.Remove((string)selectedRole);
            _unassignedRoles.Add((string)selectedRole);

            lstAssignedRoles.ItemsSource = null;
            lstUnassignedRoles.ItemsSource = null;

            lstAssignedRoles.ItemsSource = _assignedRoles;
            lstUnassignedRoles.ItemsSource = _unassignedRoles;
        }

        private void lstUnassignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = lstUnassignedRoles.SelectedItem;
            _unassignedRoles.Remove((string)selectedRole);
            _assignedRoles.Add((string)selectedRole);

            lstAssignedRoles.ItemsSource = null;
            lstUnassignedRoles.ItemsSource = null;

            lstAssignedRoles.ItemsSource = _assignedRoles;
            lstUnassignedRoles.ItemsSource = _unassignedRoles;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
