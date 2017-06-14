use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UsersUpdate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <26.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[UsersUpdate]
	@Id int,
	@FirstName nvarchar(MAX),
	@LastName nvarchar(MAX),
	@Username nvarchar(MAX) = null,
	@HomeAddress nvarchar(MAX) = null,
	@Birthdate datetime = null,
	@Phone nvarchar(MAX) = null,
	@Email nvarchar(MAX) = null,
	@FacebookAddress nvarchar(MAX) = null,
	@Gender int,
	@LocalityId int,
	@LibraryId int,
	@NationalityId int,
	@UserType int
AS
BEGIN
	SET NOCOUNT ON;

	begin
		UPDATE [dbo].[Users] set 
      [FirstName] = @FirstName
      ,[LastName] = @LastName
      ,[UserName] = @Username
      ,[HomeAddress] = @HomeAddress
      ,[Birthdate] = @Birthdate
      ,[Phone] = @Phone
      ,[Email] = @Email
      ,[FacebookAddress] = @FacebookAddress
      ,[Gender] = @Gender
      ,[Id_Locality] = @LocalityId
      ,[Id_UserType] = @UserType
      ,[Id_Nationality] = @NationalityId
	  ,[Id_Library] = @LibraryId	
		Where Id = @Id
	END
END