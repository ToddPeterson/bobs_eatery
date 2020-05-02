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
            Employee ba = lbxOutput.SelectedItem as Employee;
            Details details = new Details(ba);
            details.Show();
        }
       
    }
}
