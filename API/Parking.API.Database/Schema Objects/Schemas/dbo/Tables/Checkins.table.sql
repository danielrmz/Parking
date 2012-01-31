CREATE TABLE [dbo].[Checkins] (
    [CheckInId]      INT      IDENTITY (1, 1) NOT NULL,
    [StartTime]      DATETIME NOT NULL,
    [EndTime]        DATETIME NULL,
    [SpaceId]        INT      NOT NULL,
    [UserId]         INT      NOT NULL,
    [ReservationId]  INT      NULL,
    [RegisteredFrom] INT      NOT NULL,
    [RegisteredBy]   INT      NOT NULL
);

