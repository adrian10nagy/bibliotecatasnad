use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksPublishersGetGrouped]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksPublishersGetGrouped]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 03/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[BooksPublishersGetGrouped]
@libraryId int
AS
BEGIN
	
    -- Insert statements for procedure here

	SELECT 
      count(Id_Publisher) as NrCount
	  ,P.Name as PublisherName
	  FROM dbo.[Books] B
	  Inner Join Publishers P
	  ON B.Id_Publisher = P.Id
	  WHERE B.Id_Library = @libraryId
	  GROUP BY P.Name
	  Order by NrCount DESC

END