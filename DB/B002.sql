use bibliotecaTasnad

create table Functionalities(
	Id	int not null primary key,
	Name nvarchar(200) not null
)

Insert into Functionalities values( 1, 'ManageBooks')
Insert into Functionalities values( 2, 'ManageUSers')
Insert into Functionalities values( 3, 'Raports')
Insert into Functionalities values( 4, 'AdminLogin')

create table UserRights(
	Id	int not null primary key identity(1,1),
	Id_user int not null  foreign key REFERENCES Users(Id),
	Id_functionality int not null  foreign key REFERENCES Functionalities(Id)
)

Insert into UserRights values( 1, 1)
Insert into UserRights values( 1, 2)
Insert into UserRights values( 1, 3)
Insert into UserRights values( 1, 4)
