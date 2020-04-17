--
-- Add data related to deliverable 4
--
USE BobsAwesomeEatery
GO

-- For query 5

INSERT INTO MenuItems VALUES
	('Cookies', 'You cook them. You eat them. Such is life', 'C:\picture\cookie.jpg', 0, 40, 1, 3, 3)

DECLARE @CookieID int = @@IDENTITY

INSERT INTO Ingredients VALUES
	('Baking Soda', 'Pantry', @CookieID, 2),
	('Baking Powder', 'Pantry', @CookieID, 2),
	('Baker''s Chocolate', 'Pantry', @CookieID, 4)

-- For query 11
-- Make water free

UPDATE Drinks
	SET Price = 0
	WHERE Name = 'water'

-- Add an order where only water is ordered
INSERT INTO Orders (CustomerID, SeatingID, OrderTypeID, PaymentMethodID, DateOrdered) VALUES
	(2, 3, 1, 1, '2020-4-17')

DECLARE @OrderID int = @@IDENTITY
DECLARE @WaterID int
DECLARE @SeafoodID int

SELECT TOP 1 @WaterID = DrinkID FROM Drinks
	WHERE Name = 'water'

SELECT TOP 1 @SeafoodID = miv.MenuItemVariationID FROM MenuItemVariations miv
	JOIN MenuItems mi ON mi.MenuItemID = miv.MenuItemID
	ORDER BY miv.Price DESC

INSERT INTO OrderItems (OrderID, MenuItemVariationID, DrinkID) VALUES
	(@OrderID, NULL, @WaterID),
	(@OrderID, @SeafoodID, NULL)

-- For query 14
INSERT INTO CuisineTypes VALUES ('Swedish')
