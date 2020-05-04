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
using EateryLibrary;
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

        private int ID;

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

            ID = customer.ID;

            btnSubmit.Content = "Edit";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Customer cus = new Customer();

            if (!int.TryParse(tbxCustomerNumber.Text, out int customerNumber))
            {
                MessageBox.Show("Invalid customer number");
                return;
            }

            if (!int.TryParse(tbxCityID.Text, out int cityID))
            {
                MessageBox.Show("Invalid City ID");
                return;
            }

            cus.FirstName = tbxFirstName.Text;
            cus.MiddleName = tbxMiddleName.Text;
            cus.LastName = tbxLastName.Text;
            cus.PhoneNumber = tbxPhoneNumber.Text;
            cus.CustomerNumber = customerNumber;
            cus.StreetAddress = tbxStreetAddress.Text;
            cus.CityID = cityID;
            cus.Email = tbxEmail.Text;

            if (btnSubmit.Content.ToString() == "Create")
            {
                DAL.CustomerAdd(cus);
            }
            else if (btnSubmit.Content.ToString() == "Edit")
            {
                cus.ID = ID;
                DAL.CustomerUpdate(cus);
            }

            Details details = new Details(cus);
            details.Show();
            this.Close();
        }
    }
}
