Create Procedure dbo.GetJobDetailsbyId(@id int) as
Begin
	Declare @tmpJobs table
	(JID int identity(1,1),
	Id int,
	Title varchar(100),	
	Description varchar(50),
	PostedDate DateTime,
	ClosingDate DateTime,
	Locationid int,
	Locationtitle varchar(100),
	City varchar(50),
	State varchar(50),
	Country varchar(50),
	Zip varchar(5),
	DepartmentId int,
	DepartmentName varchar(50)
	)

	insert into @tmpJobs
	Select j.Id,j.Title,j.Description,j.PostedDate,j.ClosingDate,j.Id,j.Title,l.City,l.State,l.Country,l.Zip,d.Id as Deptid,d.Name 
	from Jobs j inner join Locations l on j.locationid=l.Id 
	inner join Departments d on d.id=j.Id where j.id = @id

	select Id,'JOB'+'-'+CAST(Id as varchar(50))as Code,Title,Description,PostedDate,ClosingDate,Locationid,Locationtitle,City,State,Country,Zip,DepartmentId,DepartmentName 
	from @tmpJobs		
End
