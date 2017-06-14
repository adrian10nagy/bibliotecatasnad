use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksGetByTitlePublisherDomain]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksGetByTitlePublisherDomain]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 12/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[BooksGetByTitlePublisherDomain]
@title nvarchar(MAX),
@publisherId int = null,
@domainId int = null,
@libraryId int
AS
BEGIN
	
    -- Insert statements for procedure here
	IF(@publisherId IS NULL AND @domainId IS NULL)
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
		  FROM dbo.[Books] B
		  Inner Join Publishers P
		  ON B.Id_Publisher = P.Id
			WHERE B.Title like '%' + @title +'%'
			AND B.Id_Library = @libraryId
			
	ELSE IF(@publisherId IS NULL and @domainId IS NOT null)
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
		  FROM dbo.[Books] B
		  Inner Join Publishers P
		  ON B.Id_Publisher = P.Id
			WHERE B.Title like '%' + @title +'%'
			AND B.Id_BookDomain = @domainId
			AND B.Id_Library = @libraryId

	ELSE IF(@domainId IS NULL and @publisherId IS NOT null)
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
		  FROM dbo.[Books] B
		  Inner Join Publishers P
		  ON B.Id_Publisher = P.Id
			WHERE B.Title like '%' + @title +'%'
			AND B.Id_Publisher = @publisherId
			AND B.Id_Library = @libraryId

	ELSE
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
		  FROM dbo.[Books] B
		  Inner Join Publishers P
		  ON B.Id_Publisher = P.Id
			WHERE B.Title like '%' + @title +'%'
			AND B.Id_Publisher = @publisherId
			AND B.Id_BookDomain = @domainId
			AND B.Id_Library = @libraryId

END