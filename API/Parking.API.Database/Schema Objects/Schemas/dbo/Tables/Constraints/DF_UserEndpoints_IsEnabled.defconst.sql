ALTER TABLE [dbo].[UserEndpoints]
    ADD CONSTRAINT [DF_UserEndpoints_IsEnabled] DEFAULT ((1)) FOR [IsEnabled];

