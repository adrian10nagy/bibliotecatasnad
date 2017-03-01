

use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersGetAllByDate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UsersGetAllByDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 12/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[UsersGetAllByDate]
@date date
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
      ,U.[Id_Nationality]
	  ,L.Name as Locality
  FROM dbo.[Users] U
  Inner Join Localities L
  ON U.Id_Locality = L.Id
	WHERE U.[JoinDate] > @date
	AND U.[JoinDate] < dateadd(DD, 1, @date)

END