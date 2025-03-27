////////////// setting for sql not work properly /////////////////////////
USE master;
CREATE LOGIN [IIS APPPOOL\apiwithjwttoken] FROM WINDOWS;

USE Testdb;
CREATE USER [IIS APPPOOL\apiwithjwttoken] FOR LOGIN [IIS APPPOOL\apiwithjwttoken];

ALTER ROLE db_owner ADD MEMBER [IIS APPPOOL\apiwithjwttoken];

////////////// setting for sql not work properly /////////////////////////
