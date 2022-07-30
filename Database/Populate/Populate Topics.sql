
/*
DELETE FROM Topics
DBCC CHECKIDENT (Topics, RESEED, 0)
*/

INSERT INTO Topics (Title, Created, Modified) VALUES ('Work', GETDATE(), GETDATE())
INSERT INTO Topics (Title, Created, Modified) VALUES ('School', GETDATE(), GETDATE())
INSERT INTO Topics (Title, Created, Modified) VALUES ('Women', GETDATE(), GETDATE())
INSERT INTO Topics (Title, Created, Modified) VALUES ('Men', GETDATE(), GETDATE())
INSERT INTO Topics (Title, Created, Modified) VALUES ('Kids', GETDATE(), GETDATE())
INSERT INTO Topics (Title, Created, Modified) VALUES ('Adults', GETDATE(), GETDATE()) 

SELECT * FROM Topics
