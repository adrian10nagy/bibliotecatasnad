
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksUpdate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <26.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[BooksUpdate]
	@Id int,
	@BookConditionId int,
	@BookDomainId int,
	@BookFormatId int,
	@BookLanguageId int,
	@BookSubjectId int,
	@InternalNr nvarchar(MAX),
	@LibraryId int,
	@NrPages int,
	@PublisherId int,
	@PublishYear int = null,
	@Title nvarchar(MAX),
	@Volume nvarchar(MAX) = null,
	@Description nvarchar(MAX) = null,
	@ImageUrl nvarchar(MAX) = null,
	@PreviewLink nvarchar(MAX) = null
AS
BEGIN
	SET NOCOUNT ON;

	begin
		UPDATE [dbo].[Books] set 
      [Title] = @Title
      ,[PublishYear] = @PublishYear
      ,[Volume] = @Volume
      ,[InternalNr] = @InternalNr
      ,[NrPages] = @NrPages
      ,[Id_Publisher] = @PublisherId
      ,[Id_BookCondition] = @BookConditionId
      ,[Id_Library] = @LibraryId
      ,[Id_BookFormat] = @BookFormatId 
      ,[Id_BookDomain] = @BookDomainId 
      ,[Id_BookSubject] = @BookSubjectId
      ,[Id_Language] = @BookLanguageId
	  ,[Description] = @Description
	  ,[ImageUrl] = @ImageUrl
	  ,[PreviewLink] = @PreviewLink
		Where Id = @Id
	END
END