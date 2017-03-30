use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersGetByEmail]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UsersGetByEmail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 14/01/2017
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[UsersGetByEmail]
	@email nvarchar(MAX)
AS
BEGIN
	
	SELECT top 1 
		[Id]
	  ,[FirstName]
      ,[LastName]
      ,[UserName]
      ,[HomeAddress]
      ,[Birthdate]
      ,[Phone]
      ,[FacebookAddress] 
      ,[Gender]
      ,[Id_Locality] 
      ,[Id_UserType]
      ,[Id_Nationality]
	FROM [dbo].[Users]
	   where [Email] = @email

END