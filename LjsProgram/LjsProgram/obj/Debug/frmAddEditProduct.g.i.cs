// Updated by XamlIntelliSenseFileGenerator 12/7/2020 10:16:46 PM
#pragma checksum "..\..\frmAddEditProduct.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6C0F89CF5E535704EC25522A96BFEC17F0D222B2ACE9D22581472D242AA4EF8A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LjsProgram;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace LjsProgram
{


    /// <summary>
    /// frmAddEditProduct
    /// </summary>
    public partial class frmAddEditProduct : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LjsProgram;component/frmaddeditproduct.xaml", System.UriKind.Relative);

#line 1 "..\..\frmAddEditProduct.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBox txtFirstName;
        internal System.Windows.Controls.TextBox txtLastName;
        internal System.Windows.Controls.TextBox txtEmail;
        internal System.Windows.Controls.TextBox txtPhoneNumber;
        internal System.Windows.Controls.CheckBox chkActive;
        internal System.Windows.Controls.ListBox lstAssignedRoles;
        internal System.Windows.Controls.ListBox lstUnassignedRoles;
        internal System.Windows.Controls.Button btnEditSave;
        internal System.Windows.Controls.Button btnCancel;
        internal System.Windows.Controls.TextBlock txtBlockTitle;
        internal System.Windows.Controls.TextBox txtProductID;
    }
}

