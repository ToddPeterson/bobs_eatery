using EateryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EateryLibrary
{
    public static class DAL
    {
        private const string db = "BobsAwesomeEatery";

        #region MenuItems
        /// <summary>
        /// Get a list of all MenuItems in the database
        /// </summary>
        public static List<MenuItems> MenuItemsGetAll()
        {
            SqlConnection conn = null;
            List<MenuItems> output = new List<MenuItems>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocMenuItemsGetAll", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while(dr.Read())
                {
                    output.Add(FillMenuItem(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Get the menu item associated with the given ID
        /// </summary>
        /// <param name="id">Primary key associated with a MenuItem</param>
        /// <returns></returns>
        public static MenuItems MenuItemGetByID(int id)
        {
            SqlConnection conn = null;
            MenuItems output = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocMenuItemsGetByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MenuItemID", id);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output = FillMenuItem(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Create a new MenuItem record in the database
        /// </summary>
        /// <param name="model">A MenuItem data model</param>
        /// <returns>The input MenuItem model with the ID attribute set</returns>
        public static MenuItems MenuItemCreate(MenuItems model)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sproc_MenuItemsAdd", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@Name", model.Name);
                comm.Parameters.AddWithValue("@Description", model.Description);
                comm.Parameters.AddWithValue("@PicturePath", model.PicturePath);
                comm.Parameters.AddWithValue("@IsSideItem", model.IsSideItem);
                comm.Parameters.AddWithValue("@PrepTime", model.PrepTime);
                comm.Parameters.AddWithValue("@PrepMethodID", model.PrepMethodID);
                comm.Parameters.AddWithValue("@CategoryID", model.CategoryID);
                comm.Parameters.AddWithValue("@CuisineTypeID", model.CuisineTypeID);

                SqlParameter outID = new SqlParameter("@MenuItemID", System.Data.SqlDbType.Int);
                outID.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(outID);

                conn.Open();
                comm.ExecuteNonQuery();

                if (outID.Value != DBNull.Value)
                    model.ID = (int)outID.Value;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return model;
        }

        /// <summary>
        /// Update a MenuItem record in the database
        /// </summary>
        /// <param name="model">A MenuItem model</param>
        /// <returns>The number of rows affected</returns>
        public static int MenuItemUpdate(MenuItems model)
        {
            SqlConnection conn = null;
            int numberOfRowsAffected = 0;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sproc_MenuItemsUpdateByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@MenuItemID", model.ID);
                comm.Parameters.AddWithValue("@Name", model.Name);
                comm.Parameters.AddWithValue("@Description", model.Description);
                comm.Parameters.AddWithValue("@PicturePath", model.PicturePath);
                comm.Parameters.AddWithValue("@IsSideItem", model.IsSideItem);
                comm.Parameters.AddWithValue("@PrepTime", model.PrepTime);
                comm.Parameters.AddWithValue("@PrepMethodID", model.PrepMethodID);
                comm.Parameters.AddWithValue("@CategoryID", model.CategoryID);
                comm.Parameters.AddWithValue("@CuisineTypeID", model.CuisineTypeID);

                conn.Open();
                numberOfRowsAffected = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return numberOfRowsAffected;
        }

        private static MenuItems FillMenuItem(SqlDataReader dr)
        {
            MenuItems mi = new MenuItems();
            mi.ID = (int)dr["MenuItemID"];
            mi.Name = (string)dr["Name"];
            mi.Description = (string)dr["Description"];
            mi.PicturePath = (string)dr["PicturePath"];
            mi.IsSideItem = (bool)dr["IsSideItem"];
            mi.PrepTime = (int)dr["PrepTime"];
            mi.PrepMethodID = (int)dr["PrepMethodID"];
            mi.CategoryID = (int)dr["CategoryID"];
            mi.CuisineTypeID = (int)dr["CuisineTypeID"];

            return mi;
        }
        #endregion

        #region Employees

        /// <summary>
        /// Get a list of all Employeess in the database
        /// </summary>
        public static List<Employee> EmployeesGetAll()
        {
            SqlConnection conn = null;
            List<Employee> output = new List<Employee>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocEmployeesGetAll", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillEmployee(dr));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Get the Employee associated with the given ID
        /// </summary>
        /// <param name="id">Primary key associated with an Employee</param>
        /// <returns></returns>
        public static Employee EmployeeGetByID(int id)
        {
            SqlConnection conn = null;
            Employee output = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocEmployeesGetByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@EmployeeID", id);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output = FillEmployee(dr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Create a new Employee record in the database
        /// </summary>
        /// <param name="emp">An Employee data model</param>
        /// <returns>The input Employee model with the ID attribute set</returns>
        public static Employee EmployeeCreate(Employee emp)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sproc_EmployeesAdd", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@FirstName", emp.FirstName);
                comm.Parameters.AddWithValue("@MiddleName", emp.MiddleName);
                comm.Parameters.AddWithValue("@LastName", emp.LastName);
                comm.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                comm.Parameters.AddWithValue("@Wage", emp.Wage);
                comm.Parameters.AddWithValue("@ContactFirstName", emp.ContactFirstName);
                comm.Parameters.AddWithValue("@ContactMiddleName", emp.ContactMiddleName);
                comm.Parameters.AddWithValue("@ContactLastName", emp.ContactLastName);
                comm.Parameters.AddWithValue("@ContactPhoneNumber", emp.ContactPhoneNumber);
                comm.Parameters.AddWithValue("@StreetAddress", emp.StreetAddress);
                comm.Parameters.AddWithValue("@CityID", emp.CityID);
                comm.Parameters.AddWithValue("@EmployeePositionID", emp.EmployeePositionID);
                comm.Parameters.AddWithValue("@EmployeeNumber", emp.EmployeeNumber);

                SqlParameter outID = new SqlParameter("@EmployeeID", System.Data.SqlDbType.Int);
                outID.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(outID);

                conn.Open();
                comm.ExecuteNonQuery();

                if (outID.Value != DBNull.Value)
                    emp.ID = (int)outID.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return emp;
        }

        /// <summary>
        /// Update a MenuItem record in the database
        /// </summary>
        /// <param name="emp">An Employee model</param>
        /// <returns>The number of rows affected</returns>
        public static int EmployeeUpdate(Employee emp)
        {
            SqlConnection conn = null;
            int numberOfRowsAffected = 0;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sproc_EmployeesUpdateByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                int id = (int)emp.ID;
                comm.Parameters.AddWithValue("@EmployeeID", id);
                comm.Parameters.AddWithValue("@FirstName", emp.FirstName);
                comm.Parameters.AddWithValue("@MiddleName", emp.MiddleName);
                comm.Parameters.AddWithValue("@LastName", emp.LastName);
                comm.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                comm.Parameters.AddWithValue("@Wage", emp.Wage);
                comm.Parameters.AddWithValue("@ContactFirstName", emp.ContactFirstName);
                comm.Parameters.AddWithValue("@ContactMiddleName", emp.ContactMiddleName);
                comm.Parameters.AddWithValue("@ContactLastName", emp.ContactLastName);
                comm.Parameters.AddWithValue("@ContactPhoneNumber", emp.ContactPhoneNumber);
                comm.Parameters.AddWithValue("@StreetAddress", emp.StreetAddress);
                comm.Parameters.AddWithValue("@CityID", emp.CityID);
                comm.Parameters.AddWithValue("@EmployeePositionID", emp.EmployeePositionID);
                comm.Parameters.AddWithValue("@EmployeeNumber", emp.EmployeeNumber);

                conn.Open();
                numberOfRowsAffected = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return numberOfRowsAffected;
        }

        private static Employee FillEmployee(SqlDataReader dr)
        {
            Employee emp = new Employee();
            emp.ID = (int)dr["EmployeeID"];
            emp.FirstName = (string)dr["FirstName"];
            emp.MiddleName = (string)dr["MiddleName"];
            emp.LastName = (string)dr["LastName"];
            emp.PhoneNumber = (string)dr["PhoneNumber"];
            emp.Wage = (decimal)dr["Wage"];
            emp.ContactFirstName = (string)dr["ContactFirstName"];
            emp.ContactMiddleName = (string)dr["ContactMiddleName"];
            emp.ContactLastName = (string)dr["ContactLastName"];
            emp.ContactPhoneNumber = (string)dr["ContactPhoneNumber"];
            emp.StreetAddress = (string)dr["StreetAddress"];
            emp.CityID = (int)dr["CityID"];
            emp.EmployeePositionID = (int)dr["EmployeePositionID"];
            emp.EmployeeNumber = (int)dr["EmployeeNumber"];

            return emp;
        }

        #endregion

        #region Customers

        /// <summary>
        /// Get a list of all Customers in the database
        /// </summary>
        public static List<Customer> CustomerGetAll()
        {
            SqlConnection conn = null;
            List<Customer> output = new List<Customer>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocCustomersGetAll", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillCustomer(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Get the Customer associated with the given ID
        /// </summary>
        /// <param name="id">Primary key associated with a Customer</param>
        /// <returns>Customer</returns>
        public static Customer CustomerGetByID(int id)
        {
            SqlConnection conn = null;
            Customer output = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocCustomersGetByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CustomerID", id);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output = FillCustomer(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Create a new Customer record in the database
        /// </summary>
        /// <param name="emp">Customer data model</param>
        /// <returns>The id of customer added</returns>
        public static Customer CustomerAdd(Customer customer)
        {
            if (customer == null)
                return customer;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sproc_CustomersAdd", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@FirstName", customer.FirstName);
                comm.Parameters.AddWithValue("@MiddleName", customer.MiddleName);
                comm.Parameters.AddWithValue("@LastName", customer.LastName);
                comm.Parameters.AddWithValue("@CustomerNumber", customer.CustomerNumber);
                comm.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                comm.Parameters.AddWithValue("@Email", customer.Email);
                comm.Parameters.AddWithValue("@StreetAddress", customer.StreetAddress);
                comm.Parameters.AddWithValue("@CityID", customer.CityID);

                SqlParameter retParameter = comm.Parameters.Add("@CustomerID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                comm.ExecuteNonQuery();

                if (retParameter.Value != DBNull.Value)
                    customer.ID = (int)retParameter.Value;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return customer;
        }

        /// <summary>
        /// Update a Customer record in the database
        /// </summary>
        /// <param name="emp">A customer model</param>
        /// <returns>Effected ID</returns>
        public static int CustomerUpdate(Customer customer)
        {
            if (customer == null) return -1;
            int effectedId = -1;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));
                SqlCommand comm = new SqlCommand("sproc_CustomersUpdateByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                comm.Parameters.AddWithValue("@CustomerID", customer.ID);
                comm.Parameters.AddWithValue("@FirstName", customer.FirstName);
                comm.Parameters.AddWithValue("@MiddleName", customer.MiddleName);
                comm.Parameters.AddWithValue("@LastName", customer.LastName);
                comm.Parameters.AddWithValue("@CustomerNumber", customer.CustomerNumber);
                comm.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                comm.Parameters.AddWithValue("@Email", customer.Email);
                comm.Parameters.AddWithValue("@StreetAddress", customer.StreetAddress);
                comm.Parameters.AddWithValue("@CityID", customer.CityID);

                effectedId = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return effectedId;
        }

        private static Customer FillCustomer(SqlDataReader dr)
        {
            Customer customer= new Customer();
            customer.ID = (int)dr["CustomerID"];
            customer.FirstName = (string)dr["FirstName"];
            customer.MiddleName = (string)dr["MiddleName"];
            customer.LastName = (string)dr["LastName"];
            customer.CustomerNumber = (int)dr["CustomerNumber"];
            customer.PhoneNumber = (string)dr["PhoneNumber"];
            customer.Email = (string)dr["Email"];
            customer.StreetAddress = (string)dr["StreetAddress"];
            customer.CityID = (int)dr["CityID"];
            return customer;
        }

        #endregion

        #region Orders
        public static List<Order> OrdersGetAll()
        {
            SqlConnection conn = null;
            List<Order> output = new List<Order>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocOrdersGetAll", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillOrder(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        public static Order OrderGetByID(int id)
        {
            SqlConnection conn = null;
            Order output = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocOrdersGetByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@OrderID", id);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output = FillOrder(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        public static Order OrderCreate(Order model)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sproc_OrdersAdd", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@CustomerID", model.CustomerID);
                comm.Parameters.AddWithValue("@SeatingID", model.SeatingID);
                comm.Parameters.AddWithValue("@OrderTypeID", model.OrderTypeID);
                comm.Parameters.AddWithValue("@DateOrdered", model.DateOrdered);
                comm.Parameters.AddWithValue("@PaymentMethodID", model.PaymentMethodID);

                SqlParameter outID = new SqlParameter("@OrderID", System.Data.SqlDbType.Int);
                outID.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(outID);

                conn.Open();
                comm.ExecuteNonQuery();

                if (outID.Value != DBNull.Value)
                    model.OrderID = (int)outID.Value;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return model;
        }

        public static int OrderUpdate(Order model)
        {
            SqlConnection conn = null;
            int numberOfRowsAffected = 0;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sproc_OrdersUpdateByID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@OrderID", model.OrderID);
                comm.Parameters.AddWithValue("@CustomerID", model.CustomerID);
                comm.Parameters.AddWithValue("@SeatingID", model.SeatingID);
                comm.Parameters.AddWithValue("@OrderTypeID", model.OrderTypeID);
                comm.Parameters.AddWithValue("@DateOrdered", model.DateOrdered);
                comm.Parameters.AddWithValue("@PaymentMethodID", model.PaymentMethodID);

                conn.Open();
                numberOfRowsAffected = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return numberOfRowsAffected;
        }

        private static Order FillOrder(SqlDataReader dr)
        {
            Order o = new Order();
            o.OrderID = (int)dr["OrderID"];
            o.CustomerID = (int)dr["CustomerID"];
            o.SeatingID = (int)dr["SeatingID"];
            o.OrderTypeID = (int)dr["OrderTypeID"];
            o.DateOrdered = (DateTime)dr["DateOrdered"];
            o.PaymentMethodID = (int)dr["PaymentMethodID"];

            return o;
        }

        #endregion

        /// <summary>
        /// Query 5
        /// Returns a bool indicating whether or not an employee with the given name exists in the database
        /// </summary>
        /// <param name="firstName">string: The employee's first name</param>
        /// <param name="lastName">string: The employee's last name</param>
        /// <returns></returns>
        public static bool EmployeeExists(string firstName, string lastName)
        {
            SqlConnection conn = null;
            bool output = false;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocEmployeesExists", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@FirstName", firstName);
                comm.Parameters.AddWithValue("@LastName", lastName);

                // Add a parameter to get the return value from the sproc
                comm.Parameters.Add("@return", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                conn.Open();
                comm.ExecuteNonQuery();

                // return value is 1 or -1
                output = ((int)comm.Parameters["@return"].Value == 1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Query 6
        /// Given a customer's first and last name, returns all orders made by that customer
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static List<Order> OrdersGetByCustomer(string firstName, string lastName)
        {
            SqlConnection conn = null;
            List<Order> output = new List<Order>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocOrdersGetByCustomer", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@FirstName", firstName);
                comm.Parameters.AddWithValue("@LastName", lastName);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillOrder(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// Query 7
        /// Given a string pattern, returns all employees whose first or last name contain that string.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static List<Employee> EmployeesGetByNameLikeString(string pattern)
        {
            SqlConnection conn = null;
            List<Employee> output = new List<Employee>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocEmployeesGetByNameLikeString", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@String", pattern);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillEmployee(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        #region SprocsEightToTen
        public static int CitiesCreate(string cityName, int zip, string stateName)
        {
            SqlConnection conn = null;
            int output = 0;

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));
                SqlCommand comm = new SqlCommand("sproc_CitiesCreate", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CityName", cityName);
                comm.Parameters.AddWithValue("@ZipCode", zip);
                comm.Parameters.AddWithValue("@StateName", stateName);
                comm.Parameters.Add("@CityID", System.Data.SqlDbType.Int);
                comm.Parameters["@CityID"].Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                comm.ExecuteNonQuery();
                output = (int)comm.Parameters["@CityID"].Value;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }


        public static List<Tuple<string, int, string, DateTime>> CustomersEatInOrdersGetByDate(string dateString)
        {
            DateTime date;
            bool parsed = DateTime.TryParse(dateString, out date);

            List<Tuple<string, int, string, DateTime>> results = new List<Tuple<string, int, string, DateTime>>();

            if (parsed)
            {
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(GlobalConfig.ConnectionString(db));
                    SqlCommand comm = new SqlCommand("sprocCustomersEatInOrdersGetByDate", conn);
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@Date", date);


                    conn.Open();
                    SqlDataReader dr = comm.ExecuteReader();

                    while (dr.Read())
                    {
                        Tuple<string, int, string, DateTime> row = new Tuple<string, int, string, DateTime>(
                                (string)dr["Customer"],
                                (int)dr["Table Number"],
                                (string)dr["Menu Item"],
                                (DateTime)dr["Date Ordered"]
                            );
                        results.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                finally
                {
                    if (conn != null) conn.Close();
                }

                return results;
            }

            return null;
        }


        public static List<MenuItems> EntreesGetBetweenDates(string beginDate, string endDate)
        {
            SqlConnection conn = null;
            List<MenuItems> results = new List<MenuItems>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));
                SqlCommand comm = new SqlCommand("sprocEntreesGetBetweenDates", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@BeginDate", beginDate);
                comm.Parameters.AddWithValue("@EndDate", endDate);


                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    MenuItems menuItem = new MenuItems();
                    menuItem.ID = (int)dr["MenuItemID"];
                    menuItem.Name = (string)dr["Name"];
                    menuItem.Description = (string)dr["Description"];
                    menuItem.PicturePath = (string)dr["PicturePath"];
                    menuItem.IsSideItem = (bool)dr["IsSideItem"];
                    menuItem.PrepTime = (int)dr["PrepTime"];
                    menuItem.PrepMethodID = (int)dr["PrepMethodID"];
                    menuItem.CategoryID = (int)dr["CategoryID"];
                    menuItem.CuisineTypeID = (int)dr["CuisineTypeID"];
                    results.Add(menuItem);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return results;
        }


        #endregion


        #region Sprocs11to14

        /// <summary>
        /// Query 11
        /// Lists menu items that belong to a given cuisine
        /// </summary>
        /// <param name="cuisineID">ID that belongs to the cuisine in question</param>
        /// <returns>all mennu items that meet the requirements</returns>
        public static List<MenuItems> MenuItemsGetByCuisineID(int cuisineID)
        {
            SqlConnection conn = null;
            List<MenuItems> output = new List<MenuItems>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocMenuItemsGetByCuisineID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CuisineID", cuisineID);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillMenuItem(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }


            return output;

        }

        /// <summary>
        /// Query 12
        /// Lists customers served by a specific employee
        /// </summary>
        /// <param name="employeeID">ID that belongs to the employee in question</param>
        /// <returns>All customers that meet the requirements</returns>
        public static List<Customer> CustomersGetByEmployeeID(int employeeID)
        {
            SqlConnection conn = null;
            List<Customer> output = new List<Customer>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));
                //which sproc do we want to use?
                SqlCommand comm = new SqlCommand("sprocCustomersGetByEmployeeID", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillCustomer(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }


            return output;
        }

        /// <summary>
        /// Query 13
        /// Lists all menu items ordered in a given year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>All menu items that meet the requirements</returns>
        public static List<MenuItems> MenuItemGetByYearOrdered(int year)
        {
            SqlConnection conn = null;
            List<MenuItems> output = new List<MenuItems>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocMenuItemGetByYearOrdered", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Year", year);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillMenuItem(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }


            return output;
        }

        /// <summary>
        /// Query 14
        /// lists all menu items that have been ordered under specified amount of times 
        /// in between two dates
        /// </summary>
        /// <param name="numOrders">max number of orders</param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns>list of menu items that meet requirements</returns>
        public static List<MenuItems> MenuItemGetLowSalesByDate(int numOrders, DateTime beginDate, DateTime endDate)
        {
            //only a datetime option??
            SqlConnection conn = null;
            List<MenuItems> output = new List<MenuItems>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocMenuItemGetLowSalesByDate", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@BeginDate", beginDate);
                comm.Parameters.AddWithValue("@EndDate", endDate);
                comm.Parameters.AddWithValue("@NumberOfOrders", numOrders);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(FillMenuItem(dr));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }
        #endregion

        #region Extras
        /// <summary>
        /// Get the city name, zip code, and state name associated with a given cityID
        /// </summary>
        /// <param name="id">A valid CityID</param>
        /// <returns>A tuple containing (CityName, ZipCode, StateName)</returns>
        public static Tuple<string, string, string> GetCityData(int id)
        {
            SqlConnection conn = null;
            Tuple<string, string, string> output = new Tuple<string, string, string>("", "", "");

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                string sql = "SELECT c.Name [city], c.ZipCode [zip], s.Name [state] FROM Cities c "
                    + "JOIN States s ON c.StateID = s.StateID "
                    + "WHERE c.CityID = @id";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandType = System.Data.CommandType.Text;
                comm.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output = new Tuple<string, string, string>(
                            (string)dr["city"],
                            (string)dr["zip"],
                            (string)dr["state"]
                        );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        //public static Tuple<string, string, string> GetCitiesALL()
        //{
        //    SqlConnection conn = null;
        //    Tuple<string, string, string> output = new Tuple<string, string, string>("", "", "");

        //    try
        //    {
        //        conn = new SqlConnection(GlobalConfig.ConnectionString(db));

        //        SqlCommand comm = new SqlCommand("sprocCitiesGetALL", conn);
        //        comm.CommandType = System.Data.CommandType.StoredProcedure;
        //        conn.Open();
        //        SqlDataReader dr = comm.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            output = new Tuple<string, string, string>(
        //                    (string)dr["city"],
        //                    (string)dr["zip"],
        //                    (string)dr["state"]
        //                );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (conn != null) conn.Close();
        //    }

        //    return output;
        //}

        /// <summary>
        /// returns name of employee's position by id
        /// </summary>
        /// <param name="id">id of position of employee</param>
        /// <returns></returns>
        public static string GetEmployePositionByID(int id)
        {
            SqlConnection conn = null;
            string output = "";

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                SqlCommand comm = new SqlCommand("sprocEmployeePositionGetALL", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@EmployeePositionID", id);
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    output = (string)dr["Name"];
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        /// <summary>
        /// adds a employee position if it's not alreade added in database and returns the id of employee position with the name
        /// </summary>
        /// <returns>employee position id</returns>
        public static int EmployePositionCreate(string name)
        {
            SqlConnection conn = null;
            int output = 0;
            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));
                SqlCommand comm = new SqlCommand("sproc_EmployeePositionAdd", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", name);
                comm.Parameters.Add("@EmployeePositionID", System.Data.SqlDbType.Int);
                comm.Parameters["@EmployeePositionID"].Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                comm.ExecuteNonQuery();
                output = (int)comm.Parameters["@EmployeePositionID"].Value;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return output;
        }

        

        public static Tuple<List<string>, List<string>> GetOrderItems(Order order)
        {
            SqlConnection conn = null;
            List<string> menuItems = new List<string>();
            List<string> drinks = new List<string>();

            try
            {
                conn = new SqlConnection(GlobalConfig.ConnectionString(db));

                string menuItemSql = "SELECT mi.Name FROM Orders o "
                    + "JOIN OrderItems oi ON o.OrderID = oi.OrderID "
                    + "JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID "
                    + "JOIN MenuItems mi ON mi.MenuItemID = miv.MenuItemID "
                    + "WHERE o.OrderID = @id";
                SqlCommand menuItemComm = new SqlCommand(menuItemSql, conn);
                menuItemComm.CommandType = System.Data.CommandType.Text;
                menuItemComm.Parameters.AddWithValue("@id", order.OrderID);

                string drinkSql = "SELECT d.Name FROM Orders o "
                    + "JOIN OrderItems oi ON o.OrderID = oi.OrderID "
                    + "JOIN Drinks d ON oi.DrinkID = d.DrinkID "
                    + "WHERE o.OrderID = @id";

                SqlCommand drinkComm = new SqlCommand(drinkSql, conn);
                drinkComm.CommandType = System.Data.CommandType.Text;
                drinkComm.Parameters.AddWithValue("@id", order.OrderID);

                conn.Open();
                SqlDataReader dr1 = menuItemComm.ExecuteReader();

                while (dr1.Read())
                {
                    menuItems.Add((string)dr1["Name"]);
                }
                dr1.Close();

                SqlDataReader dr2 = drinkComm.ExecuteReader();

                while (dr2.Read())
                {
                    drinks.Add((string)dr2["Name"]);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            Tuple<List<string>, List<string>> output = new Tuple<List<string>, List<string>>(
                menuItems,
                drinks
                );

            return output;
        }

        #endregion
    }
}
