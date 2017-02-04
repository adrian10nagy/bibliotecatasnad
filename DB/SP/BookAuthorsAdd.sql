use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookAuthorsAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BookAuthorsAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <21.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[BookAuthorsAdd]
	@authorId int,
	@bookId int,
	@bookAthorType int
AS
BEGIN
	SET NOCOUNT ON;
	
	begin
		Insert into [dbo].BookAuthors(
		[Id_Author],
		[Id_Book],
		[Id_BookAuthorType]
		)
		values(
			@authorId,
			@bookId,
			@bookAthorType
		)
	END
END