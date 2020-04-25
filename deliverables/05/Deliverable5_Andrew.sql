USE BobsAwesomeEatery
GO

-- 1. (a)
CREATE PROCEDURE sprocEmployeesGetAll
-- No inputs
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Employees
END
GO

-- 1. (b)
CREATE PROCEDURE sprocEmployeeGetByID
@EmployeeID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Employees
		WHERE Employees.EmployeeID = @EmployeeID
END
GO

-- 1. (c)
CREATE PROCEDURE sproc_EmployeeAdd
@EmployeeID int OUTPUT
,@FirstName nvarchar(50)
,@MiddleName nvarchar(50)
,@LastName nvarchar(50)
,@PhoneNumber nvarchar(15)
,@Wage money
,@ContactFirstName nvarchar(50)
,@ContactMiddleName nvarchar(50)
,@ContactLastName nvarchar(50)
,@ContactPhoneNumber nvarchar(15)
,@StreetAddress nvarchar(50)
,@CityID int
,@EmployeePositionID int
,@EmployeeNumber int
AS
BEGIN
	INSERT INTO Employees VALUES (@FirstName, @MiddleName, @LastName, @PhoneNumber, @Wage, @ContactFirstName, @ContactMiddleName, @ContactLastName, @ContactPhoneNumber, @StreetAddress, @CityID, @EmployeePositionID, @EmployeeNumber)
	SET @EmployeeID = @@IDENTITY
END
GO

-- 1. (d)
CREATE PROCEDURE sproc_EmployeeUpdate
-- inputs?
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Employees
END
GO

-- 2. (a)
CREATE PROCEDURE sprocOrdersGetAll
-- no inputs
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Orders
END
GO

-- 2. (b)
CREATE PROCEDURE sprocOrderGetByID
@OrderID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Orders
		WHERE OrderID = @OrderID
END
GO

-- 2. (c)
CREATE PROCEDURE sproc_OrderAdd
@OrderID int OUTPUT
,@CustomerID int
,@SeatingID int
,@OrderTypeID int
,@PaymentMethodID int
,@DateOrdered datetime
AS
BEGIN
	INSERT INTO Orders VALUES (@CustomerID, @SeatingID, @OrderTypeID, @PaymentMethodID, @DateOrdered)
	SET @OrderID = @@IDENTITY
END
GO

-- 2. (d)
CREATE PROCEDURE sproc_OrderUpdate
-- inputs?
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Orders
END
GO


-- 3. (a)
CREATE PROCEDURE sprocCustomersGetAll
-- no inputs
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Customers
END
GO

-- 3. (b)
CREATE PROCEDURE sprocCustomerGetByID
@CustomerID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Customers
		WHERE CustomerID = @CustomerID
END
GO

-- 3. (c)
CREATE PROCEDURE sproc_CustomerAdd
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
	INSERT INTO Customers VALUES (@FirstName, @MiddleName, @LastName, @CustomerNumber, @PhoneNumber, @Email, @StreetAddress, @CityID)
	SET @CustomerID = @@IDENTITY
END
GO

-- 3. (d)
CREATE PROCEDURE sproc_CustomerUpdate
-- inputs?
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Customers
END
GO

-- 4. (a)
CREATE PROCEDURE sprocMenuItemsGetAll
-- no inputs
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM MenuItems
END
GO

-- 4. (b)
CREATE PROCEDURE sprocMenuItemGetByID
@MenuItemID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM MenuItems
		WHERE MenuItemID = @MenuItemID
END
GO

-- 4. (c)
CREATE PROCEDURE sproc_MenuItemAdd
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
	INSERT INTO MenuItems VALUES (@Name, @Description, @PicturePath, @IsSideItem, @PrepTime, @PrepMethodID, @CategoryID, @CuisineTypeID)
	SET @MenuItemID = @@IDENTITY
END
GO

-- 4. (d)
CREATE PROCEDURE sproc_MenuItemUpdate
-- inputs?
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM MenuItems
END
GO


-- 5.
CREATE PROCEDURE sprocEmployeeCheckExists
@FirstName nvarchar(50)
,@LastName nvarchar(50)
--,@Result int OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Employees
		WHERE FirstName = @FirstName
		AND LastName = @LastName

	IF @@ROWCOUNT > 0
		BEGIN
			--SET @Result = 1
			--RETURN
			RETURN 1
		END
	ELSE
		BEGIN
			--SET @Result = -1
			--RETURN
			RETURN -1
		END
END
GO

-- 6.
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

-- 7.
CREATE PROCEDURE sprocEmployeesGetByString
@ContainedString nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM Employees
		WHERE FirstName LIKE CONCAT('%', @ContainedString, '%')
		OR LastName LIKE CONCAT('%', @ContainedString, '%')
END
GO

-- 8.
CREATE PROCEDURE sproc_CityStateAdd
@City nvarchar(50)
,@ZipCode nvarchar(20)
,@State nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @StateID int

	SELECT @StateID = StateID FROM States
		WHERE [Name] = @State

	IF @@ROWCOUNT < 1
		BEGIN
			INSERT INTO States VALUES (@State)
			SET @StateID = @@IDENTITY
		END

	SELECT * FROM Cities
		WHERE [Name] = @City

	IF @@ROWCOUNT < 1
		BEGIN
			INSERT INTO Cities VALUES (@City, @StateID, @ZipCode)
		END
END
GO


-- 9.
CREATE PROCEDURE sprocCustomerEatInOrdersGetByDate
@Date datetime
AS
BEGIN
	SET NOCOUNT ON
	SELECT cus.FirstName, cus.LastName, t.Number [Table Number], mi.Name [Menu Item], o.DateOrdered FROM Customers cus
		JOIN Orders o ON o.CustomerID = cus.CustomerID
		JOIN Seatings s ON s.SeatingID = o.SeatingID
		JOIN [Tables] t ON t.TableID = s.TableID
		JOIN OrderItems oi ON oi.OrderID = o.OrderID
		JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID
		JOIN MenuItems mi ON mi.MenuItemID = miv.MenuItemID
		WHERE CAST(o.DateOrdered AS date) = CAST(@Date AS date)
END
GO


-- 10.
CREATE PROCEDURE sprocEntreesGetBetweenDate
@DateOne datetime
,@DateTwo datetime
AS
BEGIN
	SET NOCOUNT ON
	SELECT DISTINCT mi.MenuItemID, mi.[Name], mi.[Description], mi.PicturePath, mi.IsSideItem, mi.PrepTime, mi.PrepMethodID, mi.CategoryID, mi.CuisineTypeID FROM MenuItems mi
			JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
			JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
			JOIN Orders o ON o.OrderID = oi.OrderID
		WHERE o.DateOrdered BETWEEN @DateOne AND @DateTwo
		AND mi.CategoryID = 2
END
GO


-- 11.
CREATE PROCEDURE sprocMenuItemsGetByCuisine
@CuisineTypeID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM MenuItems
		WHERE CuisineTypeID = @CuisineTypeID
END
GO


-- 12.
CREATE PROCEDURE sprocCustomersGetByServer
@EmployeeID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT c.FirstName, c.LastName FROM Orders o
			JOIN Customers c ON c.CustomerID = o.CustomerID
			JOIN Seatings s ON s.SeatingID = o.SeatingID
		WHERE s.EmployeeID = @EmployeeID
END
GO


-- 13.




-- 14.