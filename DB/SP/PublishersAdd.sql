use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublishersAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[PublishersAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <21.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[PublishersAdd]
	@Name nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	declare @id int
	
	begin
		Insert into [dbo].Publishers([Name])
		values(@name)

		select @id = cast(Scope_Identity() as int)
	
	select @id as [id]
	
	END
END