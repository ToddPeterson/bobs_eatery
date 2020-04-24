--
-- Create Stored Procedures
--

-- Employees

USE BobsAwesomeEatery
GO

CREATE PROCEDURE sprocEmployeeGetAll 
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM dbo.Employees
END
GO

CREATE PROCEDURE sprocEmployeeGet
	@EmployeeID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM dbo.Employees
		WHERE EmployeeID = @EmployeeID
END
GO

CREATE PROCEDURE sproc_EmployeeCreate
	@FirstName nvarchar(50)
	,@MiddleName nvarchar(50)
	,@LastName nvarchar(50)
	,@PhoneNumber nvarchar(15)
	,@Wage money
	,@ContactFirstName nvarchar(50)
	,@ContactLastName nvarchar(50)
	,@ContactPhoneNumber nvarchar(15)
	,@StreetAddress nvarchar(50)
	,@CityID int
	,@EmployeePositionID int
	,@EmployeeNumber int
	,@EmployeeID int = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Employees 
		(FirstName, MiddleName, LastName, 
		PhoneNumber, Wage, ContactFirstName, 
		ContactLastName, ContactPhoneNumber, 
		StreetAddress, CityID, EmployeePositionID, 
		EmployeeNumber)
		VALUES
		(@FirstName, @MiddleName, @LastName, 
		@PhoneNumber, @Wage, @ContactFirstName, 
		@ContactLastName, @ContactPhoneNumber, 
		@StreetAddress, @CityID, @EmployeePositionID, 
		@EmployeeNumber)
		
	SET @EmployeeID = @@Identity
END
GO

CREATE PROCEDURE sproc_EmployeeUpdate
	@EmployeeID int
	,@FirstName nvarchar(50)
	,@MiddleName nvarchar(50)
	,@LastName nvarchar(50)
	,@PhoneNumber nvarchar(15)
	,@Wage money
	,@ContactFirstName nvarchar(50)
	,@ContactLastName nvarchar(50)
	,@ContactPhoneNumber nvarchar(15)
	,@StreetAddress nvarchar(50)
	,@CityID int
	,@EmployeePositionID int
	,@EmployeeNumber int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE Employees 
		SET
			FirstName = @FirstName
			,MiddleName = @MiddleName
			,LastName = @LastName
			,PhoneNumber = @PhoneNumber
			,Wage = @Wage
			,ContactFirstName = @ContactFirstName
			,ContactLastName = @ContactLastName
			,ContactPhoneNumber = @ContactPhoneNumber
			,StreetAddress = @StreetAddress
			,CityID = @CityID
			,EmployeePositionID = @EmployeePositionID
			,EmployeeNumber = @EmployeeNumber
		WHERE EmployeeID = @EmployeeID

END
GO

-- Purchases

CREATE PROCEDURE sprocOrderGetAll
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM Orders
END
GO

CREATE PROCEDURE sprocOrderGet
	@OrderID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM Orders
		WHERE OrderID = @OrderID
END
GO

CREATE PROCEDURE sproc_OrderCreate
	@CustomerID int
	, @SeatingID int
	, @OrderTypeID int
	, @PaymentMethodID int
	, @DateOrdered datetime
	, @OrderID int = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Orders
	(CustomerID, SeatingID, OrderTypeID, 
	PaymentMethodID, DateOrdered)
	VALUES
	(@CustomerID, @SeatingID, @OrderTypeID, 
	@PaymentMethodID, @DateOrdered)

	SET @OrderID = @@IDENTITY
END
GO

CREATE PROCEDURE sproc_OrderUpdate
	@OrderID int
	, @CustomerID int
	, @SeatingID int
	, @OrderTypeID int
	, @PaymentMethodID int
	, @DateOrdered datetime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE Orders 
		SET
			CustomerID = @CustomerID
			, SeatingID = @SeatingID
			, OrderTypeID = @OrderTypeID
			, PaymentMethodID = @PaymentMethodID
			, DateOrdered = @DateOrdered
		WHERE
			OrderID = @OrderID
END
GO

-- Customers

CREATE PROCEDURE sprocCustomerGetAll
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM Customers
END
GO

CREATE PROCEDURE sprocCustomersGetByID
	@CustomerID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM Customers
		WHERE CustomerID = @CustomerID
END
GO

CREATE PROCEDURE sproc_CustomerCreate
	@FirstName nvarchar(50)
	, @MiddleName nvarchar(50)
	, @LastName nvarchar(50)
	, @CustomerNumber int
	, @PhoneNumber nvarchar(15)
	, @Email nvarchar(100)
	, @StreetAddress nvarchar(50)
	, @CityID int
	, @CustomerID int = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Customers
		(FirstName, MiddleName, LastName, 
		CustomerNumber, PhoneNumber, Email, 
		StreetAddress, CityID)
		VALUES
		(@FirstName, @MiddleName, @LastName, 
		@CustomerNumber, @PhoneNumber, @Email, 
		@StreetAddress, @CityID)

	SET @CustomerID = @@IDENTITY
END
GO

CREATE PROCEDURE sproc_CustomerUpdate
	@CustomerID int
	, @FirstName nvarchar(50)
	, @MiddleName nvarchar(50)
	, @LastName nvarchar(50)
	, @CustomerNumber int
	, @PhoneNumber nvarchar(15)
	, @Email nvarchar(100)
	, @StreetAddress nvarchar(50)
	, @CityID int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE Customers 
		SET
			FirstName = @FirstName
			, MiddleName = @MiddleName
			, LastName = @MiddleName
			, CustomerNumber = @CustomerNumber
			, PhoneNumber = @PhoneNumber
			, Email = @Email
			, StreetAddress = @StreetAddress
			, CityID = @CityID
		WHERE
			CustomerID = @CustomerID
END
GO

-- Menu Items



-- Given a first and last name for an employee, return 1 if the employee name is found in database, return -1 otherwise.E.g. if Fred Smith is in your database, return 1 else -1

CREATE PROCEDURE sprocEmployeeExists
	@FirstName nvarchar(50)
	, @LastName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	IF EXISTS(SELECT * FROM Employees WHERE FirstName = @FirstName AND LastName = @LastName)
		RETURN 1
	ELSE
		RETURN -1
END
GO

-- Given a first and last name for acustomer, return all orders from that customer.

CREATE PROCEDURE sprocOrderGetByCustomer
	@FirstName nvarchar(50)
	, @LastName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CustomerID int = 0
	SELECT @CustomerID = CustomerID FROM Customers
		WHERE FirstName = @FirstName
			AND LastName = @LastName

	IF (@CustomerID < 1)
		RETURN -1

	SELECT * FROM Orders 
		WHERE CustomerID = @CustomerID
END
GO

-- Return all employees whose first or last name contains a given string.E.g. if “ar” was given as a parameter, you would return: Mark Smith, Marc Lewis, Paris Marconi, Howey Marsdin, etc.

CREATE PROCEDURE sprocEmployeeGetByNameLike
	@Pattern nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM Employees
		WHERE FirstName LIKE '%' + @Pattern + '%'
			OR LastName LIKE '%' + @Pattern + '%'

END
GO

-- Given the City, Zip, and State name, add the information to the database. Associate them as needed. Do not add data to the relative tables if the tables already contain the given information. Make sure to add any information that is not in the tables.

CREATE PROCEDURE sproc_CityCreate
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
	IF (@StateID < 1)
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
	IF (@CityID < 1)
		BEGIN
			INSERT INTO Cities ([Name], StateID, ZipCode)
				VALUES (@CityName, @StateID, @ZipCode)
			SET @CityID = @@IDENTITY
		END
	
END
GO

-- List the customer name, table number, entrée ordered, and date and time ordered for all items purchased on a given date.

CREATE PROCEDURE sprocOrderItemGetByDate
	@DateOrdered date
AS
BEGIN
	SET NOCOUNT ON

	--DECLARE @EntreeID int
	--SELECT @EntreeID = CategoryID FROM Categories
	--	WHERE [Name] = 'Entree'

	SELECT c.FirstName + ' ' + c.LastName AS [Customer], 
			t.Number AS [Table Number],
			mi.Name AS [Entree],
			o.DateOrdered
		FROM OrderItems oi
		JOIN Orders o ON oi.OrderID = o.OrderID
		JOIN Customers c ON c.CustomerID = o.CustomerID
		JOIN Seatings s ON s.SeatingID = o.SeatingID
		JOIN [Tables] t ON t.TableID = s.TableID
		JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID
		JOIN MenuItems mi ON mi.MenuItemID = miv.MenuItemID
		WHERE CAST(o.DateOrdered AS date) = @DateOrdered
			--AND mi.CategoryID = @EntreeID

END
GO

-- List all entrées orderedbetween two given dates.

CREATE PROCEDURE sprocMenuItemGetByDateOrdered
	@StartDate datetime
	, @EndDate datetime
AS
BEGIN
	SET NOCOUNT ON

	SELECT mi.* FROM MenuItems mi
		JOIN MenuItemVariations miv ON mi.MenuItemID = miv.MenuItemID
		JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
		JOIN Orders o ON oi.OrderID = o.OrderID
		WHERE o.DateOrdered BETWEEN @StartDate AND @EndDate
END
GO

-- Given acuisineID, list all menu items in that cuisine.

CREATE PROCEDURE sprocMenuItemGetByCuisine
	@CuisineID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM MenuItems
		WHERE CuisineTypeID = @CuisineID
END
GO

-- Given anemployeeID, list allthe customers served by that employee.

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

-- Given a year, all menu items purchased during that calendar year.

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

-- Given a number of orders and a start and end date, list any menu item that has less than that number of orders between (inclusive) the given dates.

CREATE PROCEDURE sprocMenuItemGetLowSalesByDate
	@NumberOfSales int
	, @StartDate datetime
	, @EndDate datetime
AS
BEGIN
	SET NOCOUNT ON

	SELECT mi.* FROM MenuItems mi
		JOIN (SELECT mi.MenuItemID, COUNT(oi.OrderItemID) AS [Number of Sales] FROM CuisineTypes ct
			JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
			JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
			LEFT JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
			JOIN Orders o ON o.OrderID = oi.OrderID
			WHERE o.DateOrdered BETWEEN @StartDate AND @EndDate
			GROUP BY mi.MenuItemID) 
			sc ON sc.MenuItemID = mi.MenuItemID
		WHERE sc.[Number of Sales] < @NumberOfSales

END
GO