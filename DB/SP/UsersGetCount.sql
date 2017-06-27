use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersGetCount]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UsersGetCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 18/01/2017
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[UsersGetCount]
@libraryId int
AS
BEGIN
	
		SELECT count(1) as num
	FROM [bibliotecaTasnad].[dbo].[Users] U
	WHERE U.Id_Library = @libraryId
END