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

-- For query 14
INSERT INTO CuisineTypes VALUES ('Swedish')
