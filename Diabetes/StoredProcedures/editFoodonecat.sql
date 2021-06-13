
CREATE PROCEDURE [dbo].[editFoodonecat]
	@foodID int,
	@name VARCHAR (50),
	@calories int,
	@carbohydrates float,
	@proteins float,
	@fats float,
	@GI int,
	@GL int,
	@main int
AS
	UPDATE Food SET name = @name, Calories=@calories, Carbohydrates = @carbohydrates, Proteins=@proteins, Fats=@fats, GI=@GI, GL=@GL, MainCat=@main WHERE Id = @foodID