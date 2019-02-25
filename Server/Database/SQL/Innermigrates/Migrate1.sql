BEGIN TRAN
GO
--step 2: create table dbo.UserFollowingMap---------------------------------------------------------
GO
CREATE TABLE [dbo].[UserFollowingMap] (
	[UserFollowingMapID]	[uniqueidentifier]	 NOT NULL,
	[CreateDate]			[datetime]			 NOT NULL,
	[MasterID]				[uniqueidentifier]	 NOT NULL,
	[ChildID]				[uniqueidentifier]	 NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 2 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 2 is finished with errors' SET NOEXEC ON END
GO
--step 3: add index ChildUser_Relationship to table dbo.UserFollowingMap----------------------------
GO
CREATE NONCLUSTERED INDEX [ChildUser_Relationship] ON [dbo].[UserFollowingMap]([ChildID]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 3 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 3 is finished with errors' SET NOEXEC ON END
GO
--step 4: dbo.UserFollowingMap: add primary key Key6------------------------------------------------
GO
ALTER TABLE [dbo].[UserFollowingMap] ADD CONSTRAINT [Key6] PRIMARY KEY CLUSTERED ([UserFollowingMapID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 4 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 4 is finished with errors' SET NOEXEC ON END
GO
--step 5: add index MasterUser_Relationship to table dbo.UserFollowingMap---------------------------
GO
CREATE NONCLUSTERED INDEX [MasterUser_Relationship] ON [dbo].[UserFollowingMap]([MasterID]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 5 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 5 is finished with errors' SET NOEXEC ON END
GO
--step 6: dbo.UserFollowingMap: add foreign key PortalUser_UserFollowingMap_1-----------------------
GO
ALTER TABLE [dbo].[UserFollowingMap] ADD CONSTRAINT [PortalUser_UserFollowingMap_1] FOREIGN KEY ([MasterID]) REFERENCES [dbo].[PortalUser] ([PortalUserID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 6 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 6 is finished with errors' SET NOEXEC ON END
GO
--step 7: dbo.UserFollowingMap: add foreign key PortalUser_UserFollowingMap_2-----------------------
GO
ALTER TABLE [dbo].[UserFollowingMap] ADD CONSTRAINT [PortalUser_UserFollowingMap_2] FOREIGN KEY ([ChildID]) REFERENCES [dbo].[PortalUser] ([PortalUserID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 7 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 7 is finished with errors' SET NOEXEC ON END
GO
--step 8: create table dbo.PortalUserProfile--------------------------------------------------------
GO
CREATE TABLE [dbo].[PortalUserProfile] (
	[PortalUserProfileID]		[uniqueidentifier]	 NOT NULL,
	[StatusText]				[nvarchar](500)		 COLLATE Cyrillic_General_CI_AS NULL,
	[AvatarID]					[uniqueidentifier]	 NULL,
	[ListCount]					[bigint]			 NOT NULL,
	[FollowerCount]				[bigint]			 NOT NULL,
	[FollowingCount]			[bigint]			 NOT NULL,
	[PortalUserID]				[uniqueidentifier]	 NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 8 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 8 is finished with errors' SET NOEXEC ON END
GO
--step 9: dbo.PortalUserProfile: add primary key PortalUserProFileKey-------------------------------
GO
ALTER TABLE [dbo].[PortalUserProfile] ADD CONSTRAINT [PortalUserProFileKey] PRIMARY KEY CLUSTERED ([PortalUserProfileID], [PortalUserID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 9 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 9 is finished with errors' SET NOEXEC ON END
GO
--step 10: dbo.PortalUserProfile: add foreign key PortalUser_PortalUserProfile_1--------------------
GO
ALTER TABLE [dbo].[PortalUserProfile] ADD CONSTRAINT [PortalUser_PortalUserProfile_1] FOREIGN KEY ([PortalUserID]) REFERENCES [dbo].[PortalUser] ([PortalUserID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 10 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 10 is finished with errors' SET NOEXEC ON END
GO
--step 11: drop index ListID_PuplicIDIndex from table dbo.List--------------------------------------
GO
DROP INDEX [dbo].[List].[ListID_PuplicIDIndex]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 11 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 11 is finished with errors' SET NOEXEC ON END
GO
----------------------------------------------------------------------
IF @@TRANCOUNT > 0 BEGIN COMMIT TRAN PRINT 'Synchronization is successfully finished.' END
GO
