USE [master]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Jobs](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[LocationId] int,
	[DepartmentId] int, 	
	[ClosingDate] datetime,
	[PostedDate] datetime,
	constraint fk_Location foreign key (LocationId) references dbo.[Locations](Id),
	constraint fk_Department foreign key (DepartmentId) references dbo.[Departments](Id) 
) 

select * from Jobs
/*insert into Jobs(Title,Description,LocationId,DepartmentId,ClosingDate,PostedDate)
values ('Project Manager','Solid e',2,1,'2022-05-20 22:40:34.307',getdate())*/



