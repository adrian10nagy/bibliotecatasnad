use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetForLogin]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserGetForLogin]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 14/01/2017
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[UserGetForLogin]
	@username varchar(MAX),
	@password varchar(MAX)
AS
BEGIN
	
	SELECT top 1
	   U.[Id]
	  ,U.[FirstName]
      ,U.[LastName]
      ,U.[Email] 
	  ,U.[Flags]
      ,U.[Id_UserType]
	  ,U.Id_Locality
	  ,L.Name as Locality
	  ,Lib.Id as LibraryId
	  ,Lib.Name as LibraryName
	FROM [dbo].[Users] U
		INNER JOIN Localities L
		ON U.Id_Locality = L.Id
		INNER JOIN Libraries Lib
		ON U.Id_Library = Lib.Id
	   where [PasswordHashed] = @password
	   AND [UserName] = @username

END