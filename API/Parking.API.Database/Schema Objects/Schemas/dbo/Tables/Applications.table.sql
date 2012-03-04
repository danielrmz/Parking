CREATE TABLE [dbo].[Applications] (
    [ApplicationId]   INT           IDENTITY (1, 1) NOT NULL,
    [ApplicationName] NVARCHAR (50) NOT NULL,
    [CreatedAt]       DATETIME      NULL
);

