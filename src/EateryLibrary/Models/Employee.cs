using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EateryLibrary.Models
{
    public class Employee
    {
        #region Private Variables
        private int _ID;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _PhoneNumber;
        private decimal _Wage;
        private string _ContactFirstName;
        private string _ContactMiddleName;
        private string _ContactLastName;
        private string _ContactPhoneNumber;
        private string _StreetAddress;
        private int _CityID;
        private int _EmployeePositionID;
        private int _EmployeeNumber;
        #endregion

        #region Public Variables

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        
        public decimal Wage
        {
            get { return _Wage; }
            set { _Wage = value; }
        }

        public string ContactFirstName
        {
            get { return _ContactFirstName; }
            set { _FirstName = value; }
        }

        public string ContactMiddleName
        {
            get { return _ContactMiddleName; }
            set { _ContactMiddleName = value; }
        }

        public string ContactLastName
        {
            get { return _ContactLastName; }
            set { _ContactLastName = value; }
        }

        public string ContactPhoneNumber
        {
            get { return _ContactPhoneNumber; }
            set { _ContactPhoneNumber = value; }
        }

        public string StreetAddress
        {
            get { return _StreetAddress; }
            set { _StreetAddress = value; }
        }

        public int CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }

        public int EmployeePositionID
        {
            get { return _EmployeePositionID; }
            set { _EmployeePositionID = value; }
        }

        public int EmployeeNumber
        {
            get { return _EmployeeNumber; }
            set { _EmployeeNumber = value; }
        }

        #endregion
    }
}
