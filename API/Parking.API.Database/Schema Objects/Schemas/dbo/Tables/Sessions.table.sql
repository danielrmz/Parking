CREATE TABLE [dbo].[Sessions] (
    [SessionId]  UNIQUEIDENTIFIER NOT NULL,
    [UserId]     INT              NULL,
    [CreatedAt]  DATETIME         NULL,
    [ExpiresAt]  DATETIME         NULL,
    [Data]       NVARCHAR (MAX)   NULL,
    [LastAccess] DATETIME         NULL
);

