use bibliotecaTasnad

create table ErrorLogs(
	Id	int not null primary key identity(1,1),
	[Message] nvarchar(MAX) not null,
	Created datetime not null,
)

create table Localities(
	Id	int not null primary key identity(1,1),
	Name nvarchar(25) not null
)

create table BookFormats(
	Id	int not null primary key identity(1,1),
	Name nvarchar(15) not null
)

create table BookDomains(
	Id	int not null primary key identity(1,1),
	Name nvarchar(50) not null
)

create table BookSubjects(
	Id	int not null primary key identity(1,1),
	Name nvarchar(50) not null
)

create table Languages(
	Id	int not null primary key identity(1,1),
	Name nvarchar(10) not null
)

create table Nationalities(
	Id	int not null primary key identity(1,1),
	Name nvarchar(10) not null
)

create table Occupations(
	Id	int not null primary key identity(1,1),
	Name nvarchar(25) not null
)

create table BookConditions(
	Id	int not null primary key identity(1,1),
	Name nvarchar(15) not null
)

create table UserTypes(
	Id	int not null primary key identity(1,1),
	Name nvarchar(20) not null
)

create table Libraries(
	Id	int not null primary key identity(1,1),
	Name nvarchar(20) not null
)

create table Publishers(
	Id	int not null primary key identity(1,1),
	Name nvarchar(MAX) not null, 
)

create table Users(
	Id	int not null primary key identity(1,1),
	FirstName nvarchar(50) not null, 
	LastName nvarchar(50) not null, 
	UserName nvarchar(50) null,
	PasswordHashed nvarchar(100) null,
	HomeAddress nvarchar(300) null, 
	Birthdate datetime null,
	Phone nvarchar(15) null, 
	Email nvarchar(50) null, 
	FacebookAddress nvarchar(300) null, 
	JoinDate datetime not null,
	Flags int default 0,
	Gender int default 0,
	Id_Locality int not null foreign key REFERENCES Localities(Id),
	Id_UserType int not null foreign key REFERENCES UserTypes(Id),
	Id_Nationality int not null foreign key REFERENCES Nationalities(Id)
)

create table Books(
	Id	int not null primary key identity(1,1),
	Title nvarchar(MAX) not null, 
	PublishYear int null, 
	Volume nvarchar(50) null, 
	InternalNr nvarchar(MAX) not null,
	NrPages int not null,
	AddedDate datetime not null,
	Id_AddedBy int not null foreign key REFERENCES Users(Id),
	Id_Publisher int not null foreign key REFERENCES Publishers(Id),
	Id_BookCondition int not null foreign key REFERENCES BookConditions(Id),
	Id_Library int not null foreign key REFERENCES Libraries(Id),
	Id_BookFormat int not null foreign key REFERENCES BookFormats(Id),
	Id_BookDomain int not null foreign key REFERENCES BookDomains(Id),
	Id_BookSubject int not null foreign key REFERENCES BookSubjects(Id),
	Id_Language int not null foreign key REFERENCES Languages(Id)
)

create table ISBN(
	Id	int not null primary key identity(1,1),
	Value nvarchar(50) not null,
	Id_Book int not null foreign key REFERENCES Books(Id)
)

create table Authors(
	Id	int not null primary key identity(1,1),
	Name nvarchar(MAX) not null
)

create table BookAuthorTypes(
	Id	int not null primary key identity(1,1),
	Name nvarchar(10) not null
)

create table BookAuthors(
	Id	int not null primary key identity(1,1),
	Id_Book int not null foreign key REFERENCES Books(Id), 
	Id_Author int not null foreign key REFERENCES Authors(Id),
	Id_BookAuthorType int not null foreign key REFERENCES BookAuthorTypes(Id),
)

create table Loans(
	Id	int not null primary key identity(1,1),
	Id_User int not null foreign key REFERENCES Users(Id),
	Id_Book int not null foreign key REFERENCES Books(Id), 
	LoanDate datetime not null,
	ReturnedDate datetime null,
	Id_ReturnedBookCondition int null foreign key REFERENCES BookConditions(Id)	
)

create table Reservations(
	Id	int not null primary key identity(1,1),
	Id_User int not null foreign key REFERENCES Users(Id),
	Id_Book int not null foreign key REFERENCES Books(Id), 
	ReservedDate datetime not null,
	Flags int default 1,	
)