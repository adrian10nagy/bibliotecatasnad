use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersGetById]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UsersGetById]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 14/01/2017
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[UsersGetById]
	@id int
AS
BEGIN
	
	SELECT top 1 
	   [FirstName]
      ,[LastName]
      ,[UserName]
      ,[HomeAddress]
      ,[Birthdate]
      ,[Phone]
      ,[Email]
      ,[FacebookAddress] 
      ,[Gender]
      ,[Id_Locality] 
      ,[Id_UserType]
      ,[Id_Nationality]
	FROM [dbo].[Users]
	   where [Id] = @id

END