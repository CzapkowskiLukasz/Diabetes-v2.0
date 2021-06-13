
CREATE PROCEDURE [dbo].[createfood]
	@name VARCHAR (50),
	@calories int,
	@carbohydrates float,
	@proteins float,
	@fats float,
	@GI int,
	@GL int,
	@main int,
	@sec int,
	@foodID int OUTPUT
AS
	INSERT INTO Food (Name, Calories, Carbohydrates, Proteins, Fats, GI, GL, MainCat, SecCat) VALUES (@name, @calories, @carbohydrates, @proteins, @fats, @GI, @GL, @main, @sec)
	SET @foodID = @@IDENTITY