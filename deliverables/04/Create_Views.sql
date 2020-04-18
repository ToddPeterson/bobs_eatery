USE BobsAwesomeEatery
GO

CREATE VIEW MenuItemSalesCount AS
	SELECT mi.MenuItemID, COUNT(oi.OrderItemID) AS [Number of Sales] FROM CuisineTypes ct
		JOIN MenuItems mi ON mi.CuisineTypeID = ct.CuisineTypeID
		JOIN MenuItemVariations miv ON miv.MenuItemID = mi.MenuItemID
		LEFT JOIN OrderItems oi ON oi.MenuItemVariationID = miv.MenuItemVariationID
		GROUP BY mi.MenuItemID
GO