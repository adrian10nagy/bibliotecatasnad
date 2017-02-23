use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansReturn]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LoansReturn]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <12.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[LoansReturn]
	@loanId int,
	@returnedDate datetime
AS
BEGIN
	SET NOCOUNT ON;

	begin
		UPDATE [dbo].[Loans] set 
      [ReturnedDate] = @returnedDate
		Where Id = @loanId
	END
END