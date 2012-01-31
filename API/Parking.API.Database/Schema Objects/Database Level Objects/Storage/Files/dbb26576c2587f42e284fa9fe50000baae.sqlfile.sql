ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [dbb26576c2587f42e284fa9fe50000baae], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', MAXSIZE = 20480 KB, FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

