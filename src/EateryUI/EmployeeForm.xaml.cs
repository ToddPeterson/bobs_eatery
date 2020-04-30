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
    /// Interaction logic for EmployeeForm.xaml
    /// </summary>
    public partial class EmployeeForm : Window
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        public EmployeeForm(Employee employee)
        {
            InitializeComponent();
            tbxFirstName.Text = employee.FirstName;
            tbxMiddleName.Text = employee.MiddleName;
            tbxLastName.Text = employee.LastName;
            tbxPhoneNumber.Text = employee.PhoneNumber;
            tbxContactFirstName.Text = employee.ContactFirstName;
            tbxContactMiddleName.Text = employee.ContactMiddleName;
            tbxContactLastName.Text = employee.ContactLastName;
            tbxContactPhoneNumber.Text = employee.ContactPhoneNumber;
            tbxWage.Text = employee.Wage.ToString();
            tbxStreetAddress.Text = employee.StreetAddress;
            tbxCityID.Text = employee.CityID.ToString();
            tbxEmployeePositionID.Text = employee.EmployeePositionID.ToString();
            tbxEmployeeNumber.Text = employee.EmployeeNumber.ToString();

            btnSubmit.Content = "Edit";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
