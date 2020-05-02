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
    class TestCustomers
    {
        [Test]
        public void GetAll()
        {
            List<Customer> customers = DAL.CustomerGetAll();
            Assert.Greater(customers.Count, 0);
        }

        [Test]
        public void GetByID()
        {
            Customer c = DAL.CustomerGetByID(11);

            Assert.AreEqual(c.FirstName, "Aurora");
            Assert.AreEqual(c.MiddleName, "Teagan");
            Assert.AreEqual(c.LastName, "Foreman");
            Assert.AreEqual(c.CustomerNumber, 11);
            Assert.AreEqual(c.PhoneNumber, "(412) 543-3101");
            Assert.AreEqual(c.Email, "eu.dolor.egestas@porttitorerosnec.co.uk");
            Assert.AreEqual(c.StreetAddress, "519-979 Sed, Rd.");
            Assert.AreEqual(c.CityID, 51);
        }

        [Test]
        public void Create()
        {
            Customer c = new Customer();

            c.FirstName = "fn";
            c.MiddleName = "mn";
            c.LastName = "ln";
            c.CustomerNumber = UniqueCustomerNumber();
            c.PhoneNumber = "999999999";
            c.Email = "fake@email.com";
            c.StreetAddress = "321 Hawthorne Rd.";
            c.CityID = 9;

            Customer created = DAL.CustomerAdd(c);

            Assert.AreNotEqual(created.ID, 0);

            Customer check = DAL.CustomerGetByID(created.ID);

            Assert.AreEqual(c.FirstName, check.FirstName);
            Assert.AreEqual(c.MiddleName, check.MiddleName);
            Assert.AreEqual(c.LastName, check.LastName);
            Assert.AreEqual(c.CustomerNumber, check.CustomerNumber);
            Assert.AreEqual(c.PhoneNumber, check.PhoneNumber);
            Assert.AreEqual(c.Email, check.Email);
            Assert.AreEqual(c.StreetAddress, check.StreetAddress);
            Assert.AreEqual(c.CityID, check.CityID);
        }

        [Test]
        public void Update()
        {
            Customer c = new Customer();

            c.FirstName = "fn";
            c.MiddleName = "mn";
            c.LastName = "ln";
            c.CustomerNumber = UniqueCustomerNumber();
            c.PhoneNumber = "999999999";
            c.Email = "fake@email.com";
            c.StreetAddress = "321 Hawthorne Rd.";
            c.CityID = 9;

            c = DAL.CustomerAdd(c);

            c.FirstName = "newfn";
            c.MiddleName = "newmn";
            c.LastName = "newln";
            c.CustomerNumber = UniqueCustomerNumber();
            c.PhoneNumber = "new999999999";
            c.Email = "newfake@email.com";
            c.StreetAddress = "321 New Hawthorne Rd.";
            c.CityID = 11;

            int rows = DAL.CustomerUpdate(c);

            Assert.AreEqual(rows, 1);

            Customer check = DAL.CustomerGetByID(c.ID);

            Assert.AreEqual(c.FirstName, check.FirstName);
            Assert.AreEqual(c.MiddleName, check.MiddleName);
            Assert.AreEqual(c.LastName, check.LastName);
            Assert.AreEqual(c.CustomerNumber, check.CustomerNumber);
            Assert.AreEqual(c.PhoneNumber, check.PhoneNumber);
            Assert.AreEqual(c.Email, check.Email);
            Assert.AreEqual(c.StreetAddress, check.StreetAddress);
            Assert.AreEqual(c.CityID, check.CityID);
        }

        private int UniqueCustomerNumber()
        {
            List<Customer> customers = DAL.CustomerGetAll();
            int nextNum = customers.OrderByDescending(x => x.CustomerNumber).First().CustomerNumber + 1;

            return nextNum;
        }
    }
}
