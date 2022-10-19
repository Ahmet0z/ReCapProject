CREATE TRIGGER AddUserFindeksWhenRegistered
ON Users
AFTER INSERT
AS
BEGIN
declare @userid int
select @userid = Id from inserted
UPDATE Users SET Findeks = 250 WHERE Id = @userid
END