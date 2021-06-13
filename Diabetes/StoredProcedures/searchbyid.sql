DROP PROCEDURE searchByID
GO

CREATE PROCEDURE [dbo].[searchByID]
	@foodId int
AS
	SELECT * FROM Food
	WHERE Id = @foodId