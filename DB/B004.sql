use bibliotecaTasnad
create table ErrorLogs(
	Id	int not null primary key identity(1,1),
	[Message] nvarchar(MAX) not null,
	Created datetime not null,
)