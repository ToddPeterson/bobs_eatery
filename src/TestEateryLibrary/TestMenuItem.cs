using EateryLibrary;
using EateryLibrary.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEateryLibrary
{
    [TestFixture]
    class TestMenuItem
    {
        [Test]
        public void GetAll()
        {
            List<MenuItem> items = DAL.MenuItemsGetAll();
            Assert.Greater(items.Count, 0);
        }

        [Test]
        public void GetByID()
        {
            MenuItem mi = DAL.MenuItemGetByID(4);
            Assert.AreEqual(mi.ID, 4);
            Assert.AreEqual(mi.Name, "seafood platter");
            Assert.AreEqual(mi.Description, "a, malesuada id, erat. Etiam vestibulum massa rutrum magna. Cras convallis convallis dolor. Quisque tincidunt pede ac urna. Ut tincidunt vehicula risus. Nulla eget metus eu erat semper rutrum. Fusce dolor quam, elementum at, egestas a, scelerisque sed, sapien. Nunc pulvinar arcu et pede. Nunc sed orci");
            Assert.AreEqual(mi.PicturePath, "eu enim. Etiam imperdiet dictum magna. Ut tincidunt");
            Assert.AreEqual(mi.IsSideItem, true);
            Assert.AreEqual(mi.PrepTime, 387);
            Assert.AreEqual(mi.PrepMethodID, 2);
            Assert.AreEqual(mi.CategoryID, 4);
            Assert.AreEqual(mi.CuisineTypeID, 3);
        }

        [Test]
        public void Create()
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

            MenuItem created = DAL.MenuItemCreate(mi);

            Assert.AreNotEqual(created.ID, 0);

            MenuItem check = DAL.MenuItemGetByID(created.ID);

            Assert.AreEqual(mi.Name, check.Name);
            Assert.AreEqual(mi.Description, check.Description);
            Assert.AreEqual(mi.PicturePath, check.PicturePath);
            Assert.AreEqual(mi.IsSideItem, check.IsSideItem);
            Assert.AreEqual(mi.PrepTime, check.PrepTime);
            Assert.AreEqual(mi.PrepMethodID, check.PrepMethodID);
            Assert.AreEqual(mi.CategoryID, check.CategoryID);
            Assert.AreEqual(mi.CuisineTypeID, check.CuisineTypeID);
        }

        [Test]
        public void Update()
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

            Assert.AreEqual(num, 1);

            MenuItem check = DAL.MenuItemGetByID(mi.ID);
            Assert.AreEqual(mi.Name, check.Name);
            Assert.AreEqual(mi.Description, check.Description);
            Assert.AreEqual(mi.PicturePath, check.PicturePath);
            Assert.AreEqual(mi.IsSideItem, check.IsSideItem);
            Assert.AreEqual(mi.PrepTime, check.PrepTime);
            Assert.AreEqual(mi.PrepMethodID, check.PrepMethodID);
            Assert.AreEqual(mi.CategoryID, check.CategoryID);
            Assert.AreEqual(mi.CuisineTypeID, check.CuisineTypeID);
        }
    }
}
