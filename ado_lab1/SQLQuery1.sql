USE MyDB;

CREATE TABLE Department (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Auto-increment ID
    Name NVARCHAR(100) NOT NULL       -- Department name
);

INSERT INTO Department (Name)
VALUES ('HR'), ('IT'), ('Finance');


SELECT * FROM Department;

