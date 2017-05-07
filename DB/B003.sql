use bibliotecaTasnad

alter table Books
Add 
	[Description] nvarchar(MAX) null,
	[ImageUrl] nvarchar(MAX) null,
	[PreviewLink] nvarchar(MAX) null