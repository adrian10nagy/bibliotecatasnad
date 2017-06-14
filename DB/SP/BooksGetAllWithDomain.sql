use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksGetAllWithDomain]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksGetAllWithDomain]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 03/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[BooksGetAllWithDomain]
@libraryId int
AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT B.[Id]
      ,B.[Title]
      ,B.[PublishYear]
      ,B.[Volume]
      ,B.[InternalNr]
      ,B.[NrPages]
      ,B.[Id_Publisher]
	  ,P.Name as PublisherName
      ,B.[Id_BookCondition]
      ,B.[Id_BookFormat]
      ,B.[Id_Language]
	  ,B.Id_BookDomain
	  ,BD.Name as DomainName
	  ,BD.CZU as CZU
  FROM dbo.[Books] B
  Inner Join Publishers P
	ON B.Id_Publisher = P.Id
  Inner Join BookDomains BD
	ON BD.Id = B.Id_BookDomain
	WHERE B.Id_Library = @libraryId
END