
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ISBNGetByBookId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[ISBNGetByBookId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 04/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[ISBNGetByBookId]
@bookId int
AS
BEGIN
    -- Insert statements for procedure here
	SELECT 
		[Id],
		[Value]
		
  FROM dbo.[ISBN] 
  WHERE [Id_Book] = @bookId

END