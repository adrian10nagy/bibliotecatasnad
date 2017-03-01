
use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BooksGetAllByDate]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[BooksGetAllByDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Adrian Nagy
-- Create date: 12/02/2017
-- =============================================
CREATE PROCEDURE [dbo].[BooksGetAllByDate]
@date date
AS
BEGIN
	
    -- Insert statements for procedure here
SELECT B.[Id]
      ,B.[Title]
      ,B.[PublishYear]
      ,B.[Volume]
      ,B.[InternalNr]
      ,B.[NrPages]
      ,B.[Id_Publisher]
	  ,P.Name as PublisherName
      ,B.[Id_BookCondition]
      ,B.[Id_BookFormat]
      ,B.[Id_Language]
  FROM dbo.[Books] B
  Inner Join Publishers P
  ON B.Id_Publisher = P.Id
	WHERE B.[AddedDate] > @date
	AND B.[AddedDate] < dateadd(DD, 1, @date)

END