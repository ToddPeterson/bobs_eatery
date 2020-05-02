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
    class TestOrder
    {
        [Test]
        public void GetAll()
        {
            List<Order> orders = DAL.OrdersGetAll();
            Assert.Greater(orders.Count, 0);
        }

        [Test]
        public void GetByID()
        {
            Order o = DAL.OrderGetByID(5);

            Assert.AreEqual(o.CustomerID, 62);
            Assert.AreEqual(o.SeatingID, 3);
            Assert.AreEqual(o.OrderTypeID, 1);
            Assert.AreEqual(o.PaymentMethodID, 3);
            Assert.AreEqual(o.DateOrdered, new DateTime(2015, 2, 20, 5, 53, 8));
        }

        [Test]
        public void Create()
        {
            Order o = new Order();

            o.CustomerID = 66;
            o.SeatingID = 5;
            o.OrderTypeID = 2;
            o.PaymentMethodID = 2;
            o.DateOrdered = new DateTime(2020, 4, 20, 11, 11, 4);

            Order created = DAL.OrderCreate(o);

            Assert.AreNotEqual(created.OrderID, 0);

            Order check = DAL.OrderGetByID(created.OrderID);

            Assert.AreEqual(o.CustomerID, check.CustomerID);
            Assert.AreEqual(o.SeatingID, check.SeatingID);
            Assert.AreEqual(o.OrderTypeID, check.OrderTypeID);
            Assert.AreEqual(o.PaymentMethodID, check.PaymentMethodID);
            Assert.AreEqual(o.DateOrdered, check.DateOrdered);
        }

        [Test]
        public void Update()
        {
            Order o = new Order();

            o.CustomerID = 66;
            o.SeatingID = 5;
            o.OrderTypeID = 2;
            o.PaymentMethodID = 2;
            o.DateOrdered = new DateTime(2020, 4, 20, 11, 11, 4);

            o = DAL.OrderCreate(o);

            o.CustomerID = 65;
            o.SeatingID = 7;
            o.OrderTypeID = 3;
            o.PaymentMethodID = 3;
            o.DateOrdered = new DateTime(2021, 4, 20, 11, 11, 4);

            int num = DAL.OrderUpdate(o);

            Assert.AreEqual(num, 1);

            Order check = DAL.OrderGetByID(o.OrderID);

            Assert.AreEqual(o.CustomerID, check.CustomerID);
            Assert.AreEqual(o.SeatingID, check.SeatingID);
            Assert.AreEqual(o.OrderTypeID, check.OrderTypeID);
            Assert.AreEqual(o.PaymentMethodID, check.PaymentMethodID);
            Assert.AreEqual(o.DateOrdered, check.DateOrdered);
        }

        [Test]
        public void GetByCustomer()
        {
            List<Order> orders = DAL.OrdersGetByCustomer("Asher", "Santiago");
            Assert.Greater(orders.Count, 0);
        }
    }
}
