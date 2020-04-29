using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EateryLibrary.Models
{
    public class Customer
    {
        #region Private Variables
        private int _ID;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private int _CustomerNumber;
        private string _PhoneNumber;
        private string _Email;
        private string _StreetAddress;
        private int _CityID;
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

        public int CustomerNumber
        {
            get { return _CustomerNumber; }
            set { _CustomerNumber = value; }
        }

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
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

        #endregion

        public  override string ToString()
        {
            return FirstName + " " + MiddleName + " " + LastName;
        }
    }
}
