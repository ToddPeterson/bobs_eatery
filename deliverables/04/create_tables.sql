USE BobsAwesomeEatery

CREATE TABLE States (
	StateID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
)

CREATE TABLE Cities (
	CityID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	StateID int FOREIGN KEY REFERENCES States(StateID),
	ZipCode nvarchar(20) NOT NULL
)

CREATE TABLE Customers (
	CustomerID int IDENTITY(1,1) PRIMARY KEY,
	FirstName nvarchar(50) NOT NULL,
	MiddleName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	PhoneNumber nvarchar(15) NOT NULL,
	Email nvarchar(100) NOT NULL,
	StreetAddress nvarchar(50) NOT NULL,
	CityID int FOREIGN KEY REFERENCES Cities(CityID),
)

CREATE TABLE Accounts (
	AccountID int IDENTITY(1,1) PRIMARY KEY,
	UserName nvarchar(50) NOT NULL,
	[Password] nvarchar(50) NOT NULL,
	CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID)
)

CREATE TABLE CuisineTypes (
	CuisineTypeID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL
)

CREATE TABLE Categories (
	CategoryID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL
)

CREATE TABLE PrepMethods (
	PrepMethodID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL
)

CREATE TABLE MenuItems (
	MenuItemID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	PicturePath nvarchar(255) NOT NULL,
	IsSideItem bit NOT NULL,
	PrepTime int NOT NULL,
	PrepMethodID int FOREIGN KEY REFERENCES PrepMethods(PrepMethodID),
	CategoryID int FOREIGN KEY REFERENCES Categories(CategoryID),
	CuisineTypeID int FOREIGN KEY REFERENCES CuisineTypes(CuisineTypeID),
)

CREATE TABLE SideItemPairings (
	SideItemPairingID int IDENTITY(1,1) PRIMARY KEY,
	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),
	SideItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID)
)

CREATE TABLE Ratings (
	RatingID int IDENTITY(1,1) PRIMARY KEY,
	RatingValue smallint NOT NULL,
	AccountID int FOREIGN KEY REFERENCES Accounts(AccountID),
	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),
)

CREATE TABLE DietaryRestrictions (
	DietaryRestrictionID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),
)

CREATE TABLE MenuItemVariations (
	MenuItemVariationID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	Price money NOT NULL,
	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),
)

CREATE TABLE DrinkTypes (
	DrinkTypeID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	HasFreeRefills bit NOT NULL
)

CREATE TABLE Drinks (
	DrinkID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	Price money NOT NULL,
	DrinkTypeID int FOREIGN KEY REFERENCES DrinkTypes(DrinkTypeID),
)

CREATE TABLE Vendors (
	VendorID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	DateDelivered DateTime NOT NULL
)

CREATE TABLE Ingredients (
	IngredientID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
	StorageLocation nvarchar(50) NOT NULL,
	MenuItemID int FOREIGN KEY REFERENCES MenuItems(MenuItemID),
	VendorID int FOREIGN KEY REFERENCES Vendors(VendorID),
)

CREATE TABLE TableStyles (
	TableStyleID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
)

CREATE TABLE [Tables] (
	TableID int IDENTITY(1,1) PRIMARY KEY,
	Number int NOT NULL,
	NumberOfSeats int NOT NULL,
	TableStyleID int FOREIGN KEY REFERENCES TableStyles(TableStyleID)
)

CREATE TABLE EmployeePositions (
	EmployeePositionID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL,
)

CREATE TABLE Employees (
	EmployeeID int IDENTITY(1,1) PRIMARY KEY,
	FirstName nvarchar(50) NOT NULL,
	MiddleName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	PhoneNumber nvarchar(15) NOT NULL,
	Wage money NOT NULL,
	ContactFirstName nvarchar(50) NOT NULL,
	ContactMiddleName nvarchar(50) NOT NULL,
	ContactLastName nvarchar(50) NOT NULL,
	ContactPhoneNumber nvarchar(15) NOT NULL,
	StreetAddress nvarchar(50) NOT NULL,
	CityID int FOREIGN KEY REFERENCES Cities(CityID),
	EmployeePositionID int FOREIGN KEY REFERENCES EmployeePositions(EmployeePositionID),
	EmployeeNumber INT UNIQUE NOT NULL
)

CREATE TABLE Seatings (
	SeatingID int IDENTITY(1,1) PRIMARY KEY,
	TimeSatDown datetime NOT NULL,
	TimeLeft datetime NOT NULL,
	TableID int FOREIGN KEY REFERENCES [Tables](TableID),
	EmployeeID int FOREIGN KEY REFERENCES Employees(EmployeeID)
)

CREATE TABLE CustomersToTables (
	CustomerToSeatingID int IDENTITY(1,1) PRIMARY KEY,
	CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID),
	SeatingID int FOREIGN KEY REFERENCES Seatings(SeatingID),
)

CREATE TABLE Shifts (
	ShiftID int IDENTITY(1,1) PRIMARY KEY,
	DateTimeIn DateTime NOT NULL,
	DateTimeOut DateTime NOT NULL,
	EmployeeID int FOREIGN KEY REFERENCES Employees(EmployeeID),
)

CREATE TABLE PaymentMethods (
	PaymentMethodID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(50) NOT NULL
)

CREATE TABLE OrderMethods (
	OrderMethodID int IDENTITY(1,1) PRIMARY KEY,
	Method nvarchar(30) NOT NULL
)

CREATE TABLE OrderTypes (
	OrderTypeID int IDENTITY(1,1) PRIMARY KEY,
	[Type] nvarchar(30) NOT NULL,
	OrderMethodID int FOREIGN KEY REFERENCES OrderMethods(OrderMethodID)
)

CREATE TABLE Orders (
	OrderID int IDENTITY(1,1) PRIMARY KEY,
	CustomerID int FOREIGN KEY REFERENCES Customers(CustomerID),
	SeatingID int FOREIGN KEY REFERENCES Seatings(SeatingID),
	OrderTypeID int FOREIGN KEY REFERENCES OrderTypes(OrderTypeID),
	PaymentMethodID int FOREIGN KEY REFERENCES PaymentMethods(PaymentMethodID),
	DateOrdered DateTime NOT NULL
)

CREATE TABLE OrderItems (
	OrderItemID int IDENTITY(1,1) PRIMARY KEY,
	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),
	MenuItemVariationID int FOREIGN KEY REFERENCES MenuItemVariations(MenuItemVariationID),
	DrinkID int FOREIGN KEY REFERENCES Drinks(DrinkID),
)

CREATE TABLE CheckDetails (
	CheckDetailID int IDENTITY(1,1) PRIMARY KEY,
	CheckNumber nvarchar(20) NOT NULL,
	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),
)

CREATE TABLE Deliveries (
	DeliveryID int IDENTITY(1,1) PRIMARY KEY,
	Charge money NOT NULL,
	DateTimeOut DateTime NOT NULL,
	DateTimeIn DateTime NOT NULL,
	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),
	EmployeeID int FOREIGN KEY REFERENCES Employees(EmployeeID),
)

