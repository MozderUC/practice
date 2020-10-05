INSERT INTO 
	[dbo].[Employees] ([LastName], [FirstName], [Title])
VALUES (@LastName, @FirstName, @Title);
-- return id of the last inserted row
SELECT CAST(SCOPE_IDENTITY() as int);