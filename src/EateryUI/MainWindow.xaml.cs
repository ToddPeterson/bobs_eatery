﻿using EateryLibrary;
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
    }
}
