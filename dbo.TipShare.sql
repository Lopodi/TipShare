﻿CREATE TABLE [dbo].[Employee]
(
	EmployeeID INT NOT NULL PRIMARY KEY IDENTITY,
	UserID INT NOT NULL, --FOREIGN KEY REFERENCES User(UserID),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR (50) NOT NULL,
	EmployeeStatus NVARCHAR (50) NOT NULL,
	StatusDate DATETIME NOT NULL,

)
