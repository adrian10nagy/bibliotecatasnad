
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReservationsGetBookSatus]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ReservationsGetBookSatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 12/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[ReservationsGetBookSatus]
@bookId int
AS
BEGIN
	
    -- Insert statements for procedure here

	SELECT ReservedDate as reservationDate 
	from [dbo].[Reservations] R
	WHERE R.Flags = 1
	AND R.Id_Book = @bookId

END