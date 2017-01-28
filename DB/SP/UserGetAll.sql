use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserGetAll]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 27/01/2017
-- =============================================
CREATE PROCEDURE [dbo].[UserGetAll]
AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT U.[Id]
      ,U.[FirstName]
      ,U.[LastName]
      ,U.[UserName]
      ,U.[HomeAddress]
      ,U.[Birthdate]
      ,U.[Phone]
      ,U.[Email]
      ,U.[FacebookAddress]
      ,U.[JoinDate]
      ,U.[Flags]
      ,U.[Gender]
      ,U.[Id_Locality]
      ,U.[Id_UserType]
	  ,L.Name as Locality
  FROM dbo.[Users] U
  Inner Join Localities L
  ON U.Id_Locality = L.Id
END