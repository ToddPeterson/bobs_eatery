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
            customer.FirstName = tbxFirstName.Text;
            customer.MiddleName = tbxMiddleName.Text;
            customer.LastName = tbxLastName.Text;
            customer.PhoneNumber = tbxPhoneNumber.Text;
            customer.CustomerNumber = int.Parse(tbxCustomerNumber.Text);
            customer.StreetAddress = tbxStreetAddress.Text;
            customer.CityID = int.Parse(tbxCityID.Text);
            customer.Email = tbxEmail.Text;

            btnSubmit.Content = "Edit";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
