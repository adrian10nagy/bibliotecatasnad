use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReservationsAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ReservationsAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <25.03.2017>
-- =============================================
CREATE PROCEDURE [dbo].[ReservationsAdd]
	@BookId int,
	@UserId int,
	@LoanDate datetime
AS
BEGIN
	SET NOCOUNT ON;

	begin
		Insert into [dbo].Reservations(
			Id_Book,
			Id_User,
			ReservedDate
		)
		values(
			@BookId,
			@UserId,
			@LoanDate
		)

	
	END
END