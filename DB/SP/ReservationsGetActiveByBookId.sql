
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReservationsGetActiveByBookId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ReservationsGetActiveByBookId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 12/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[ReservationsGetActiveByBookId]
@bookId int
AS
BEGIN
	
    -- Insert statements for procedure here
SELECT TOP 1 R.[Id]
      ,R.[Id_User] AS UserId
      ,R.[ReservedDate]
      ,R.[Flags]
	  ,U.FirstName
	  ,U.LastName
  FROM [bibliotecaTasnad].[dbo].[Reservations] R
	INNER JOIN Users U
		ON r.Id_User = U.Id
	WHERE R.[Id_Book] = @bookId
	and R.[Flags] = 1

END