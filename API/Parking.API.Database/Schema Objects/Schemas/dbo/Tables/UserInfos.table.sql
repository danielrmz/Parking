CREATE TABLE [dbo].[UserInfos] (
    [UserId]               INT            NOT NULL,
    [FirstName]            NVARCHAR (255) NOT NULL,
    [LastName]             NVARCHAR (255) NULL,
    [Gender]               NVARCHAR (1)   NULL,
    [PhoneHome]            NVARCHAR (255) NULL,
    [PhoneOffice]          NVARCHAR (255) NULL,
    [PhoneOfficeExtension] NVARCHAR (255) NULL,
    [PhoneCel]             NVARCHAR (255) NULL,
    [ProfilePictureUrl]    NVARCHAR (255) NULL,
    [ContactEmail]         NVARCHAR (255) NULL
);

