
CREATE PROCEDURE [dbo].[createfoodonecat]
	@name VARCHAR (50),
	@calories int,
	@carbohydrates float,
	@proteins float,
	@fats float,
	@GI int,
	@GL int,
	@main int,
	@foodID int OUTPUT
AS
	INSERT INTO Food (Name, Calories, Carbohydrates, Proteins, Fats, GI, GL, MainCat) VALUES (@name, @calories, @carbohydrates, @proteins, @fats, @GI, @GL, @main)
	SET @foodID = @@IDENTITY