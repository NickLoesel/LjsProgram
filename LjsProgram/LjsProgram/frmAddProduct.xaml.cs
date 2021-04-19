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
using DataObjects;

namespace LjsProgram
{
    /// <summary>
    /// Interaction logic for frmAddEditProduct.xaml
    /// </summary>
    public partial class frmAddEditProduct : Window
    {
        private Product _product;
        private bool _addProduct = false;
        private IProductManager _productManager = new ProductManager();
        public frmAddEditProduct()
        {
            _product = new Product();
            _addProduct = true;

            InitializeComponent();
        }

        public frmAddEditProduct(Product product)
        {
            _product = product;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addProduct)
            {
                txtProductID.Text = "Assigned Automatically";
                txtProductID.IsEnabled = false;

                txtProductName.Text = "";
                txtVendorName.Text = "";
                txtProductType.Text = "";
                txtBuyPrice.Text = "";
                txtSalePrice.Text = "";
                txtQuantity.Text = "";
                chkBoxActive.IsChecked = true;

                setupEdit();
                chkBoxActive.IsEnabled = false;

            }

            else
            {
                txtProductID.Text = _product.ProductID.ToString();
                txtProductName.Text = _product.ProductName;
                txtVendorName.Text = _product.Vendor;
                txtProductType.Text = _product.ProductName;
                txtBuyPrice.Text = _product.BuyPrice.ToString();
                txtSalePrice.Text = _product.SalePrice.ToString();
                txtQuantity.Text = _product.Quantity.ToString();
                chkBoxActive.IsChecked = _product.Active ;
            }
        }

        private void btnEditSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (((string)btnEditSaveProduct.Content) == "Edit")
            {
                setupEdit();
            }
            else
            {
                if (_addProduct == false)
                {
                    if (!txtProductName.Text.IsValidProductName())
                    {
                        MessageBox.Show("Invalid Product Name.");
                        txtProductName.Focus();
                        txtProductName.SelectAll();
                        return;
                    }

                    if (!txtVendorName.Text.IsValidVendorName())
                    {
                        MessageBox.Show("Invalid Vendor Name.");
                        txtVendorName.Focus();
                        txtVendorName.SelectAll();
                        return;
                    }

                    if (!txtProductType.Text.IsValidProductType())
                    {
                        MessageBox.Show("Invalid Product Type.");
                        txtProductType.Focus();
                        txtProductType.SelectAll();
                        return;
                    }

                    if (!txtBuyPrice.Text.IsValidBuyPrice())
                    {
                        MessageBox.Show("Invalid Buy price.");
                        txtBuyPrice.Focus();
                        txtBuyPrice.SelectAll();
                        return;
                    }

                    if (!txtSalePrice.Text.IsValidSalePrice())
                    {
                        MessageBox.Show("Invalid Sale Price.");
                        txtSalePrice.Focus();
                        txtSalePrice.SelectAll();
                        return;
                    }

                    if (!txtQuantity.Text.IsValidQuantity())
                    {
                        MessageBox.Show("Please Enter a Positive Number.");
                        txtQuantity.Focus();
                        txtQuantity.SelectAll();
                        return;
                    }

                    var newProduct = new Product()
                    {
                        ProductName = txtProductName.Text,
                        Vendor = txtVendorName.Text,
                        ProductType = txtProductType.Text,
                        BuyPrice = Convert.ToDecimal(txtBuyPrice.Text),
                        SalePrice = Convert.ToDecimal(txtSalePrice.Text),
                        Quantity = Convert.ToInt32(txtQuantity.Text),
                        Active = (bool)chkBoxActive.IsChecked
                    };

                    try
                    {
                        _productManager.UpdateProduct(_product, newProduct);
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
                    if (!txtProductName.Text.IsValidProductName())
                    {
                        MessageBox.Show("Invalid Product Name.");
                        txtProductName.Focus();
                        txtProductName.SelectAll();
                        return;
                    }

                    if (!txtVendorName.Text.IsValidVendorName())
                    {
                        MessageBox.Show("Invalid Vendor Name.");
                        txtVendorName.Focus();
                        txtVendorName.SelectAll();
                        return;
                    }

                    if (!txtProductType.Text.IsValidProductType())
                    {
                        MessageBox.Show("Invalid Product Type.");
                        txtProductType.Focus();
                        txtProductType.SelectAll();
                        return;
                    }

                    if (!txtBuyPrice.Text.IsValidBuyPrice())
                    {
                        MessageBox.Show("Invalid Buy price.");
                        txtBuyPrice.Focus();
                        txtBuyPrice.SelectAll();
                        return;
                    }

                    if (!txtSalePrice.Text.IsValidSalePrice())
                    {
                        MessageBox.Show("Invalid Sale Price.");
                        txtSalePrice.Focus();
                        txtSalePrice.SelectAll();
                        return;
                    }

                    if (!txtQuantity.Text.IsValidQuantity())
                    {
                        MessageBox.Show("Please Enter a Positive Number.");
                        txtQuantity.Focus();
                        txtQuantity.SelectAll();
                        return;
                    }

                    var newProduct = new Product()
                    {
                        ProductName = txtProductName.Text,
                        Vendor = txtVendorName.Text,
                        ProductType = txtProductType.Text,
                        BuyPrice = Convert.ToDecimal(txtBuyPrice.Text),
                        SalePrice = Convert.ToDecimal(txtSalePrice.Text),
                        Quantity = Convert.ToInt32(txtQuantity.Text),
                        Active = (bool)chkBoxActive.IsChecked
                    };

                    try
                    {
                        _productManager.AddNewProduct(newProduct);
                        this.DialogResult = true;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
        }

        private void setupEdit()
        {
            btnEditSaveProduct.Content = "Save";
            txtProductName.IsReadOnly = false;
            txtVendorName.IsReadOnly = false;
            txtProductType.IsReadOnly = false;
            txtBuyPrice.IsReadOnly = false;
            txtSalePrice.IsReadOnly = false;
            txtQuantity.IsReadOnly = false;
            chkBoxActive.IsEnabled = true;
            txtProductName.BorderBrush = Brushes.Black;
            txtVendorName.BorderBrush = Brushes.Black;
            txtProductType.BorderBrush = Brushes.Black;
            txtBuyPrice.BorderBrush = Brushes.Black;
            txtSalePrice.BorderBrush = Brushes.Black;
            txtQuantity.BorderBrush = Brushes.Black;
            txtProductName.Focus();
        }
    }
}
