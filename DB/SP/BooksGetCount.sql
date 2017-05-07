use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksGetCount]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksGetCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 18/01/2017
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[BooksGetCount]
@libraryId int
AS
BEGIN
	
	SELECT count(1) as num
	FROM [bibliotecaTasnad].[dbo].[Books]
	where Id_Library = @libraryId
END