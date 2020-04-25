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

--3.a. Return all customers from the database.
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