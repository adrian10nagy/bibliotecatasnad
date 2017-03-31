
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksGetLast]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksGetLast]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 03/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[BooksGetLast]
@nr int
AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT top(@nr) [Id]
      ,[Title]
  FROM dbo.[Books] 
    order by [AddedDate] desc
END