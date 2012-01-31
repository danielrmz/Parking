ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [dbb26576c2587f42e284fa9fe50000baae_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_log.LDF', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

