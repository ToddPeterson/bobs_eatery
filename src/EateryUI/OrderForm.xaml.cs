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
using System.Windows.Shapes;
using EateryLibrary;

namespace EateryUI
{
    /// <summary>
    /// Interaction logic for OrderForm.xaml
    /// </summary>
    public partial class OrderForm : Window
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        int updateID;

        public OrderForm(Order order)
        {
            InitializeComponent();
            cmbCustomerID.SelectedItem = order.CustomerID;
            cmbOrderTypeID.SelectedItem = order.OrderTypeID;
            cmbPaymentMethodID.SelectedItem = order.PaymentMethodID;
            cmbSeatingID.SelectedItem = order.SeatingID;
            dpDateOrdered.SelectedDate = order.DateOrdered;
            updateID = order.OrderID;
            btnCreateOrder.Content = "Edit";
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order newOrder = new Order();
                newOrder.CustomerID = int.Parse(cmbCustomerID.SelectedItem.ToString());
                newOrder.OrderTypeID = int.Parse(cmbOrderTypeID.SelectedItem.ToString());
                newOrder.PaymentMethodID = int.Parse(cmbPaymentMethodID.SelectedItem.ToString());
                newOrder.SeatingID = int.Parse(cmbSeatingID.SelectedItem.ToString());
                newOrder.DateOrdered = dpDateOrdered.DisplayDate;

                if (btnCreateOrder.Content.ToString() == "Create")
                {
                    Order ord = DAL.OrderCreate(newOrder);
                    Details details = new Details(ord);
                    details.Show();
                    this.Close();
                }
                else if (btnCreateOrder.Content.ToString() == "Edit")
                {
                    newOrder.OrderID = updateID;
                    DAL.OrderUpdate(newOrder);
                    Details details = new Details(newOrder);
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
