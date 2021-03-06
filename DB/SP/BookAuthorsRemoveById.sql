
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookAuthorsRemoveById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BookAuthorsRemoveById]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <12.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[BookAuthorsRemoveById]
	@bookId int
AS
BEGIN
	SET NOCOUNT ON;

	begin
		DELETE [dbo].[BookAuthors] 
		Where Id_Book = @bookId
	END
END