use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ISBNAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ISBNAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <21.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[ISBNAdd]
	@bookId int,
	@value nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	declare @id int
	
	begin
		Insert into [dbo].ISBN(
		[Value],
		[Id_book]
		)
		values(@value, @bookId)

		select @id = cast(Scope_Identity() as int)
	
	select @id as [id]
	
	END
END