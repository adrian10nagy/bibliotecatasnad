use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRightsGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserRightsGetAll]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 27/06/2017
-- =============================================
CREATE PROCEDURE [dbo].[UserRightsGetAll]
AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT
		[Id_user]
       ,[Id_functionality]
  FROM [bibliotecaTasnad].[dbo].[UserRights]

END