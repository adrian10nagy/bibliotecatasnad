use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ISBNsRemoveByBookId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ISBNsRemoveByBookId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <12.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[ISBNsRemoveByBookId]
	@bookId int
AS
BEGIN
	SET NOCOUNT ON;

	begin
		DELETE [dbo].[ISBN] 
		Where Id_Book = @bookId
	END
END