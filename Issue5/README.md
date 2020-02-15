This repository folder is explaining how to replicate the issue at the SqlClient DbType (for Time/Date).
Issue Link: https://github.com/dotnet/SqlClient/issues/5 

1. Open SSMS.
2. Execute the script below.

	CREATE DATABASE [Test];
	
	GO
	
	USE [Test];
	
	GO
	
	CREATE TABLE [TimeTable]
	(
		[Id] INT IDENTITY(1, 1) NOT NULL
		, [Time] TIME
	)
	ON [PRIMARY]
	
	GO

3. To test the issue, follow the steps for 4 and 5.
4. For NetFramework:
	- Navigate to folder 'NetFramework'.
	- Open the SqlClientDbTypeTimeNetFrameworkIssue.sln
	- Click F5.
4. For NetCore:
	- Navigate to folder 'NetCore'.
	- Open the SqlClientDbTypeTimeIssueNetCore.sln
	- Click F5.
