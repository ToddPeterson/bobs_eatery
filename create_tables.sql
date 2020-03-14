

Create Table States(

	StateID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

)

Create Table Cities(

	CityID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	StateID int FOREIGN KEY REFERENCES States(StateID),

)

Create Table Customers(

	CustomerID int IDENTITY(1,1) PRIMARY KEY,

	FirstName nvarchar(50) NOT NULL,

	MiddleName nvarchar(50) NOT NULL, ----- I have this as not null

	LastName nvarchar(50) NOT NULL,

	PhoneNumber nvarchar(15) NOT NULL,

	Email nvarchar(15) NOT NULL,

	StreetAddress nvarchar(50) NOT NULL,

	CityID int FOREIGN KEY REFERENCES Cities(CityID),

)

Create Table Accounts(

	AccountID int IDENTITY(1,1) PRIMARY KEY,

	UserName nvarchar(50) NOT NULL,

	[Password] nvarchar(50) NOT NULL,

	CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID)

) 

Create Table CuisineTypes(

	CuisineTypeID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL

)

Create Table Categories(

	CategoryID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL

)

Create Table PrepMethods(

	PrepMethodID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL

)

Create Table MenuItems(

	MenuItemID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	[Description] nvarchar(max) NOT NULL,

	PicturePath nvarchar(255) NOT NULL,

	IsSideItem bit NOT NULL,

	PrepTime smallint NOT NULL, -- I made this small int cause i guess we are storing in minutes

	PrepMethodID int FOREIGN KEY REFERENCES PrepMethods(PrepMethodID),

	CategoryID int FOREIGN KEY REFERENCES Categories(CategoryID),

	CuisineTypeID int FOREIGN KEY REFERENCES CuisineTypes(CuisineTypeID),

)

Create Table SideItemPairings(

	SideItemPairingID int IDENTITY(1,1) PRIMARY KEY,

	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),

	SideItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID)

)

Create Table Ratings(

	RatingID int IDENTITY(1,1) PRIMARY KEY,

	RatingValue smallint NOT NULL, -- I made this small int

	AccountID int FOREIGN KEY REFERENCES Accounts(AccountID),

	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),

)

Create Table DietaryRestrictions(

	DietaryRestrictionID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),

)

Create Table MenuItemVariations(

	MenuItemVariationID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	Price money NOT NULL,

	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),

)

Create Table DrinkTypes(

	DrinkTypeID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	HasFreeRefills bit NOT NULL

)

Create Table Drinks(

	DrinkID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	Price money NOT NULL,

	DrinkTypeID int FOREIGN KEY REFERENCES DrinkTypes(DrinkTypeID),

)

Create Table Vendors(

	VendorID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	DateDelivered DateTime NOT NULL

)

Create Table Ingredients(

	IngredientID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

	StorageLocation nvarchar(50) NOT NULL,

	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),

	VendorID int FOREIGN KEY REFERENCES Vendors(VendorID),

)

Create Table TableStyles(

	TableStyleID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

)

Create Table [Tables](

	TableID int IDENTITY(1,1) PRIMARY KEY,

	Number int NOT NULL, 

	NumberOfSeats int NOT NULL, 

	TableStyleID int FOREIGN KEY REFERENCES TableStyles(TableStyleID)

)

Create Table EmployeePositions(

	EmployeePositionID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL,

)

Create Table Employees(

	EmployeeID int IDENTITY(1,1) PRIMARY KEY,

	FirstName nvarchar(50) NOT NULL,

	MiddleName nvarchar(50) NOT NULL, ----- I have this as not null

	LastName nvarchar(50) NOT NULL,

	PhoneNumber nvarchar(15) NOT NULL,

	Wage money NOT NULL,

	ContactFirstName nvarchar(50) NOT NULL,

	ContactMiddleName nvarchar(50) NOT NULL, -- I have this as NOT null

	ContactLastName nvarchar(50) NOT NULL,

	ContactPhoneNumber nvarchar(15) NOT NULL, -- made this nvarchar(15)

	StreetAddress nvarchar(50) NOT NULL,

	CityID int FOREIGN KEY REFERENCES Cities(CityID), -- i used FK for cities

	EmployeePositionID int FOREIGN KEY REFERENCES EmployeePositions(EmployeePositionID)

)

Create Table Seatings(

	SeatingID int IDENTITY(1,1) PRIMARY KEY,

	TimeSatDown int NOT NULL, -- I prefer Time instead of int

	TimeLeft int NOT NULL, -- I prefer Time instead of int

	TableID int FOREIGN KEY REFERENCES [Tables](TableID),

	EmployeeID int FOREIGN KEY REFERENCES Employees(EmployeeID)

)

Create Table CustomersToTables(

	CustomerToSeatingID int IDENTITY(1,1) PRIMARY KEY,

	CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID),

	SeatingID int FOREIGN KEY REFERENCES Seatings(SeatingID),

)

Create Table Shifts(

	ShiftID int IDENTITY(1,1) PRIMARY KEY,

	DateTimeIn DateTime NOT NULL,

	DateTimeOut DateTime NOT NULL,

	EmployeeID int FOREIGN KEY REFERENCES Employees(EmployeeID),

)

Create Table PaymentMethods(

	PaymentMethodID int IDENTITY(1,1) PRIMARY KEY,

	[Name] nvarchar(50) NOT NULL

)

Create Table OrderTypes(

	OrderTypeID int IDENTITY(1,1) PRIMARY KEY,

	[Type] nvarchar(30) NOT NULL

)

Create Table Orders(

	OrderID int IDENTITY(1,1) PRIMARY KEY,

	CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID),

	SeatingID int FOREIGN KEY REFERENCES Seatings(SeatingID),

	OrderTypeID int FOREIGN KEY REFERENCES OrderTypes(OrderTypeID),

	PaymentMethodID int FOREIGN KEY REFERENCES PaymentMethods(PaymentMethodID),

)

Create Table OrderItems(

	OrderItemID int IDENTITY(1,1) PRIMARY KEY,

	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),

	MenuItemVariationID int FOREIGN KEY REFERENCES MenuItemVariations(MenuItemVariationID),

	DrinkID int FOREIGN KEY REFERENCES Drinks(DrinkID),

)

Create Table CheckDetails(

	CheckDetailID int IDENTITY(1,1) PRIMARY KEY,

	CheckNumber int NOT NULL,

	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),

)

Create Table Deliveries(

	DeliveryID int IDENTITY(1,1) PRIMARY KEY,

	Charge money NOT NULL, -- I set this to money

	DateTimeOut DateTime NOT NULL,

	DateTimeIn DateTime NOT NULL,

	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),

	EmployeeID int FOREIGN KEY REFERENCES Employees(EmployeeID),

)

