USE BobsAwesomeEatery
GO

--1. Employees:
--a. Return all employees from the database.

CREATE PROCEDURE sprocEmployeesGetAll
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM dbo.Employees
END
GO

--b. Return an employee from a given EmployeeID

CREATE PROCEDURE sprocEmployeesGetByID
@EmployeeID int
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM Employees
		WHERE EmployeeID = @EmployeeID
END
GO

--c. Add an employee.

CREATE PROCEDURE sproc_EmployeesAdd
@EmployeeID int OUTPUT
,@FirstName nvarchar(50)
,@MiddleName nvarchar(50)
,@LastName nvarchar(50)
,@PhoneNumber nvarchar(15)
,@Wage money
,@ContactFirstName nvarchar(50)
,@ContactMiddleName nvarchar(50)
,@ContactLastName nvarchar(50)
,@ContactPhoneNumber nvarchar(50)
,@StreetAddress nvarchar(50)
,@CityID int
,@EmployeePositionID int
,@EmployeeNumber int
AS
BEGIN
	INSERT INTO Employees(FirstName,MiddleName,LastName,PhoneNumber,Wage,ContactFirstName,ContactMiddleName,ContactLastName,ContactPhoneNumber,StreetAddress,CityID,EmployeePositionID,EmployeeNumber) VALUES 
		(@FirstName,@MiddleName,@LastName,@PhoneNumber,@Wage,@ContactFirstName,@ContactMiddleName,@ContactLastName,@ContactPhoneNumber,@StreetAddress,@CityID,@EmployeePositionID,@EmployeeNumber)
	SET @EmployeeID = @@IDENTITY
END
GO

--d. Modify an existing employee.

CREATE PROCEDURE sproc_EmployeesUpdateByID
@EmployeeID int 
,@FirstName nvarchar(50)
,@MiddleName nvarchar(50)
,@LastName nvarchar(50)
,@PhoneNumber nvarchar(15)
,@Wage money
,@ContactFirstName nvarchar(50)
,@ContactMiddleName nvarchar(50)
,@ContactLastName nvarchar(50)
,@ContactPhoneNumber nvarchar(50)
,@StreetAddress nvarchar(50)
,@CityID int
,@EmployeePositionID int
,@EmployeeNumber int
AS
BEGIN
	UPDATE Employees
		SET
			FirstName = @FirstName
			,MiddleName = @MiddleName
			,LastName = @LastName
			,PhoneNumber = @PhoneNumber
			,Wage = @Wage
			,ContactFirstName = @ContactFirstName
			,ContactMiddleName = @ContactMiddleName
			,ContactLastName = @ContactLastName
			,ContactPhoneNumber = @ContactPhoneNumber
			,StreetAddress = @StreetAddress
			,CityID = @CityID
			,EmployeePositionID = @EmployeePositionID
			,EmployeeNumber = @EmployeeNumber
		WHERE EmployeeID = @EmployeeID
END
GO

--2. Purchases:
--a. Return all purchases from the database.

CREATE PROCEDURE sprocOrdersGetAll
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM Orders
END
GO

--b. Return a purchase from a given PurchaseID

CREATE PROCEDURE sprocOrdersGetByID
@OrderID int
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM Orders
		WHERE OrderID = @OrderID
END
GO

--c. Add a purchase.

CREATE PROCEDURE sproc_OrdersAdd
@OrderID int OUTPUT
,@CustomerID int
,@SeatingID int
,@OrderTypeID int
,@PaymentMethodID int
,@DateOrdered DateTime
AS
BEGIN
	INSERT INTO Orders(CustomerID,SeatingID,OrderTypeID,PaymentMethodID,DateOrdered) VALUES 
		(@CustomerID,@SeatingID,@OrderTypeID,@PaymentMethodID,@DateOrdered)
	SET @OrderID = @@IDENTITY
END
GO


--d. Modify an existing purchase.

CREATE PROCEDURE sproc_OrdersUpdateByID
@OrderID int 
,@CustomerID int
,@SeatingID int
,@OrderTypeID int
,@PaymentMethodID int
,@DateOrdered DateTime
AS
BEGIN
	UPDATE Orders
		SET
			CustomerID = @CustomerID
			,SeatingID = @SeatingID
			,OrderTypeID = @OrderTypeID
			,PaymentMethodID = @PaymentMethodID
			,DateOrdered = @DateOrdered
		WHERE OrderID = @OrderID
END
GO

--3. Customers:
--a. Return all customers from the database.

CREATE PROCEDURE sprocCustomersGetAll
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM Customers
END
GO

--b. Return a customer from a given CustomerID

CREATE PROCEDURE sprocCustomersGetByID
@CustomerID int
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM Customers
		WHERE CustomerID = @CustomerID
END
GO

--c. Add a customer.

CREATE PROCEDURE sproc_CustomersAdd
@CustomerID int OUTPUT
,@FirstName nvarchar(50)
,@MiddleName nvarchar(50)
,@LastName nvarchar(50)
,@CustomerNumber int
,@PhoneNumber nvarchar(15)
,@Email nvarchar(100)
,@StreetAddress nvarchar(50)
,@CityID int
AS
BEGIN
	INSERT INTO Customers(FirstName,MiddleName,LastName,CustomerNumber,PhoneNumber,Email,StreetAddress,CityID) VALUES 
		(@FirstName,@MiddleName,@LastName,@CustomerNumber,@PhoneNumber,@Email,@StreetAddress,@CityID)
	SET @CustomerID = @@IDENTITY
END
GO

--d. Modify an existing customer.

CREATE PROCEDURE sproc_CustomersUpdateByID
@CustomerID int 
,@FirstName nvarchar(50)
,@MiddleName nvarchar(50)
,@LastName nvarchar(50)
,@CustomerNumber int
,@PhoneNumber nvarchar(15)
,@Email nvarchar(100)
,@StreetAddress nvarchar(50)
,@CityID int
AS
BEGIN
	UPDATE Customers
		SET
			FirstName = @FirstName
			,MiddleName = @MiddleName
			,LastName = @LastName
			,CustomerNumber = @CustomerID
			,PhoneNumber = @PhoneNumber
			,Email = @Email
			,StreetAddress = @StreetAddress
			,CityID = @CityID
		WHERE CustomerID = @CustomerID
END
GO


--4. Menu Items:
--a. Return all menu items from the database.

CREATE PROCEDURE sprocMenuItemsGetAll
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM MenuItems
END
GO

--b. Return a menu item from a given MenuItemID

CREATE PROCEDURE sprocMenuItemsGetByID
@MenuItemID int
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM MenuItems
		WHERE MenuItemID = @MenuItemID
END
GO

--c. Add a menu item.

CREATE PROCEDURE sproc_MenuItemsAdd
@MenuItemID int OUTPUT
,@Name nvarchar(50)
,@Description nvarchar(max)
,@PicturePath nvarchar(255)
,@IsSideItem bit
,@PrepTime int
,@PrepMethodID int
,@CategoryID int
,@CuisineTypeID int
AS
BEGIN
	INSERT INTO MenuItems(Name,Description,PicturePath,IsSideItem,PrepTime,PrepMethodID,CategoryID,CuisineTypeID) VALUES 
		(@Name,@Description,@PicturePath,@IsSideItem,@PrepTime,@PrepMethodID,@CategoryID,@CuisineTypeID)
	SET @MenuItemID = @@IDENTITY
END
GO

--d. Modify an existing menu item.

CREATE PROCEDURE sproc_MenuItemsUpdateByID
@MenuItemID int 
,@Name nvarchar(50)
,@Description nvarchar(max)
,@PicturePath nvarchar(255)
,@IsSideItem bit
,@PrepTime int
,@PrepMethodID int
,@CategoryID int
,@CuisineTypeID int
AS
BEGIN
	UPDATE MenuItems
		SET
			Name = @Name
			,Description = @Description
			,PicturePath = @PicturePath
			,IsSideItem = @IsSideItem
			,PrepTime = @PrepTime
			,PrepMethodID = @PrepMethodID
			,CategoryID = @CategoryID
			,CuisineTypeID = @CuisineTypeID
		WHERE MenuItemID = @MenuItemID
END
GO

--5. Given a first and last name for an employee, return 1 if the employee name is found in
--database, return -1 otherwise. E.g. if Fred Smith is in your database, return 1 else -1.

CREATE PROCEDURE sprocEmployeesExists
@FirstName nvarchar(50)
,@LastName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @NameExists int
	SET @NameExists = (
		SELECT EmployeeID FROM Employees
			WHERE FirstName = @FirstName AND LastName = @LastName
	)
	IF @NameExists > 0
		RETURN 1
	ELSE 
		RETURN -1
END
GO

--6. Given a first and last name for a customer, return all orders from that customer.

CREATE PROCEDURE sprocOrdersGetByCustomer
@FirstName nvarchar(50)
,@LastName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT o.OrderID, o.CustomerID, o.SeatingID, o.OrderTypeID, o.PaymentMethodID, o.DateOrdered FROM Orders o
			JOIN Customers c ON c.CustomerID = o.CustomerID
		WHERE c.FirstName = @FirstName
		AND c.LastName = @LastName
END
GO

--7. Return all employees whose first or last name contains a given string. E.g. if “ar” was
--given as a parameter, you would return: Mark Smith, Marc Lewis, Paris Marconi, Howey
--Marsdin, etc.

CREATE PROCEDURE sprocEmployeesGetByNameLikeString
@String nvarchar(50)
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM Employees
		WHERE FirstName LIKE '%' + @String + '%'
		OR LastName LIKE '%' + @String + '%'
END
GO

-- 8. Given the City, Zip, and State name, add the information to the database. Associate them as needed. Do not add data to the relative tables if the tables already contain the given information. Make sure to add any information that is not in the tables.

CREATE PROCEDURE sproc_CitiesCreate
	@CityName nvarchar(50)
	, @ZipCode nvarchar(20)
	, @StateName nvarchar(50)
	, @CityID int = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @StateID int = 0

	-- Create the State if it doesn't exist
	SELECT @StateID = StateID FROM States
		WHERE [Name] = @StateName
	IF @@ROWCOUNT < 1
		BEGIN
			INSERT INTO States ([Name])
				VALUES (@StateName)
			SET @StateID = @@IDENTITY
		END

	-- Create the City if it doesn't exist
	SELECT @CityID = CityID FROM Cities
		WHERE [Name] = @CityName
			AND StateID = @StateID
			AND ZipCode = @ZipCode
	IF @@ROWCOUNT < 1
		BEGIN
			INSERT INTO Cities ([Name], StateID, ZipCode)
				VALUES (@CityName, @StateID, @ZipCode)
			SET @CityID = @@IDENTITY
		END
	
END
GO

--9. List the customer name, table number, entrée ordered, and date and time ordered for
--all items purchased on a given date.

ALTER PROCEDURE sprocCustomersEatInOrdersGetByDate
@Date date
AS
BEGIN
	SET NOCOUNT ON
	SELECT cus.FirstName + ' ' + cus.LastName AS [Customer], t.Number [Table Number], mi.Name [Menu Item], o.DateOrdered [Date Ordered] FROM Customers cus
		JOIN Orders o ON o.CustomerID = cus.CustomerID
		JOIN Seatings s ON s.SeatingID = o.SeatingID
		JOIN [Tables] t ON t.TableID = s.TableID
		JOIN OrderItems oi ON oi.OrderID = o.OrderID
		JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID
		JOIN MenuItems mi ON mi.MenuItemID = miv.MenuItemID
		WHERE CAST(o.DateOrdered AS date) = @Date
END
GO

--10. List all entrées ordered between two given dates.

CREATE PROCEDURE sprocEntreesGetBetweenDates
@BeginDate Date
, @EndDate Date
AS
BEGIN
	SET NOCOUNT ON
	SELECT DISTINCT mi.* FROM Orders o
		JOIN OrderItems oi ON o.OrderID = oi.OrderID
		JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID
		JOIN MenuItems mi ON miv.MenuItemID = mi.MenuItemID
		WHERE CAST(o.DateOrdered AS Date) > @BeginDate AND CAST(o.DateOrdered AS Date) < @EndDate
END
GO

--11. Given a cuisine ID, list all menu items in that cuisine.

CREATE PROCEDURE sprocMenuItemsGetByCuisineID
@CuisineID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT mi.* FROM MenuItems mi
		JOIN CuisineTypes ct ON mi.CuisineTypeID = ct.CuisineTypeID
		WHERE ct.CuisineTypeID = @CuisineID
END
GO

--12. Given an employee ID, list all the customers served by that employee.
CREATE PROCEDURE sprocCustomersGetByEmployeeID
@EmployeeID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT c.* FROM Customers c
		JOIN Orders o ON c.CustomerID = o.CustomerID
		JOIN Seatings s ON o.SeatingID = s.SeatingID
		JOIN Employees e ON s.EmployeeID = e.EmployeeID
		WHERE e.EmployeeID = @EmployeeID
END
GO

--12. Given an employee ID, list all the customers served by that employee.

CREATE PROCEDURE sprocCustomersGetByServer
	@EmployeeID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT c.* FROM Customers c
		JOIN CustomersToTables c2t ON c.CustomerID = c2t.CustomerID
		JOIN Seatings s ON c2t.SeatingID = s.SeatingID
		WHERE s.EmployeeID = @EmployeeID
END
GO

--13. Given a year, all menu items purchased during that calendar year.

CREATE PROCEDURE sprocMenuItemGetByYearOrdered
	@Year int
AS
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT mi.* FROM MenuItems mi
		JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
		JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
		JOIN Orders o ON o.OrderID = oi.OrderID
		WHERE YEAR(o.DateOrdered) = @Year
END
GO

--14. Given a number of orders and a start and end date, list any menu item that has less than
--that number of orders between (inclusive) the given dates.
CREATE PROCEDURE sprocMenuItemGetLowSalesByDate
@BeginDate Date
,@EndDate Date
,@NumberOfOrders int
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM MenuItems mi 
		JOIN (
			SELECT mi.MenuItemID AS [MenuItemID], COUNT(mi.MenuItemID) AS [TimesOrdered] FROM Orders o
				JOIN OrderItems oi ON o.OrderID = oi.OrderID
				JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID
				JOIN MenuItems mi ON miv.MenuItemID = mi.MenuItemID
				WHERE CAST(o.DateOrdered AS Date) > @BeginDate AND CAST(o.DateOrdered AS Date) < @EndDate
				GROUP BY mi.MenuItemID
		) a ON a.MenuItemID = mi.MenuItemID
		WHERE a.TimesOrdered < @NumberOfOrders
END