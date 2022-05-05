Create Procedure dbo.GetJobDetails(@searchString varchar(30)=null,@pageNo int,@pageSize int,@locationId int=null,@departmentId int=null) as
Begin
	Declare @tmpJobs table
	(JID int identity(1,1),
	Id int,
	Title varchar(100),
	Location varchar(50),
	Department varchar(50),
	PostedDate DateTime,
	ClosingDate DateTime)

	
	if(isnull(@departmentId,0) != 0 and isnull(@locationId,0) != 0)
	Begin	
		insert into @tmpJobs
		Select j.Id,j.Title,l.Title as location,d.Name as Department,j.PostedDate,j.ClosingDate from Jobs j 
		inner join Locations l on j.locationid=l.Id
		inner join Departments d on j.DepartmentId=d.Id where LocationId=@locationId and DepartmentId=@departmentId	
	end
	else if(isnull(@departmentId,0) != 0)
	begin
		insert into @tmpJobs
		Select j.Id,j.Title,l.Title as location,d.Name as Department,j.PostedDate,j.ClosingDate from Jobs j 
		inner join Locations l on j.locationid=l.Id
		inner join Departments d on j.DepartmentId=d.Id where DepartmentId=@departmentId
	end
	else if(isnull(@locationId,0) != 0)
	Begin
		insert into @tmpJobs
		Select j.Id,j.Title,l.Title as location,d.Name as Department,j.PostedDate,j.ClosingDate from Jobs j 
		inner join Locations l on j.locationid=l.Id
		inner join Departments d on j.DepartmentId=d.Id where LocationId=@locationId	
	End
	else
	Begin		
		insert into @tmpJobs
		Select j.Id,j.Title,l.Title as location,d.Name as Department,j.PostedDate,j.ClosingDate from Jobs j 
		inner join Locations l on j.locationid=l.Id
		inner join Departments d on j.DepartmentId=d.Id
	End
	if(isnull(@searchString,'')!='')
		select Id,'JOB'+'-'+CAST(JID as varchar(50))as Code,Title,Location,Department,PostedDate,ClosingDate from @tmpJobs
		where Title like'%' + @searchString + '%' and JID > (@pageNo-1)*@pageSize and JID<(@pageNo*@pageSize)+1
	else
		select Id,'JOB'+'-'+CAST(JID as varchar(50))as Code,Title,Location,Department,PostedDate,ClosingDate from @tmpJobs
		where JID > (@pageNo-1)*@pageSize and JID<(@pageNo*@pageSize)+1
End

