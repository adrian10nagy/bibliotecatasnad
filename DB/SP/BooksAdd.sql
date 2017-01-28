use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <19.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[BooksAdd]
	@AddedbyId int,
	@AddedDate datetime,
	@BookConditionId int,
	@BookDomainId int,
	@BookFormatId int,
	@BookLanguageId int,
	@BookSubjectId int,
	@InternalNr nvarchar(MAX),
	@Isbn nvarchar(MAX),
	@LibraryId int,
	@NrPages int,
	@Title nvarchar(MAX),
	@PublisherId int,
	@PublishYear int,
	@Volume nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	declare @id int
	
	if(@id is null)
	begin
		Insert into [dbo].Books(
		[Title]
      ,[PublishYear]
      ,[Volume]
      ,[ISBN]
      ,[InternalNr]
      ,[NrPages]
      ,[AddedDate]
      ,[Id_AddedBy]
      ,[Id_Publisher]
      ,[Id_BookCondition]
      ,[Id_Library]
      ,[Id_BookFormat]
      ,[Id_BookDomain]
      ,[Id_BookSubject]
      ,[Id_Language])
	values(
		@Title,
		@PublishYear,
		@Volume,
		@Isbn,
		@InternalNr,
		@NrPages,
		@AddedDate,
		@AddedbyId,
		@PublisherId,
		@BookConditionId,
		@LibraryId,
		@BookFormatId,
		@BookDomainId,
		@BookSubjectId,
		@BookLanguageId
	)

		select @id = cast(Scope_Identity() as int)
	end
	
	select @id as [id]
END