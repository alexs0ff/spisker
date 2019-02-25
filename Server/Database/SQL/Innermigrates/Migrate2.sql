GO
BEGIN TRAN
GO
--step 2: dbo.PortalUserProfile: drop primary key PortalUserProFileKey------------------------------
GO
ALTER TABLE [dbo].[PortalUserProfile] DROP CONSTRAINT [PortalUserProFileKey]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 2 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 2 is finished with errors' SET NOEXEC ON END
GO
--step 3: dbo.PortalUserProfile: add primary key PortalUserProFileKey-------------------------------
GO
ALTER TABLE [dbo].[PortalUserProfile] ADD CONSTRAINT [PortalUserProFileKey] PRIMARY KEY CLUSTERED ([PortalUserProfileID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 3 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 3 is finished with errors' SET NOEXEC ON END
GO
--step 4: add index IX_PortalUser_PortalUserProfile_1 to table dbo.PortalUserProfile----------------
GO
CREATE NONCLUSTERED INDEX [IX_PortalUser_PortalUserProfile_1] ON [dbo].[PortalUserProfile]([PortalUserID]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 4 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 4 is finished with errors' SET NOEXEC ON END
GO
--step 5: add index PortalUserIdUniqueIndex to table dbo.PortalUserProfile--------------------------
GO
CREATE UNIQUE NONCLUSTERED INDEX [PortalUserIdUniqueIndex] ON [dbo].[PortalUserProfile]([PortalUserID]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 5 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 5 is finished with errors' SET NOEXEC ON END
GO
----------------------------------------------------------------------
IF @@TRANCOUNT > 0 BEGIN COMMIT TRAN PRINT 'Synchronization is successfully finished.' END
GO
