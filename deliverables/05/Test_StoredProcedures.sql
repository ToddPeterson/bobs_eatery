USE BobsAwesomeEatery
GO

-- sprocEmployeeExists

DECLARE @ret1a int
DECLARE @ret1b int
DECLARE @FirstName nvarchar(50)
DECLARE @LastName nvarchar(50)

SELECT TOP 1 @FirstName = FirstName, @LastName = LastName FROM Employees

EXECUTE @ret1a = sprocEmployeesExists @FirstName, @LastName
EXECUTE @ret1b = sprocEmployeesExists 'xxx', 'xxx'

IF (@ret1a = 1 AND @ret1b = -1)
	PRINT('PASS :: sprocEmployeeExists')
ELSE
	PRINT('FAIL :: sprocEmployeeExists')

-- sprocOrderGetByCustomer

--SELECT c.* FROM Customers c
--	JOIN Orders o ON o.CustomerID = c.CustomerID

EXECUTE sprocOrdersGetByCustomer 'Asher', 'Santiago'

-- sprocEmployeeGetByNameLike

--SELECT * FROM Employees

EXECUTE sprocEmployeesGetByNameLikeString 'ck'

-- sproc_CityCreate

EXECUTE sproc_CityCreate 'Pocatello', '83202', 'asfdsadf'

SELECT * FROM Cities
	WHERE Name = 'Pocatello'
SELECT * FROM States

-- sprocOrderItemGetByDate

--SELECT DateOrdered FROM Orders

EXECUTE sprocCustomersEatInOrdersGetByDate '2020-04-03'

-- sprocMenuItemGetByDateOrdered

EXECUTE sprocEntreesGetBetweenDates '2020-01-01', '2020-04-20'

-- sprocMenuItemGetByCuisine

EXECUTE sprocMenuItemsGetByCuisineID 2

-- sprocCustomersGetByServer

EXECUTE sprocCustomersGetByServer 3

-- sprocMenuItemGetByYearOrdered

EXECUTE sprocMenuItemGetByYearOrdered 2020

-- sprocMenuItemGetLowSalesByDate

EXECUTE sprocMenuItemGetLowSalesByDate '2020-01-01', '2020-04-20', 2
EXECUTE sprocMenuItemGetLowSalesByDate '2020-01-01', '2020-04-20', 3


