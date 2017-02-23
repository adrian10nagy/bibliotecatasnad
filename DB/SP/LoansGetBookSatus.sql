use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansGetBookSatus]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LoansGetBookSatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 12/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[LoansGetBookSatus]
@bookId int
AS
BEGIN
	
    -- Insert statements for procedure here

	IF EXISTS (SELECT 1 from [dbo].[Loans] L
	WHERE L.ReturnedDate is not null 
	AND Id_Book = @bookId)
	BEGIN
	  Select 2 as bookstatus
	END
	ELSE
	BEGIN
	  Select 1 as bookstatus
	END

END