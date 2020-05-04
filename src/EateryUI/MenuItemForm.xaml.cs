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
using System.Windows.Shapes;

namespace EateryUI
{
    /// <summary>
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class MenuItemForm : Window
    {
        private int ID;

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
            txtCategoryID.Text = menuItem.CategoryID.ToString();
            txtCuisineTypeID.Text = menuItem.CuisineTypeID.ToString();
            txtPrepMethodID.Text = menuItem.PrepMethodID.ToString();

            ID = menuItem.ID;

            if (menuItem.IsSideItem)
            {
                rbIsSideItemYes.IsChecked = true;
            }
            else
            {
                rbIsSideItemNo.IsChecked = true;
            }

            btnSubmit.Content = "Edit";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MenuItems item = new MenuItems();

            if (!int.TryParse(txtPrepTime.Text, out int prepTime))
            {
                MessageBox.Show("Invalid Prep time");
                return;
            }
            if (!int.TryParse(txtCategoryID.Text, out int categoryID))
            {
                MessageBox.Show("Invalid Category ID");
                return;
            }
            if (!int.TryParse(txtCuisineTypeID.Text, out int cuisineID))
            {
                MessageBox.Show("Invalid Cuisine ID");
                return;
            }
            if (!int.TryParse(txtPrepMethodID.Text, out int prepID))
            {
                MessageBox.Show("Invalid Prep Method ID");
                return;
            }

            item.Name = txtName.Text;
            item.Description = txtDescription.Text;
            item.PicturePath = txtPicturePath.Text;
            item.PrepTime = prepTime;
            item.CategoryID = categoryID;
            item.CuisineTypeID = cuisineID;
            item.PrepMethodID = prepID;
            if (rbIsSideItemYes.IsChecked == true)
            {
                item.IsSideItem = true;
            } 
            else
            {
                item.IsSideItem = false;
            }

            if (btnSubmit.Content.ToString() == "Create")
            {
                DAL.MenuItemCreate(item);
            }
            else if (btnSubmit.Content.ToString() == "Edit")
            {
                item.ID = ID;
                DAL.MenuItemUpdate(item);
            }

            Details details = new Details(item);
            details.Show();
            this.Close();
        }
    }
}
