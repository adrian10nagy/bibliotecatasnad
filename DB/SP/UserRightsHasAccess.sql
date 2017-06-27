use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRightsHasAccess]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserRightsHasAccess]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 27/06/2017
-- =============================================
CREATE PROCEDURE [dbo].[UserRightsHasAccess]
@userId int,
@functionalityId int 
AS
BEGIN

    -- Insert statements for procedure here
	if exists(SELECT 1 FROM [bibliotecaTasnad].[dbo].[UserRights] 
		where Id_user = @userId and Id_functionality = @functionalityId)	
		BEGIN
			select 1 as hasValue
		END
	ELSE
		BEGIN
			select 0 as hasValue
		END
END