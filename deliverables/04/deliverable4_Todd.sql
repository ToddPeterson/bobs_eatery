USE BobsAwesomeEatery
GO

-- 1. Show the following employee information: first name, last name, EmployeeID. They should be listed alphabetically by last name first, if more than one employee has the same last name them sort them alphabetically by first name. 

SELECT FirstName, LastName, EmployeeID FROM Employees
	ORDER BY LastName, FirstName
GO -- Done

-- 2. Show the following customer information: first name, last name, telephone number, address, city, state, zipcode; they should be listed alphabetically by last name first. If more than one customerhas the same last name,thensort them alphabetically be first name.

SELECT cus.FirstName, cus.LastName, cus.PhoneNumber, cus.StreetAddress, cit.Name, sta.Name, cit.ZipCode  FROM Customers cus
	JOIN Cities cit ON cus.CityID = cit.CityID
	JOIN States sta ON cit.StateID = sta.StateID
	ORDER BY cus.LastName, cus.FirstName
GO -- Done

-- 3. Given a customer name, list all of the times that the customer visited the restaurant. Include how the customer paid and at which table they sat. Do not include delivery or carry out orders. Pick a name from your dataset, but the name should be able to easily be replaced.
DECLARE @CustomerFirstName nvarchar(50)
DECLARE @CustomerLastName nvarchar(50)
SET @CustomerFirstName = 'Felix'
SET @CustomerLastName = 'Giles'

SELECT o.DateOrdered, pm.Name [Payment Method], t.Number [Table Number] FROM Customers cus
	JOIN CustomersToTables c2t ON cus.CustomerID = c2t.CustomerID
	JOIN Seatings s ON c2t.SeatingID = s.SeatingID
	JOIN Orders o ON o.SeatingID = s.SeatingID
	JOIN PaymentMethods pm ON o.PaymentMethodID = pm.PaymentMethodID
	JOIN [Tables] t ON t.TableID = s.TableID
	JOIN OrderTypes ot ON ot.OrderTypeID = o.OrderTypeID
	LEFT JOIN Deliveries d ON d.OrderID = o.OrderID
	WHERE ot.Type != 'Online'
		AND d.DeliveryID IS NOT NULL
		AND cus.FirstName = @CustomerFirstName
		AND cus.LastName = @CustomerLastName
GO -- Done?

-- 4. List the Employee number,employee job title,hours worked, salary per hour, and total weekly salary for each technician who worked more than 40 hours for a given week.You will need to pick a start and end date, but these should be able to be changed easily.

DECLARE @StartDate datetime
DECLARE @EndDate datetime
SET @StartDate = '2020-03-13'
SET @EndDate = '2020-03-15'

SELECT e.EmployeeNumber, 
		MAX(ep.Name) [Position],
		MAX(hw.[Hours Worked]) [Hours Worked],
		MAX(e.Wage) AS Wage, 
		MAX(hw.[Hours Worked]) * MAX(e.Wage) [Total Wages] 
		FROM Employees e
		JOIN (SELECT e.EmployeeNumber, SUM(DATEPART(HOUR, CAST(sh.DateTimeOut - sh.DateTimeIn AS time))) AS [Hours Worked] FROM Employees e
			JOIN EmployeePositions ep ON e.EmployeePositionID = ep.EmployeePositionID
			JOIN Shifts sh ON sh.EmployeeID = e.EmployeeID
			WHERE sh.DateTimeIn > @StartDate
				AND sh.DateTimeOut < @EndDate
			GROUP BY e.EmployeeNumber) hw ON hw.EmployeeNumber = e.EmployeeNumber
	JOIN EmployeePositions ep ON e.EmployeePositionID = ep.EmployeePositionID
	JOIN Shifts sh ON sh.EmployeeID = e.EmployeeID
	WHERE [Hours Worked] > 40
		AND sh.DateTimeIn > @StartDate
		AND sh.DateTimeOut < @EndDate
	GROUP BY e.EmployeeNumber
GO -- DONE

-- 5. List the menu item name and description for all products which contain an ingredient that has a name which starts with a "B" and has a third letter of "k".  (Your answer should include Baking Powder, Baking Soda, and Baker’s Chocolate.)

SELECT DISTINCT mi.Name, mi.Description FROM MenuItems mi
	JOIN Ingredients i ON mi.MenuItemID = i.MenuItemID
	WHERE i.Name LIKE 'B_K%'
GO -- DONE

-- 6. List all vendors in alphabetical order and each ingredient they supply and the first day that item was ever delivered and the last day that ingredient was ever delivered from that vendor.


-- Pending Issue #3
SELECT v.Name, i.Name, i.IngredientID FROM Vendors v
	JOIN Ingredients i ON i.VendorID = v.VendorID
	ORDER BY v.Name

-- 7. List the customer name, table number, entrée ordered, and date and time ordered for all items purchased on a given date.(e.g. May 5th2010)

DECLARE @Date datetime = '2018-09-29'

SELECT cus.FirstName, cus.LastName, t.Number [Table Number], mi.Name [Menu Item], o.DateOrdered FROM Customers cus
	JOIN Orders o ON o.CustomerID = cus.CustomerID
	JOIN Seatings s ON s.SeatingID = o.SeatingID
	JOIN [Tables] t ON t.TableID = s.TableID
	JOIN OrderItems oi ON oi.OrderID = o.OrderID
	JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID
	JOIN MenuItems mi ON mi.MenuItemID = miv.MenuItemID
	--WHERE mi.CategoryID = 2 -- entree
	WHERE CAST(o.DateOrdered AS date) = CAST(@Date AS date)
GO -- DONE

-- 8. List food items (customer number, customer name, item ordered, item type, price, whether dine in, carry out, or delivery) for all orders associated with a specified customer. The query should a given customer number or name. Pick a name and number from your dataset, but the name should be able to easily be replaced.

DECLARE @CustomerNumber int = 44
DECLARE @CustomerFirstName nvarchar(100) = 'Lacota'
DECLARE @CustomerLastName nvarchar(100) = 'Dean'

SELECT cus.CustomerNumber, cus.FirstName, cus.LastName, mi.Name [Menu Item], ct.Name [Item Type], miv.Price, ot.Type [Order Type] FROM Customers cus
	JOIN Orders o ON o.CustomerID = cus.CustomerID
	JOIN OrderItems oi ON oi.OrderID = o.OrderID
	JOIN OrderTypes ot ON o.OrderTypeID = ot.OrderTypeID
	JOIN MenuItemVariations miv ON oi.MenuItemVariationID = miv.MenuItemVariationID
	JOIN MenuItems mi ON mi.MenuItemID = miv.MenuItemID
	Join Categories ct on ct.CategoryID = mi.CategoryID
	WHERE cus.CustomerNumber = @CustomerNumber
		OR (cus.FirstName = @CustomerFirstName
			AND cus.LastName = @CustomerLastName)
GO -- DONE


-- 9. Products
--      a. For each cuisine list the menu item, description, and the total number of sales from that cuisine.
--      b. As a continuation from a, list the menu item, description, and the total number of sales for the cuisine that has the highest number of sales.

-- a.
--SELECT MAX(ct.Name) AS [Cuisine], MAX(mi.Name) AS [Menu Item], MAX(mi.Description) AS [Description], COUNT(oi.OrderItemID) AS [Number of Sales] FROM CuisineTypes ct
--	JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
--	JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
--	LEFT JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
--	GROUP BY mi.MenuItemID
--	ORDER BY Cuisine

--SELECT ct.Name AS [Cuisine], mi.Name AS [Menu Item], mi.Description AS [Description], num.[Number of Sales] FROM CuisineTypes ct
--	JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
--	JOIN (SELECT mi.MenuItemID, COUNT(oi.OrderItemID) AS [Number of Sales] FROM CuisineTypes ct
--		JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
--		JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
--		LEFT JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
--		GROUP BY mi.MenuItemID) num ON mi.MenuItemID = num.MenuItemID
--	ORDER BY ct.Name

	-- Todd's new query for a
	SELECT ct.Name, mi.Name, mi.Description, num.[Number of Sales] FROM CuisineTypes ct
	JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
	JOIN MenuItemSalesCount num ON mi.MenuItemID = num.MenuItemID
	ORDER BY ct.Name

-- b.
--SELECT mi.CuisineTypeID, MAX(sc.[Number of Sales]) FROM MenuItems mi
--	JOIN MenuItemSalesCount sc on sc.MenuItemID = mi.MenuItemID
--	GROUP BY mi.CuisineTypeID

-- b.

--- Needs work
--SELECT ct.Name AS [Cuisine], mi.Name AS [Menu Item], mi.Description AS [Description], num.[Number of Sales] FROM CuisineTypes ct
--	JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
--	JOIN (
--		SELECT mi.MenuItemID, COUNT(oi.OrderItemID) AS [Number of Sales] FROM CuisineTypes ct
--			JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
--			JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
--			LEFT JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
--			GROUP BY mi.MenuItemID
--		) 
--	num ON mi.MenuItemID = num.MenuItemID
--	ORDER BY ct.Name
--GO -- DONE ?

-- Todds new b
SELECT ct.Name, mi.Name, mi.Description, sc.[Number of Sales] FROM MenuItems mi
	JOIN CuisineTypes ct ON mi.CuisineTypeID = ct.CuisineTypeID
	JOIN MenuItemSalesCount sc ON sc.MenuItemID = mi.MenuItemID
	JOIN (SELECT mi.CuisineTypeID, MAX(sc.[Number of Sales]) AS [MaxSales] FROM MenuItems mi
		JOIN MenuItemSalesCount sc on sc.MenuItemID = mi.MenuItemID
		GROUP BY mi.CuisineTypeID) max_sales 
		ON max_sales.MaxSales = sc.[Number of Sales] 
			AND max_sales.CuisineTypeID = mi.CuisineTypeID

-- 10. List the menu item, description, and number of suppliers for those ingredients that can be supplied from multiple suppliers.

-- TODO: Allow for many to many on vendor - ingredient

-- Andrew Query
SELECT mi.[Name], mi.MenuItemID, mi.[Description], COUNT(mi.[Name]) AS NumberOfSuppliers FROM Ingredients i
	JOIN MenuItems mi ON mi.MenuItemID = i.MenuItemID
	GROUP BY mi.[Name], mi.MenuItemID, mi.[Description]
-- end

SELECT mi.Name FROM MenuItems mi
	JOIN Ingredients i ON i.MenuItemID = mi.MenuItemID

-- 11. List the order number, total price, and count of ticket items for any order where only water was ordered. Only include tickets where every drink on the ticket ordered was water (free drinks).

-- TODO: Fix OrderItem data so only a drink or food item is ordered

SELECT o.OrderID, SUM(d.Price) + SUM(miv.Price) AS [Total Price], COUNT(oi.OrderItemID) AS [Ticket Item Count] FROM 
	(SELECT OrderID FROM Orders
		EXCEPT
		SELECT o.OrderID FROM Orders o
			JOIN OrderItems oi ON oi.OrderID = o.OrderID
			JOIN Drinks d ON d.DrinkID = oi.DrinkID
			WHERE d.Name != 'water'
			GROUP BY o.OrderID) o
	JOIN OrderItems oi ON oi.OrderID = o.OrderID
	LEFT JOIN Drinks d ON d.DrinkID = oi.DrinkID
	LEFT JOIN MenuItemVariations miv ON miv.MenuItemVariationID = oi.MenuItemVariationID
	GROUP BY o.OrderID
	ORDER BY o.OrderID
GO -- DONE

-- 12. List the order number, total price, and count of ticket items for any order where any drink other than water was ordered. If water was also ordered, it will still be included as long as anyone else on the ticket ordered something other than water (free drinks).

SELECT o.OrderID, SUM(d.Price) + SUM(miv.Price) AS [Total Price], COUNT(oi.OrderItemID) AS [Ticket Item Count] FROM 
	(SELECT DISTINCT o.OrderID FROM Orders o
		JOIN OrderItems oi ON oi.OrderID = o.OrderID
		JOIN Drinks d ON d.DrinkID = oi.DrinkID
		WHERE d.Name != 'water') o
	JOIN OrderItems oi ON oi.OrderID = o.OrderID
	LEFT JOIN Drinks d ON d.DrinkID = oi.DrinkID
	LEFT JOIN MenuItemVariations miv ON miv.MenuItemVariationID = oi.MenuItemVariationID
	GROUP BY o.OrderID
GO -- DONE?

-- 13. List any menu item that has never been ordered.

SELECT MenuItemID, Name FROM MenuItems
	EXCEPT
	SELECT mi.MenuItemID, mi.Name FROM MenuItems mi
		JOIN MenuItemVariations miv ON mi.MenuItemID = miv.MenuItemID
		JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
GO -- DONE


-- 14. List any cuisine that has never had an item ordered from it.
Use BobsAwesomeEatery
SELECT Name FROM CuisineTypes
	EXCEPT
	SELECT cui.Name FROM CuisineTypes cui
		JOIN MenuItems mi ON mi.CuisineTypeID = cui.CuisineTypeID
		JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
		LEFT JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
		WHERE oi.OrderItemID IS NOT NULL
			AND oi.MenuItemVariationID IS NOT NULL
GO -- DONE

Select * from MenuItems