use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksGetById]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 04/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[BooksGetById]
@bookId int
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
	  ,B.[Id_BookSubject]
	  ,BS.Name as SubjectName
	  ,B.[Id_BookDomain]
	  ,BD.Name as DomainName
  FROM dbo.[Books] B
	Inner Join Publishers P
  ON B.Id_Publisher = P.Id
    Inner Join BookSubjects BS
  ON B.Id_BookSubject = BS.Id
    Inner Join BookDomains BD
  ON B.Id_BookDomain = BD.Id
  WHERE B.[Id] = @bookId
END
