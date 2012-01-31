CREATE TABLE [dbo].[OAuthTokens] (
    [UserId]         INT            NOT NULL,
    [ProviderId]     INT            NOT NULL,
    [ConsumerKey]    NVARCHAR (512) NOT NULL,
    [ConsumerSecret] NVARCHAR (512) NOT NULL,
    [CreatedAt]      DATETIME       NOT NULL
);

