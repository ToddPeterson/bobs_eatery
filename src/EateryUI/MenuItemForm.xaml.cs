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

namespace EateryUI
{
    /// <summary>
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class MenuItemForm : Window
    {
        public MenuItemForm()
        {
            InitializeComponent();
        }

        public MenuItemForm(MenuItems menuItem)
        {
            InitializeComponent();
            txtName.Text = menuItem.Name;
            txtDescription.Text = menuItem.Description;
            txtPicturePath.Text = menuItem.PicturePath;
            txtPrepTime.Text = menuItem.PrepTime.ToString();

            if (menuItem.IsSideItem)
            {
                rbIsSideItemYes.IsChecked = true;
            }
            else
            {
                rbIsSideItemNo.IsChecked = true;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
