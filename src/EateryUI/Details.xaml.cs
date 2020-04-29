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
        Employee emp = new Employee();
        public Details()
        {
            InitializeComponent();
        }

        public Details(Employee employee)
        {
            InitializeComponent();
            emp = employee;
            tbDetail.Text = "Details on: " + employee.ToString();
            lbxDetails.Items.Add(employee.Details());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm(emp);
            employeeForm.Show();
            this.Close();
        }
    }
}
