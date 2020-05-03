using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EateryLibrary.Models
{
    public class Order
    {
        #region Private Fields
        private int _OrderID;
        private int _CustomerID;
        private int _SeatingID;
        private int _OrderTypeID;
        private int _PaymentMethodID;
        private DateTime _DateOrdered;
        #endregion

        #region Properties
        public int OrderID
        {
            get
            {
                return _OrderID;
            }

            set
            {
                _OrderID = value;
            }
        }

        public int CustomerID
        {
            get
            {
                return _CustomerID;
            }

            set
            {
                _CustomerID = value;
            }
        }

        public int SeatingID
        {
            get
            {
                return _SeatingID;
            }

            set
            {
                _SeatingID = value;
            }
        }

        public int OrderTypeID
        {
            get
            {
                return _OrderTypeID;
            }

            set
            {
                _OrderTypeID = value;
            }
        }

        public int PaymentMethodID
        {
            get
            {
                return _PaymentMethodID;
            }

            set
            {
                _PaymentMethodID = value;
            }
        }

        public DateTime DateOrdered
        {
            get
            {
                return _DateOrdered;
            }

            set
            {
                _DateOrdered = value;
            }
        }
        #endregion

        public override string ToString()
        {
            Customer cus = DAL.CustomerGetByID(_CustomerID);
            return $"{DateOrdered} - {cus}";
        }

        public string Details()
        {
            Tuple<List<string>, List<string>> orderItems = DAL.GetOrderItems(this);
            
            string details =
                $"Date: {_DateOrdered}\r\n"
                + "Food Ordered:\r\n";

            foreach(string item in orderItems.Item1)
            {
                details += $"\t{item}\r\n";
            }

            details += "Drinks Ordered:\r\n";
            foreach(string item in orderItems.Item2)
            {
                details += $"\t{item}\r\n";
            }

            return details;
        }
    }
}
