use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansRemove]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LoansRemove]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <12.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[LoansRemove]
	@loanId int
AS
BEGIN
	SET NOCOUNT ON;

	begin
		DELETE [dbo].[Loans] 
		Where Id = @loanId
	END
END