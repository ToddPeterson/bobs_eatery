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
                    Customer customer = lbxOutput.SelectedItem as Customer;
                    Details details = new Details(customer);
                    details.Show();
                }
                else if (selectedItem.GetType() == typeof(Order))
                {
                    Order order = lbxOutput.SelectedItem as Order;
                    Details details = new Details(order);
                    details.Show();
                }
                else if (selectedItem.GetType() == typeof(MenuItems))
                {
                    // TODO - wire up detail view for menuitem
                    MenuItems menuItem = lbxOutput.SelectedItem as MenuItems;
                    Details details = new Details(menuItem);
                    details.Show();
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
                    EmployeeForm employeeForm = new EmployeeForm();
                    employeeForm.Show();
                    break;
                case "Customers":
                    // TODO - Show create customer form
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.Show();
                    break;
                case "Orders":
                    // TODO - Show create order form
                    OrderForm orderForm = new OrderForm();
                    orderForm.Show();
                    break;
                case "Menu Items":
                    // TODO - Show create Menu Item form
                    MenuItemForm menuItemForm = new MenuItemForm();
                    menuItemForm.Show();
                    break;
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
                    Customer customer = lbxOutput.SelectedItem as Customer;
                    CustomerForm customerForm = new CustomerForm(customer);
                    customerForm.Show();
                    break;
                case "Orders":
                    // TODO - Show edit order form
                    Order order = lbxOutput.SelectedItem as Order;
                    OrderForm orderForm = new OrderForm(order);
                    orderForm.Show();
                    break;
                case "Menu Items":
                    // TODO - Show edit Menu Item form
                    MenuItems menuItem = lbxOutput.SelectedItem as MenuItems;
                    MenuItemForm menuItemForm = new MenuItemForm(menuItem);
                    menuItemForm.Show();
                    break;
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

        private void btnCustomersGetByEmployeeID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int empID;
                int.TryParse(txtCustomersGetByEmployeeID.Text, out empID);
                List<Customer> output = DAL.CustomersGetByEmployeeID(empID);
                lbxOutput.Items.Clear();
                foreach (Customer c in output)
                {
                    lbxOutput.Items.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEmployeeSearchByID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int empID;
                int.TryParse(txtEmployeeID.Text, out empID);
                Employee output = DAL.EmployeeGetByID(empID);
                lbxOutput.Items.Clear();
                lbxOutput.Items.Add(output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCustomerGetByID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int custID;
                int.TryParse(txtCustomerID.Text, out custID);
                Customer output = DAL.CustomerGetByID(custID);
                lbxOutput.Items.Clear();
                lbxOutput.Items.Add(output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMenuItemsGetByID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int menuItemID;
                int.TryParse(txtMenuItemID.Text, out menuItemID);
                MenuItems output = DAL.MenuItemGetByID(menuItemID);
                lbxOutput.Items.Clear();
                lbxOutput.Items.Add(output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOrdersGetByID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ordID;
                int.TryParse(txtOrderID.Text, out ordID);
                Order output = DAL.OrderGetByID(ordID);
                lbxOutput.Items.Clear();
                lbxOutput.Items.Add(output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAllCustomers_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();
            List<Customer> customers = DAL.CustomerGetAll();
            foreach (Customer customer in customers)
            {
                lbxOutput.Items.Add(customer);
            }
        }

        private void BtnCuisineSearch_Click(object sender, RoutedEventArgs e)
        {
            lbxOutput.Items.Clear();
            try
            {
                List<MenuItems> menuItems = DAL.MenuItemsGetByCuisineID(int.Parse(tbxCusisineSearchID.Text));
                foreach(MenuItems menuItem in menuItems)
                {
                    lbxOutput.Items.Add(menuItem);
                }
            }
            catch
            {
                MessageBox.Show("Please enter valid integer.");
            }
        }
    }
}
