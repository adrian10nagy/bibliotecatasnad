use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansGetAllByUserId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LoansGetAllByUserId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 12/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[LoansGetAllByUserId]
@userId int
AS
BEGIN
	
    -- Insert statements for procedure here
SELECT L.[Id]
      ,L.[Id_Book] as BookId
      ,L.[LoanDate]
      ,L.[ReturnedDate]
	  ,U.FirstName
	  ,U.LastName
	  ,B.Title
	  ,B.InternalNr
  FROM [dbo].[Loans] L
	INNER JOIN Users U
		ON L.Id_User = U.Id
	INNER JOIN Books B
		ON L.Id_Book = B.Id
	WHERE L.[Id_User] = @userId

END