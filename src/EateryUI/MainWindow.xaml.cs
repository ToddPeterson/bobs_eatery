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
            foreach (Customer customer in customers)
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
        
        private void btnShowOrders_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();
            List<Order> orders = DAL.OrdersGetAll();
            foreach (Order order in orders)
            {
                lbxOutput.Items.Add(order);
            }
        }

        private void btnShowMenuItems_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();
            List<MenuItems> menuItems = DAL.MenuItemsGetAll();
            foreach (MenuItems menuItemsVar in menuItems)
            {
                lbxOutput.Items.Add(menuItemsVar);
            }
        }

        private void LbxOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxOutput.SelectedIndex >= 0)
            {
                // Took help from the Db Learn Project assignment
                object selectedItem = lbxOutput.SelectedItem;
                if (selectedItem.GetType() == typeof(Employee))
                {
                    Employee employee = lbxOutput.SelectedItem as Employee;
                    Details details = new Details(employee);
                    details.Show();
                }
                else if (selectedItem.GetType() == typeof(Customer))
                {
                    Customer customer = new Customer();

                }
            }

        }

        /// <summary>
        /// Display a create form based on the selected tab
        /// </summary>
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbxOutput.SelectedIndex < 0)
                return;

            TabItem tab = tabFormSelection.SelectedItem as TabItem;
            string header = tab.Header as string;

            switch (header)
            {
                case "Employees":
                    Employee employee = lbxOutput.SelectedItem as Employee;
                    EmployeeForm form = new EmployeeForm(employee);
                    form.Show();
                    break;
                case "Customers":
                // TODO - Show edit customer form
                case "Orders":
                // TODO - Show edit order form
                case "Menu Items":
                // TODO - Show edit Menu Item form
                default:
                    break;
            }
        }

        /// <summary>
        /// Clear the listbox when the selected tab changes
        /// </summary>
        private void tabFormSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbxOutput.Items.Clear();
        }

        private void btnSearchBySubstring_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();

            string substring = tbxEmployeeSubstring.Text;

            if (substring == "")
                return;

            List<Employee> employees = DAL.EmployeesGetByNameLikeString(substring);
            foreach (Employee employee in employees)
            {
                lbxOutput.Items.Add(employee);
            }
        }


        private void btnOrderSearchByCustomer_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();

            string firstName = tbxOrderCustomerFirstName.Text;
            string lastName = tbxOrderCustomerLastName.Text;

            List<Order> orders = DAL.OrdersGetByCustomer(firstName, lastName);

            foreach (Order order in orders)
            {
                lbxOutput.Items.Add(order);
            }
        }

        private void btnEmployeeExists_Click(object sender, RoutedEventArgs e)
        {
            string firstName = tbxEmployeeExistsFirstName.Text;
            string lastName = tbxEmployeeExistsLastName.Text;

            bool exists = DAL.EmployeeExists(firstName, lastName);

            string message = exists ? "Yes" : "No";

            MessageBox.Show(message);
        }


        private void btnSproc8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string city = txtCitySproc8.Text;
                int zip = int.Parse(txtZipSproc8.Text);
                string state = txtStateSproc8.Text;

                DAL.CitiesCreate(city, zip, state);
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }

        }

        private void btnSearchSproc9_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string date = dpSproc9.Text;

                List<Tuple<string, int, string, DateTime>> results = DAL.CustomersEatInOrdersGetByDate(date);

                foreach (Tuple<string, int, string, DateTime> tuple in results)
                {
                    lbxOutput.Items.Add(tuple);
                }
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void btnEntreeSearchSproc10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string startDate = dpSproc10StartDate.Text;
                string endDate = dpSproc10EndDate.Text;

                List<MenuItems> results = DAL.EntreesGetBetweenDates(startDate, endDate);

                foreach (MenuItems tuple in results)
                {
                    lbxOutput.Items.Add(tuple);
                }

            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }

        }

        private void btnYearSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int year = int.Parse(txtYearSearch.Text);

                List<MenuItems> results = DAL.MenuItemGetByYearOrdered(year);

                foreach (MenuItems tuple in results)
                {
                    lbxOutput.Items.Add(tuple);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSproc14_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantity = int.Parse(txtSproc14.Text);
                DateTime start = dpSproc14Start.SelectedDate.GetValueOrDefault();
                DateTime end = dpSproc14End.SelectedDate.GetValueOrDefault();

                List<MenuItems> results = DAL.MenuItemGetLowSalesByDate(quantity,start,end);

                foreach (MenuItems tuple in results)
                {
                    lbxOutput.Items.Add(tuple);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
