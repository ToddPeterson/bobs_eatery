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
using EateryLibrary;
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

        int updateID;

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
            tbxEmployeeNumber.Text = employee.EmployeeNumber.ToString();
            updateID = employee.ID;
            btnSubmit.Content = "Edit";
            Tuple<string, string, string> data = DAL.GetCityData(employee.CityID);
            tbxCity.Text = data.Item1;
            tbxZipCode.Text = data.Item2;
            tbxState.Text = data.Item3;
            tbxEmployeePosition.Text = DAL.GetEmployePositionByID(employee.EmployeePositionID);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee newEmployee = new Employee();
                newEmployee.FirstName = tbxFirstName.Text;
                newEmployee.MiddleName = tbxMiddleName.Text;
                newEmployee.LastName = tbxLastName.Text;
                newEmployee.PhoneNumber = tbxPhoneNumber.Text;
                newEmployee.ContactFirstName = tbxContactFirstName.Text;
                newEmployee.ContactMiddleName = tbxContactMiddleName.Text;
                newEmployee.ContactLastName = tbxContactLastName.Text;
                newEmployee.ContactPhoneNumber = tbxContactPhoneNumber.Text;
                newEmployee.Wage = decimal.Parse(tbxWage.Text);
                newEmployee.StreetAddress = tbxStreetAddress.Text;
                newEmployee.EmployeePositionID = DAL.EmployePositionCreate(tbxEmployeePosition.Text);
                newEmployee.EmployeeNumber = int.Parse(tbxEmployeeNumber.Text);
                newEmployee.CityID = DAL.CitiesCreate(tbxCity.Text, int.Parse(tbxZipCode.Text), tbxState.Text);

                if (btnSubmit.Content.ToString() == "Create")
                {
                    Employee employee = DAL.EmployeeCreate(newEmployee);
                    Details details = new Details(employee);
                    details.Show();
                    this.Close();
                }
                else if (btnSubmit.Content.ToString() == "Edit")
                {
                    newEmployee.ID = updateID;
                    DAL.EmployeeUpdate(newEmployee);
                    Details details = new Details(newEmployee);
                    details.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("There were some input errors. Please check the input.");
            }
        }
    }
}
