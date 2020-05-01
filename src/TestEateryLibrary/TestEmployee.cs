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
    class TestEmployee
    {
        [Test]
        public void GetAll()
        {
            List<Employee> output = DAL.EmployeesGetAll();
            Assert.Greater(output.Count, 0);
        }

        [Test]
        public void GetByID()
        {
            Employee em = DAL.EmployeeGetByID(7);

            Assert.AreEqual(em.FirstName, "Vera");
            Assert.AreEqual(em.MiddleName, "Claire");
            Assert.AreEqual(em.LastName, "Donovan");
            Assert.AreEqual(em.PhoneNumber, "(557) 702-8237");
            Assert.AreEqual(em.Wage, 2730.96M);
            Assert.AreEqual(em.ContactFirstName, "Ocean");
            Assert.AreEqual(em.ContactMiddleName, "Drew");
            Assert.AreEqual(em.ContactLastName, "Powers");
            Assert.AreEqual(em.ContactPhoneNumber, "(150) 695-0453");
            Assert.AreEqual(em.StreetAddress, "5173 Integer Av.");
            Assert.AreEqual(em.CityID, 45);
            Assert.AreEqual(em.EmployeePositionID, 1);
            Assert.AreEqual(em.EmployeeNumber, 7);
        }

        [Test]
        public void Create()
        {
            Employee em = new Employee();
            em.FirstName = "FN";
            em.MiddleName = "MN";
            em.LastName = "LN";
            em.PhoneNumber = "23456";
            em.Wage = 1.468M;
            em.ContactFirstName = "cfn";
            em.ContactMiddleName = "cmn";
            em.ContactLastName = "cln";
            em.ContactPhoneNumber = "55678";
            em.StreetAddress = "123 main street";
            em.CityID = 4;
            em.EmployeePositionID = 2;
            em.EmployeeNumber = UniqueEmployeeNumber();

            Employee created = DAL.EmployeeCreate(em);

            Assert.AreNotEqual(created.ID, 0);

            Employee check = DAL.EmployeeGetByID(created.ID);

            Assert.AreEqual(em.FirstName, em.FirstName);
            Assert.AreEqual(em.MiddleName, em.MiddleName);
            Assert.AreEqual(em.LastName, em.LastName);
            Assert.AreEqual(em.PhoneNumber, check.PhoneNumber);
            Assert.AreEqual(em.Wage, check.Wage);
            Assert.AreEqual(em.ContactFirstName, check.ContactFirstName);
            Assert.AreEqual(em.ContactMiddleName, check.ContactMiddleName);
            Assert.AreEqual(em.ContactLastName, check.ContactLastName);
            Assert.AreEqual(em.ContactPhoneNumber, check.ContactPhoneNumber);
            Assert.AreEqual(em.StreetAddress, check.StreetAddress);
            Assert.AreEqual(em.CityID, check.CityID);
            Assert.AreEqual(em.EmployeePositionID, check.EmployeePositionID);
            Assert.AreEqual(em.EmployeeNumber, check.EmployeeNumber);
        }

        [Test]
        public void Update()
        {
            Employee em = new Employee();
            em.FirstName = "FN";
            em.MiddleName = "MN";
            em.LastName = "LN";
            em.PhoneNumber = "23456";
            em.Wage = 1.468M;
            em.ContactFirstName = "cfn";
            em.ContactMiddleName = "cmn";
            em.ContactLastName = "cln";
            em.ContactPhoneNumber = "55678";
            em.StreetAddress = "123 main street";
            em.CityID = 4;
            em.EmployeePositionID = 2;
            em.EmployeeNumber = UniqueEmployeeNumber();

            em = DAL.EmployeeCreate(em);

            em.FirstName = "newFN";
            em.MiddleName = "newMN";
            em.LastName = "newLN";
            em.PhoneNumber = "new23456";
            em.Wage = 2.468M;
            em.ContactFirstName = "newcfn";
            em.ContactMiddleName = "newcmn";
            em.ContactLastName = "newcln";
            em.ContactPhoneNumber = "new55678";
            em.StreetAddress = "123 new street";
            em.CityID = 3;
            em.EmployeePositionID = 3;
            em.EmployeeNumber = UniqueEmployeeNumber();

            int numRows = DAL.EmployeeUpdate(em);

            Assert.AreEqual(numRows, 1);

            Employee check = DAL.EmployeeGetByID(em.ID);

            Assert.AreEqual(em.FirstName, em.FirstName);
            Assert.AreEqual(em.MiddleName, em.MiddleName);
            Assert.AreEqual(em.LastName, em.LastName);
            Assert.AreEqual(em.PhoneNumber, check.PhoneNumber);
            Assert.AreEqual(em.Wage, check.Wage);
            Assert.AreEqual(em.ContactFirstName, check.ContactFirstName);
            Assert.AreEqual(em.ContactMiddleName, check.ContactMiddleName);
            Assert.AreEqual(em.ContactLastName, check.ContactLastName);
            Assert.AreEqual(em.ContactPhoneNumber, check.ContactPhoneNumber);
            Assert.AreEqual(em.StreetAddress, check.StreetAddress);
            Assert.AreEqual(em.CityID, check.CityID);
            Assert.AreEqual(em.EmployeePositionID, check.EmployeePositionID);
            Assert.AreEqual(em.EmployeeNumber, check.EmployeeNumber);
        }

        private int UniqueEmployeeNumber()
        {
            List<Employee> employees = DAL.EmployeesGetAll();
            int nextNum = employees.OrderByDescending(x => x.EmployeeNumber).First().EmployeeNumber + 1;

            return nextNum;
        }
    }
}
