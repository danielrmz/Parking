CREATE TABLE [dbo].[Users] (
    [UserId]     INT             IDENTITY (1, 1) NOT NULL,
    [Password]   NVARCHAR (1024) NOT NULL,
    [Email]      NVARCHAR (255)  NOT NULL,
    [LastAccess] DATETIME        NULL,
    [IsActive]   BIT             NOT NULL,
    [CreatedAt]  DATETIME        DEFAULT (getdate()) NOT NULL
);

