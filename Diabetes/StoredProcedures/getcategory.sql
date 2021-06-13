DROP PROCEDURE getcategory
GO

CREATE PROCEDURE [dbo].[getcategory]
	@id int
AS
SELECT Name
FROM Category where Id=@id