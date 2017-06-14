use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansGetCount]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LoansGetCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 18/01/2017
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[LoansGetCount]
@libraryId int
AS
BEGIN
	
	SELECT count(1) as num
	FROM [bibliotecaTasnad].[dbo].[Loans] L
	Inner Join Books B
		ON L.Id_Book = B.Id
	where B.Id_Library = @libraryId
	
END