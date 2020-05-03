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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        object model = null;

        public Details()
        {
            InitializeComponent();
        }

        public Details(Employee employee)
        {
            InitializeComponent();
            model = employee;
            tbDetail.Text = "Details on: " + employee.ToString();
            lbxDetails.Items.Add(employee.Details());
        }

        public Details(Customer customer)
        {
            InitializeComponent();
            model = customer;
            tbDetail.Text = $"Details for: {customer}";
            lbxDetails.Items.Add(customer.Details());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (model.GetType() == typeof(Employee))
            {
                EmployeeForm employeeForm = new EmployeeForm(model as Employee);
                employeeForm.Show();
            }
            else if (model.GetType() == typeof(Customer))
            {
                // TODO - show customer form
                //CustomerForm form = new CustomerForm(model as Customer);
                //form.Show();
            }
            else if (model.GetType() == typeof(MenuItems))
            {
                // TODO - add contstructor to MenuItemForm
                //MenuItemForm form = new MenuItemForm(model as MenuItems);
                //form.Show();
            }
            else if (model.GetType() == typeof(Order))
            {
                // TODO - add constructor to OrderForm
                //OrderForm form = new OrderForm(model as Order);
                //form.Show();
            }
            this.Close();
        }
    }
}
