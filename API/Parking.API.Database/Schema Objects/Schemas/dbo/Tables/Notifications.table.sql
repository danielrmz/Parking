CREATE TABLE [dbo].[Notifications] (
    [NotificationId] INT             IDENTITY (1, 1) NOT NULL,
    [UserId]         INT             NOT NULL,
    [Message]        NVARCHAR (2000) NULL,
    [CreatedAt]      DATETIME        NOT NULL,
    [CreatedBy]      INT             NOT NULL,
    [CheckInId]      INT             NULL,
    [ReadAt]         DATETIME        NULL
);

