using EateryLibrary;
using EateryLibrary.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace EateryUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();
            List<Customer> customers = DAL.CustomerGetAll();
            foreach(Customer customer in customers)
            {
                lbxOutput.Items.Add(customer);
            }
        }

        private void BtnShowEmployees_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();
            List<Employee> employees = DAL.EmployeesGetAll();
            foreach (Employee employee in employees)
            {
                lbxOutput.Items.Add(employee);
            }
        }

        private void LbxOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbxOutput.SelectedIndex >= 0)
            {
                // Took help from the Db Learn Project assignment
                object selectedItem = lbxOutput.SelectedItem;
                if (selectedItem.GetType() == typeof(Employee))
                {
                    Employee employee = lbxOutput.SelectedItem as Employee;
                    Details details = new Details(employee);
                    details.Show();
                }
                else if(selectedItem.GetType() == typeof(Customer))
                {
                    Customer customer = new Customer();

                }
            }
            
        }

        //private void BtnCreateEmployee_Click(object sender, RoutedEventArgs e)
        //{
        //    EmployeeForm employeeForm = new EmployeeForm();
        //    employeeForm.Show();
        //}

        //private void BtnCreateCustomer_Click(object sender, RoutedEventArgs e)
        //{
        //    EmployeeForm employeeForm = new EmployeeForm();
        //    employeeForm.Show();
        //    // create customer form goes here.
        //}

        /// <summary>
        /// Display a create form based on the selected tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TabItem tab = tabFormSelection.SelectedItem as TabItem;
            string header = tab.Header as string;

            switch (header)
            {
                case "Employees":
                    EmployeeForm form = new EmployeeForm();
                    form.Show();
                    break;
                case "Customers":
                    // TODO - Show create customer form
                case "Orders":
                    // TODO - Show create order form
                case "Menu Items":
                    // TODO - Show create Menu Item form
                default:
                    break;
            }
        }
    }
}
