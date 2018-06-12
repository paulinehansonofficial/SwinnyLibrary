﻿CREATE TABLE [dbo].[Book]
(
	[ISBN] NVARCHAR(20) NOT NULL,
	[Title] NVARCHAR(50) NOT NULL,
	[YearPublished] NVARCHAR(4) NOT NULL,
	[AuthorID] INT NOT NULL,
	[StudentID] NVARCHAR(9),
	CONSTRAINT PK_BOOK PRIMARY KEY (ISBN),
	CONSTRAINT FK_BOOK_AUTHOR FOREIGN KEY (AuthorID) REFERENCES Author(AuthorID),
	CONSTRAINT FK_BOOK_STUDENT FOREIGN KEY (StudentID) REFERENCES Student(StudentID)
)