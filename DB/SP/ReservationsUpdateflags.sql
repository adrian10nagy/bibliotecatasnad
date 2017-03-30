

use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReservationsUpdateflags]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ReservationsUpdateflags]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <26.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[ReservationsUpdateflags]
	@bookId int,
	@flag int
AS
BEGIN
	SET NOCOUNT ON;

	begin
		UPDATE [dbo].[Reservations] set 
      [Flags] = @flag
      
	
		Where [Id_Book] = @bookId
	END
END