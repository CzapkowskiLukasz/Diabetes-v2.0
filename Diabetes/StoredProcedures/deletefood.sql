DROP PROCEDURE foodDelete
GO

CREATE PROCEDURE [dbo].[foodDelete]
	@foodId int
AS
	DELETE FROM Food WHERE Id = @foodId