use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthorsGetAll]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[AuthorsGetAll]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 21/01/2017
-- =============================================
CREATE PROCEDURE [dbo].[AuthorsGetAll]
AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT [Id]
      ,[Name]
  FROM [Authors]
END