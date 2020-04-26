using EateryLibrary;
using EateryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = new MenuItem();
            mi.Name = "test food";
            mi.Description = "description";
            mi.PicturePath = "C:\\folder\\image.jpg";
            mi.IsSideItem = true;
            mi.PrepTime = 15;
            mi.PrepMethodID = 1;
            mi.CategoryID = 2;
            mi.CuisineTypeID = 3;

            mi = DAL.MenuItemCreate(mi);

            mi.Name = "new name";
            mi.Description = "new desc";
            mi.PicturePath = "new path";
            mi.IsSideItem = !mi.IsSideItem;
            mi.PrepTime = 123;
            mi.PrepMethodID = 2;
            mi.CategoryID = 3;
            mi.CuisineTypeID = 4;

            int num = DAL.MenuItemUpdate(mi);
        }
    }
}
