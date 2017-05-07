alter table Users
Add Id_Library int null foreign key REFERENCES Libraries(Id) 

	update Users 
	set Id_Library = 1
