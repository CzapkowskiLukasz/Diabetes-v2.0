DROP PROCEDURE search
GO

CREATE PROCEDURE [dbo].[search]
	@name NCHAR(50)
AS
	SELECT * FROM Food
	WHERE Name = @name