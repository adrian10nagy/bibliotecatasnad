use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LoansAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <11.02.2017>
-- =============================================
CREATE PROCEDURE [dbo].[LoansAdd]
	@BookId int,
	@UserId int,
	@LoanDate datetime
AS
BEGIN
	SET NOCOUNT ON;

	begin
		Insert into [dbo].Loans(
			Id_Book,
			Id_User,
			LoanDate
		)
		values(
			@BookId,
			@UserId,
			@LoanDate
		)

	
	END
END