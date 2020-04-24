USE BobsAwesomeEatery
GO

-- sprocEmployeeExists

DECLARE @ret1a int
DECLARE @ret1b int
DECLARE @FirstName nvarchar(50)
DECLARE @LastName nvarchar(50)

SELECT TOP 1 @FirstName = FirstName, @LastName = LastName FROM Employees

EXECUTE @ret1a = sprocEmployeeExists @FirstName, @LastName
EXECUTE @ret1b = sprocEmployeeExists 'xxx', 'xxx'

IF (@ret1a = 1 AND @ret1b = -1)
	PRINT('PASS :: sprocEmployeeExists')
ELSE
	PRINT('FAIL :: sprocEmployeeExists')

-- sprocOrderGetByCustomer

--SELECT c.* FROM Customers c
--	JOIN Orders o ON o.CustomerID = c.CustomerID

EXECUTE sprocOrderGetByCustomer 'Asher', 'Santiago'

-- sprocEmployeeGetByNameLike

--SELECT * FROM Employees

EXECUTE sprocEmployeeGetByNameLike 'ck'

-- sproc_CityCreate

EXECUTE sproc_CityCreate 'Ypsilanti', '83202', 'Michigan'

SELECT * FROM Cities
	WHERE Name = 'Ypsilanti'
SELECT * FROM States

-- sprocOrderItemGetByDate

--SELECT DateOrdered FROM Orders

EXECUTE sprocOrderItemGetByDate '2020-04-03'

-- sprocMenuItemGetByDateOrdered

EXECUTE sprocMenuItemGetByDateOrdered '2020-01-01', '2020-04-20'

-- sprocMenuItemGetByCuisine

EXECUTE sprocMenuItemGetByCuisine 2

-- sprocCustomersGetByServer

EXECUTE sprocCustomersGetByServer 3

-- sprocMenuItemGetByYearOrdered

EXECUTE sprocMenuItemGetByYearOrdered 2020

-- sprocMenuItemGetLowSalesByDate

EXECUTE sprocMenuItemGetLowSalesByDate 3, '2020-01-01', '2020-04-20'
EXECUTE sprocMenuItemGetLowSalesByDate 2, '2020-01-01', '2020-04-20'


