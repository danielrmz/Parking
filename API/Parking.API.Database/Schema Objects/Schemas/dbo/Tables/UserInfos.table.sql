CREATE TABLE [dbo].[UserInfos] (
    [UserId]               INT            NOT NULL,
    [FirstName]            NVARCHAR (255) NOT NULL,
    [LastName]             NVARCHAR (255) NULL,
    [Gender]               BIT            NOT NULL,
    [PhoneHome]            NVARCHAR (255) NULL,
    [PhoneOffce]           NVARCHAR (255) NULL,
    [PhoneOfficeExtension] NVARCHAR (255) NULL,
    [PhoneCel]             NVARCHAR (255) NULL,
    [ProfilePictureUrl]    NVARCHAR (255) NULL
);

