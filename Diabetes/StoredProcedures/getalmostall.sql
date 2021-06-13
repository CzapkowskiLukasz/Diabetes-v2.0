DROP PROCEDURE getfood
GO

CREATE PROCEDURE [dbo].[getfood]
AS
SELECT Id, Name, Calories, Carbohydrates, GI
FROM Food