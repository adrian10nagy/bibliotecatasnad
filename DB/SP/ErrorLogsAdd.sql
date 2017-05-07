use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorLogsAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ErrorLogsAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <21.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[ErrorLogsAdd]
	@message nvarchar(MAX),
	@created datetime
AS
BEGIN
	SET NOCOUNT ON;
	
	begin
		Insert into [dbo].ErrorLogs
		(
		[Message],
		Created 
		)
		values
		(
			@message,
			@created
		)
	
	END
END