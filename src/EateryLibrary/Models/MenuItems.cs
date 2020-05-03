using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EateryLibrary.Models
{
    public class MenuItems
    {
		#region Private Variables
		private int _ID;
		private string _Name;
		private string _Description;
		private string _PicturePath;
		private bool _IsSideItem;
		private int _PrepTime;
		private int _PrepMethodID;
		private int _CategoryID;
		private int _CuisineTypeID;
		#endregion

		#region Public Variables
		public int ID
		{
			get { return _ID; }
			set { _ID = value; }
		}

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		public string PicturePath
		{
			get { return _PicturePath; }
			set { _PicturePath = value; }
		}

		public bool IsSideItem
		{
			get { return _IsSideItem; }
			set { _IsSideItem = value; }
		}

		public int PrepTime
		{
			get { return _PrepTime; }
			set { _PrepTime = value; }
		}


		public int PrepMethodID
		{
			get { return _PrepMethodID; }
			set { _PrepMethodID = value; }
		}

		public int CategoryID
		{
			get { return _CategoryID; }
			set { _CategoryID = value; }
		}


		public int CuisineTypeID
		{
			get { return _CuisineTypeID; }
			set { _CuisineTypeID = value; }
		}
        #endregion

        public override string ToString()
        {
            return $"{_Name}";
        }
    }
}
