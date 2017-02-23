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
	   [Id]
	  ,[FirstName]
      ,[LastName]
      ,[Email] 
	  ,[Flags]
      ,[Id_UserType]
	FROM [dbo].[Users]
	   where [PasswordHashed] = @password
	   AND [UserName] = @username

END