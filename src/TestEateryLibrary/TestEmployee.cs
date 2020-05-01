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
    }
}
