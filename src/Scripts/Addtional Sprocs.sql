Create PROCEDURE dbo.sprocEmployeePositionGetALL
@EmployeePositionID int
AS
BEGIN
	SET NOCOUNT ON
	SELECT Name From EmployeePositions
	Where EmployeePositionID = @EmployeePositionID
END
GO

Create PROCEDURE dbo.sproc_EmployeePositionAdd
@Name nvarchar(50)
, @EmployeePositionID int = 0 OUTPUT
AS
BEGIN
SET NOCOUNT ON
	SELECT @EmployeePositionID = EmployeePositionID FROM EmployeePositions
		WHERE [Name] = @Name
	IF @@ROWCOUNT < 1
		BEGIN
			INSERT INTO EmployeePositions ([Name])
				VALUES (@Name)
			SET @EmployeePositionID = @@IDENTITY
		END
END
GO