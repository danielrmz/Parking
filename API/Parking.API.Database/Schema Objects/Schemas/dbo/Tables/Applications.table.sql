CREATE TABLE [dbo].[Applications] (
    [ApplicationId]            INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationName]          NVARCHAR (50)  NOT NULL,
    [ApplicationConsumerKey]   NVARCHAR (512) NULL,
    [ApplicationConsumerToken] NVARCHAR (512) NULL,
    [CreatedAt]                DATETIME       NULL
);

