use [bibliotecaTasnad]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LibrariesAdd]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LibrariesAdd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adrian,Nagy>
-- Create date: <19.01.2017>
-- =============================================
CREATE PROCEDURE [dbo].[LibrariesAdd]
	@Name nvarchar(MAX),
	@Description nvarchar(MAX) = null,
	@Contact nvarchar(MAX) = null,
	@Domain nvarchar(MAX) = null
AS
BEGIN
	SET NOCOUNT ON;

	declare @id int
	
	if(@id is null)
	begin
		Insert into [dbo].[Libraries](
		[Name]
      ,[Description]
      ,[Contact]
      ,[Domain])
	values(
		@Name,
		@Description,
		@Contact,
		@Domain)

		select @id = cast(Scope_Identity() as int)
	end
	
	select @id as [id]
END