CREATE TABLE [dbo].[UserEndpoints] (
    [UserId]         INT            NOT NULL,
    [EndpointTypeId] INT            NOT NULL,
    [IsEnabled]      BIT            NOT NULL,
    [Value]          NVARCHAR (255) NULL,
    [Priority]       INT            NULL
);

