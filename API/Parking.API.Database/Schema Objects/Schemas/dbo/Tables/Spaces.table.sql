CREATE TABLE [dbo].[Spaces] (
    [SpaceId]      INT            IDENTITY (1, 1) NOT NULL,
    [PlaceId]      INT            NOT NULL,
    [Alias]        NVARCHAR (255) NULL,
    [AccessTypeId] INT            NOT NULL,
    [OwnerId]      INT            NOT NULL,
    [CreatedAt]    DATETIME       NOT NULL,
    [Deleted]      BIT            DEFAULT ((0)) NOT NULL
);

