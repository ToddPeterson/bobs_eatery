-- 1.a. Return all employees from the database
CREATE PROCEDURE sprocReturnEmployees
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM dbo.Employees
END
GO

--1.b. Return an employee from a given EmployeeID
CREATE PROCEDURE sprocReturnSpecificEmployees
@EmployeeID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM dbo.Employees
	WHERE EmployeeID = @EmployeeID
END
GO

--1.c. Add an employee.
CREATE PROCEDURE sproc_AddEmployee
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
	INSERT INTO Employees([FirstName],[MiddleName],[LastName],[PhoneNumber],[Wage],[ContactFirstName],[ContactMiddleName],[ContactLastName],[ContactPhoneNumber],[StreetAddress],[CityID],[EmployeePositionID],[EmployeeNumber]) VALUES
	(@FirstName,@MiddleName,@LastName,@PhoneNumber,@Wage,@ContactFirstName,@ContactMiddleName,@ContactLastName,@ContactPhoneNumber,@StreetAddress,@CityID,@EmployeePositionID,@EmployeeNumber)
	SET @EmployeeID = @@IDENTITY
END
GO

--1.d. Modify an existing employee
CREATE PROCEDURE sproc_updateEmployee
@EmployeeID int 
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
	UPDATE  dbo.Employees
	SET [FirstName] = @FirstName,[MiddleName] = @MiddleName,[LastName] = @LastName,[PhoneNumber] = @PhoneNumber,[Wage] = @Wage,[ContactFirstName] = @ContactFirstName,[ContactMiddleName]=@ContactMiddleName,[ContactLastName]=@ContactLastName,[ContactPhoneNumber]=@ContactPhoneNumber,[StreetAddress]=@StreetAddress,[CityID]=@CityID,[EmployeePositionID]=@EmployeePositionID,[EmployeeNumber]=@EmployeeNumber
	WHERE EmployeeID = @EmployeeID
END
GO

--3.a. Return all customers from the database.CREATE PROCEDURE sprocReturnCustomersASBEGIN	SET NOCOUNT ON	SELECT * FROM dbo.CustomersENDGO--3.b. Return a customer from a given CustomerIDCREATE PROCEDURE sprocReturnSpecificCustomer@CustomerID intASBEGIN	SET NOCOUNT ON	SELECT * FROM dbo.Customers	WHERE CustomerID = @CustomerIDENDGO--3.c. Add a customerCREATE PROCEDURE sproc_addCusotmer@CustomerID int OUTPUT,@FirstName nvarchar(50),@MiddleName nvarchar(50),@LastName nvarchar(50),@CustomerNumber int,@PhoneNumber nvarchar(50),@Email nvarchar(50),@StreetAddress nvarchar(50),@CityID intASBEGIN	INSERT INTO Customers([FirstName],[MiddleName],[LastName],[PhoneNumber],[CustomerNumber],[StreetAddress],[CityID],[Email]) VALUES	(@FirstName,@MiddleName,@LastName,@CustomerNumber,@PhoneNumber,@Email,@StreetAddress,@CityID)	SET @CustomerID = @@IDENTITYENDGO--3.d. Modify an existing customerCREATE PROCEDURE sproc_updateCustomer@CustomerID int,@FirstName nvarchar(50),@MiddleName nvarchar(50),@LastName nvarchar(50),@CustomerNumber int,@PhoneNumber nvarchar(50),@Email nvarchar(50),@StreetAddress nvarchar(50),@CityID intASBEGIN	UPDATE dbo.Customers	SET [FirstName]=@FirstName,[MiddleName]=@MiddleName,[LastName]=@LastName,[PhoneNumber]=@PhoneNumber,[CustomerNumber]=@CustomerNumber,[StreetAddress]=@StreetAddress,[CityID]=@CityID,[Email]=@Email	WHERE CustomerID = @CustomerIDENDGO--4.a. Return all menu items from the databaseCREATE PROCEDURE sprocReturnMenuItemsASBEGIN	SET NOCOUNT ON	SELECT * FROM dbo.MenuItemsENDGO--4.b. Return a menu item from a given MenuItemIDCREATE PROCEDURE sprocReturnSpecificMenuItem@MenuItemID intASBEGIN	SET NOCOUNT ON	SELECT * FROM dbo.MenuItems	WHERE MenuItemID = @MenuItemIDENDGO--4.c. Add a menu itemCREATE PROCEDURE sproc_addMenuItem@MenuItemID int OUTPUT,@Name nvarchar(50),@Description nvarchar(max),@PicturePath nvarchar(255),@IsSideItem bit,@PrepTime int,@PrepMethodID int,@CategoryID int,@CuisineTypeID intASBEGIN	INSERT INTO MenuItems ([Name],[Description],PicturePath,IsSideItem,PrepTime,PrepMethodID,CategoryID,CuisineTypeID) VALUES	(@Name,@Description,@PicturePath,@IsSideItem,@PrepTime,@PrepMethodID,@CategoryID,@CuisineTypeID)	SET @MenuItemID = @@IDENTITYENDGO--4.d. Modify an existing menu itemCREATE PROCEDURE sproc_updateMenuItem@MenuItemID int,@Name nvarchar(50),@Description nvarchar(max),@PicturePath nvarchar(255),@IsSideItem bit,@PrepTime int,@PrepMethodID int,@CategoryID int,@CuisineTypeID intASBEGIN	UPDATE dbo.MenuItems	SET [Name]=@Name,[Description]=@Description,PicturePath=@PicturePath,IsSideItem=@IsSideItem,PrepTime=@PrepTime,PrepMethodID=@PrepMethodID,CategoryID=@CategoryID,CuisineTypeID=@CuisineTypeID	WHERE MenuItemID = @MenuItemIDENDGO--6. Given a first and last name for a customer, return all orders from that customerCREATE PROCEDURE sprocReturnCustomerFromName@FirstName nvarchar(50),@LastName nvarchar(50)ASBEGIN	SET NOCOUNT ON	SELECT * FROM dbo.Customers	WHERE FirstName = @FirstName AND LastName = @LastNameENDGO--7. Return all employees whose first or last name contains a given string. E.g. if “ar” was
--given as a parameter, you would return: Mark Smith, Marc Lewis, Paris Marconi, Howey
--Marsdin, etc
CREATE PROCEDURE sproc_ReturnEmployeeFromString
@string nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM dbo.Employees
	WHERE FirstName LIKE '%'+@string+'%' OR LastName LIKE '%'+@string+'%'
END
GO

--11. Given a cuisine ID, list all menu items in that cuisine
CREATE PROCEDURE sprocReturnMenuItemFromCuisine
@CuisineTypeID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM dbo.MenuItems
	WHERE CuisineTypeID = @CuisineTypeID
END
GO
