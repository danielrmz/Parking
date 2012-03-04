ALTER TABLE [dbo].[Spaces]
    ADD CONSTRAINT [DF_Spaces_SpaceDirection] DEFAULT (N'left') FOR [SpaceDirection];

