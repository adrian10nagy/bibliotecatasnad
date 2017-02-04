use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookAuthorsGetByBookId]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BookAuthorsGetByBookId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 04/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[BookAuthorsGetByBookId]
@bookId int
AS
BEGIN
    -- Insert statements for procedure here
	SELECT 
       BA.[Id_Author]
      ,BA.[Id_BookAuthorType]
      
  FROM dbo.[BookAuthors] BA
	INNER JOIN Authors A
	ON A.Id = BA.Id_Book
  WHERE [Id_Book] = @bookId

END