use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UsersAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <26.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[UsersAdd]
	@FirstName nvarchar(MAX),
	@LastName nvarchar(MAX),
	@Username nvarchar(MAX) = null,
	@HomeAddress nvarchar(MAX) = null,
	@Birthdate datetime = null,
	@Phone nvarchar(MAX) = null,
	@Email nvarchar(MAX) = null,
	@FacebookAddress nvarchar(MAX) = null,
	@Password nvarchar(MAX) = null,
	@JoinDate datetime,
	@Flags int,
	@Gender int,
	@LocalityId int,
	@LibraryId int,
	@NationalityId int,
	@UserType int
AS
BEGIN
	SET NOCOUNT ON;

	declare @id int
	
	begin
		Insert into [dbo].[Users](
      [FirstName]
      ,[LastName]
      ,[UserName]
	  ,[PasswordHashed]
      ,[HomeAddress]
      ,[Birthdate]
      ,[Phone]
      ,[Email]
      ,[FacebookAddress]
      ,[JoinDate]
      ,[Flags]
      ,[Gender]
      ,[Id_Locality]
      ,[Id_UserType]
	  ,[Id_Nationality]
	  ,[Id_Library])
		values(
			@FirstName,
			@LastName,
			@Username,
			@Password,
			@HomeAddress,
			@Birthdate,
			@Phone,
			@Email,
			@FacebookAddress,
			@JoinDate,
			@Flags,
			@Gender,
			@LocalityId,
			@UserType,
			@NationalityId,
			@LibraryId)

		select @id = cast(Scope_Identity() as int)
	
	select @id as [id]
	
	END
END