CREATE TRIGGER AddUserRoleWhenRegistered
ON dbo.Users
AFTER INSERT
AS
BEGIN
INSERT INTO UserOperationClaims VALUES ((SELECT Id FROM inserted),2)
END