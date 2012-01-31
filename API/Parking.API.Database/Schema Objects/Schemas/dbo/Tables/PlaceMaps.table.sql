CREATE TABLE [dbo].[PlaceMaps] (
    [PlaceMapId]      INT            NOT NULL,
    [PlaceId]         INT            NOT NULL,
    [PlaceMapUrl]     NVARCHAR (512) NOT NULL,
    [PlaceMapVersion] INT            NOT NULL,
    [CreatedAt]       DATETIME       NOT NULL
);

