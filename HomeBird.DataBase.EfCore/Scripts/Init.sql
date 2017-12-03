USE master;
CREATE DATABASE HomeBird;
GO

USE master;
CREATE LOGIN HomeBirdDbMigrator
WITH PASSWORD = 'erwWEw2#';
GO

USE HomeBird;
CREATE USER HomeBirdDbMigrator
FROM LOGIN HomeBirdDbMigrator;
GO

ALTER ROLE db_datareader ADD MEMBER HomeBirdDbMigrator;
GO
ALTER ROLE db_datawriter ADD MEMBER HomeBirdDbMigrator;
GO
ALTER ROLE db_ddladmin ADD MEMBER HomeBirdDbMigrator;
GO

-- adding ReaderWriter
USE master;
CREATE LOGIN HomeBirdDbReaderWriter
WITH PASSWORD = 'dfgKJ2*s';
GO

USE HomeBird;
CREATE USER HomeBirdDbReaderWriter
FROM LOGIN HomeBirdDbReaderWriter;
GO

CREATE ROLE db_executor
GRANT EXECUTE TO db_executor

ALTER ROLE db_datareader ADD MEMBER HomeBirdDbReaderWriter;
GO
ALTER ROLE db_datawriter ADD MEMBER HomeBirdDbReaderWriter;
GO
ALTER ROLE db_executor ADD MEMBER HomeBirdDbReaderWriter;
GO
