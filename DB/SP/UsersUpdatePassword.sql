
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersUpdatePassword]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UsersUpdatePassword]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <26.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[UsersUpdatePassword]
	@Id int,
	@passwordOld nvarchar(MAX),
	@passwordNew nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	if exists(select Id from Users where Id = @Id AND PasswordHashed = @passwordOld)
	begin
		UPDATE [dbo].[Users] set 
      [PasswordHashed] = @passwordNew
		Where Id = @Id
		Select 1 as result
	END
	ELSE
	begin
		Select 2 as result
	end
END