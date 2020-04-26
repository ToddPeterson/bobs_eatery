﻿using EateryLibrary.Models;
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
        public static List<MenuItem> MenuItemsGetAll()
        {
            SqlConnection conn = null;
            List<MenuItem> output = new List<MenuItem>();

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
        public static MenuItem MenuItemGetByID(int id)
        {
            SqlConnection conn = null;
            MenuItem output = null;

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
        public static MenuItem MenuItemCreate(MenuItem model)
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
        public static int MenuItemUpdate(MenuItem model)
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

                SqlParameter outID = new SqlParameter("@MenuItemID", System.Data.SqlDbType.Int);
                outID.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(outID);

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

        private static MenuItem FillMenuItem(SqlDataReader dr)
        {
            MenuItem mi = new MenuItem();
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
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

                SqlCommand comm = new SqlCommand("sproc_MenuItemsAdd", conn);
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

                comm.Parameters.AddWithValue("@EmployeeID", emp.ID);
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


    }
}
