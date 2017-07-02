
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksGetCountByAddedUserId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksGetCountByAddedUserId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 18/01/2017
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[BooksGetCountByAddedUserId]
@userId int,
@libraryId int
AS
BEGIN
	
	SELECT count(1) as num
	FROM [bibliotecaTasnad].[dbo].[Books]
	where Id_AddedBy = @userId
	AND Id_Library = @libraryId
	and [AddedDate] BETWEEN 
	 CAST(GETDATE() AS DATE) AND DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
END