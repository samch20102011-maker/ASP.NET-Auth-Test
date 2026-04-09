CREATE DATABASE LoginDB
GO

USE LoginDB
GO

CREATE SCHEMA Auth 
GO

CREATE TABLE Auth.Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);
GO

-- Example user
-- INSERT INTO Auth.Users (Username, Password) VALUES ('TestUser', 'Password91');
-- GO

SELECT * FROM Auth.Users;