USE [master]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,	
	[Country] [varchar](50) NOT NULL,	
	[Zip] VarChar(5)
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) 
