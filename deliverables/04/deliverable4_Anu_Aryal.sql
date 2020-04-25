-- 1. Show the following employee information: first name, last name, EmployeeID. They should be listed alphabetically by last name first, if more than one employee has the same last name them sort them alphabetically by first name. 
SELECT FirstName, LastName, EmployeeID FROM dbo.Employees
ORDER BY LastName, FirstName;


-- 2. Show the following customer information: first name, last name, telephone number, address, city, state, zipcode; they should be listed alphabetically by last name first. If more than one customerhas the same last name,thensort them alphabetically be first name.
-- x,y,z are used because we are dealing with multiple tables, and when dealing with multiple tables we need to specify the columns as well as table
-- from which the column belongs to
SELECT x.FirstName, x.LastName, x.PhoneNumber, x.StreetAddress, y.Name AS 'City', z.Name AS 'State', y.ZipCode   
FROM Employees x
JOIN Cities y 
ON x.CityID = y.CityID
JOIN States z 
ON y.StateID = z.StateID
ORDER BY x.LastName, x.FirstName


-- 3. Given a customer name, list all of the times that the customer visited the restaurant. Include how the customer paid and at which table they sat. Do not include delivery or carry out orders. Pick a name from your dataset, but the name should be able to easily be replaced.
SELECT c.FirstName, c.LastName, t.Number AS 'TableNumber', a.DateOrdered, p.Name AS 'PaymentMethod' FROM Orders a
JOIN PaymentMethods p 
ON a.PaymentMethodID = p.PaymentMethodID
JOIN Seatings s 
ON a.SeatingID = s.SeatingID
JOIN [Tables] t 
ON s.TableID = t.TableID
JOIN Customers c 
ON a.CustomerID = c.CustomerID
WHERE c.FirstName = 'Seth' AND c.LastName = 'Barron' AND a.OrderTypeID = 1

-- 4. List the Employee number,employee job title,hours worked, salary per hour, and total weekly salary for each technician who worked more than 40 hoursfor a given week.You will need to pick a start and end date, but these should be able to be changed easily.
SELECT a.EmployeeNumber, b.Name, DATEDIFF(hour,c.DateTimeOut,c.DateTimeIn) AS HoursWorked, a.Wage
FROM Employees a, EmployeePositions b, Shifts c
WHERE DATEDIFF(hour,c.DateTimeOut,c.DateTimeIn)<40 AND b.Name = 'CEO'

-- 5. List the menu item name and description for all products which contain an ingredient that has a name which starts with a "B" and has a third letter of "k".  (Your answer should include Baking Powder, Baking Soda, and Baker’s Chocolate.)
SELECT a.Name AS 'ItemName', a.Description, b.Name AS 'Ingredient' FROM MenuItems a, Ingredients b
WHERE a.MenuItemID = b.MenuItemID AND b.Name LIKE 'B_k%'


-- 6. List all vendors in alphabetical order and each ingredient they supply and the first day that item was ever delivered and the last day that ingredient was ever delivered from that vendor.
SELECT a.VendorID,a.Name, b.Name AS 'IngredientSupplied',a.DateDelivered
FROM Vendors a, Ingredients b
WHERE a.VendorID = b.VendorID
ORDER BY a.Name


-- 7. List the customer name, table number, entrée ordered, and date and time ordered for all items purchased on a given date.(e.g. May 5th2010)
SELECT c.FirstName, c.LastName, t.Number, ot.Type AS EntreeOrdered, CONVERT(DATE, o.DateOrdered) AS 'DateOrdered', CONVERT(TIME(0), o.DateOrdered) AS 'TimeOrdered' 
FROM Customers c
JOIN Orders o
ON o.CustomerID = c.CustomerID
JOIN OrderTypes ot
ON ot.OrderTypeID = o.OrderTypeID
JOIN Seatings s
ON s.SeatingID = o.SeatingID
JOIN [Tables] t
ON t.TableID = s.TableID
WHERE CONVERT(DATE,o.DateOrdered) = '2015-01-18'


-- 8. List food items (customer number, customer name, item ordered, item type, price, whether dine in, carry out, or delivery) for all orders associated with a specified customer. The query should a given customer number or name. Pick a name and number from your dataset, but the name should be able to easily be replaced.



-- 9. Products
--      a. For each cuisine list the menu item, description, and the total number of sales from that cuisine.
--      b. As a continuation from a, list the menu item, description, and the total number of sales for the cuisine that has the highest number of sales.



-- 10. List the menu item, description, and number of suppliers for those ingredients that can be supplied from multiple suppliers.



-- 11. List the order number, total price, and count of ticket items for any order where only water was ordered. Only include tickets where every drink on the ticket ordered was water (free drinks).



-- 12. List the order number, total price, and count of ticket items for any order where any drink other than water was ordered. If water was also ordered, it will still be included as long as anyone else on the ticket ordered something other than water (free drinks).



-- 13. List any menu item that has never been ordered.
SELECT * FROM MenuItems 
WHERE NOT EXISTS
(SELECT * FROM MenuItems m
JOIN MenuItemVariations mv
ON mv.MenuItemID = m.MenuItemID
JOIN OrderItems oi
ON oi.MenuItemVariationID = mv.MenuItemID
JOIN Orders o
ON o.OrderID = oi.OrderID)


-- 14. List any cuisine that has never had an item ordered from it.


