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
using EateryLibrary.Models;

namespace EateryUI
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        public CustomerForm(Customer customer)
        {
            InitializeComponent();
            tbxFirstName.Text = customer.FirstName;
            tbxMiddleName.Text = customer.MiddleName;
            tbxLastName.Text = customer.LastName;
            tbxPhoneNumber.Text = customer.PhoneNumber;
            tbxCustomerNumber.Text = customer.CustomerNumber.ToString();
            tbxStreetAddress.Text = customer.StreetAddress;
            tbxCityID.Text = customer.CityID.ToString();
            tbxEmail.Text = customer.Email;

            btnSubmit.Content = "Edit";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
