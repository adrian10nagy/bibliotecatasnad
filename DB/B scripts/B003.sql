
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRightsUpdate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UserRightsUpdate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <26.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[UserRightsUpdate]
	@username nvarchar(MAX),
	@hasRight bit,
	@functionalityId int
AS
BEGIN
	SET NOCOUNT ON;

	if(@hasRight = 1)
		begin
			INSERT INTO UserRights
			VALUES ((select top 1 id from Users where username = @username), @functionalityId);
		END
	else
		begin
		 DELETE FROM UserRights 
		 WHERE id_user = (select top 1 id from Users where username = @username)
		 AND id_functionality = @functionalityId
	END

END