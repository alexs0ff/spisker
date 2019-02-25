SET NOCOUNT ON
SET NOEXEC OFF
SET ARITHABORT ON
SET XACT_ABORT ON
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRAN
GO
--step 2: create table dbo.ListKind-----------------------------------------------------------------
GO
CREATE TABLE [dbo].[ListKind] (
	[ListKindID]	[int]			 NOT NULL,
	[Title]			[nvarchar](255)	 COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[ListKind] ([ListKindID],[Title])  
SELECT
1,'Простой'
UNION ALL
SELECT
2,'Маркер'
UNION ALL
SELECT
3,'Нумерация'

GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 2 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 2 is finished with errors' SET NOEXEC ON END
GO
--step 3: dbo.ListKind: add primary key Key7--------------------------------------------------------
GO
ALTER TABLE [dbo].[ListKind] ADD CONSTRAINT [Key7] PRIMARY KEY CLUSTERED ([ListKindID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 3 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 3 is finished with errors' SET NOEXEC ON END
GO
--step 4: drop index IX_PortalUser_PortalUserProfile_1 from table dbo.PortalUserProfile-------------
GO
DROP INDEX [dbo].[PortalUserProfile].[IX_PortalUser_PortalUserProfile_1]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 4 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 4 is finished with errors' SET NOEXEC ON END
GO
--step 5: add index IX_PortalUser_PortalUserProfile_1 to table dbo.PortalUserProfile----------------
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PortalUser_PortalUserProfile_1] ON [dbo].[PortalUserProfile]([PortalUserID]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 5 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 5 is finished with errors' SET NOEXEC ON END
GO
--step 6: drop index PortalUserIdUniqueIndex from table dbo.PortalUserProfile-----------------------
GO
DROP INDEX [dbo].[PortalUserProfile].[PortalUserIdUniqueIndex]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 6 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 6 is finished with errors' SET NOEXEC ON END
GO
--step 7: create table dbo.ListCheckItemKind--------------------------------------------------------
GO
CREATE TABLE [dbo].[ListCheckItemKind] (
	[ListCheckItemKindID]		[int]			 NOT NULL,
	[Title]						[nvarchar](255)	 COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[ListCheckItemKind]
           ([ListCheckItemKindID]
           ,[Title])
SELECT

1,'Зачеркивание'
GO


IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 7 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 7 is finished with errors' SET NOEXEC ON END
GO
--step 8: dbo.ListCheckItemKind: add primary key Key8-----------------------------------------------
GO
ALTER TABLE [dbo].[ListCheckItemKind] ADD CONSTRAINT [Key8] PRIMARY KEY CLUSTERED ([ListCheckItemKindID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 8 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 8 is finished with errors' SET NOEXEC ON END
GO
--step 9: add columns to Table dbo.List-------------------------------------------------------------
GO
ALTER TABLE [dbo].[List] ADD 
	[ListKindID]	[int] NOT NULL CONSTRAINT [tmp_Def_77447] DEFAULT (1),
	[ListCheckItemKindID]	[int] NOT NULL CONSTRAINT [tmp_Def_8554] DEFAULT (1)
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 9 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 9 is finished with errors' SET NOEXEC ON END
GO
--step 10: dbo.List: add foreign key ListCheckItemKind_List_1---------------------------------------
GO
ALTER TABLE [dbo].[List] ADD CONSTRAINT [ListCheckItemKind_List_1] FOREIGN KEY ([ListCheckItemKindID]) REFERENCES [dbo].[ListCheckItemKind] ([ListCheckItemKindID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 10 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 10 is finished with errors' SET NOEXEC ON END
GO
--step 11: dbo.List: add foreign key ListKind_List_1------------------------------------------------
GO
ALTER TABLE [dbo].[List] ADD CONSTRAINT [ListKind_List_1] FOREIGN KEY ([ListKindID]) REFERENCES [dbo].[ListKind] ([ListKindID])
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 11 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 11 is finished with errors' SET NOEXEC ON END
GO
--step 12: add index ListCheckItemKind_Relationship to table dbo.List-------------------------------
GO
CREATE NONCLUSTERED INDEX [ListCheckItemKind_Relationship] ON [dbo].[List]([ListCheckItemKindID]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 12 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 12 is finished with errors' SET NOEXEC ON END
GO
--step 13: add index ListKind_Relationship to table dbo.List----------------------------------------
GO
CREATE NONCLUSTERED INDEX [ListKind_Relationship] ON [dbo].[List]([ListKindID]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 13 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 13 is finished with errors' SET NOEXEC ON END
GO
--step 14: dbo.List: drop default tmp_Def_8554------------------------------------------------------
GO
ALTER TABLE [dbo].[List] DROP CONSTRAINT [tmp_Def_8554]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 14 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 14 is finished with errors' SET NOEXEC ON END
GO
--step 15: dbo.List: drop default tmp_Def_77447-----------------------------------------------------
GO
ALTER TABLE [dbo].[List] DROP CONSTRAINT [tmp_Def_77447]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 15 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 15 is finished with errors' SET NOEXEC ON END
GO
----------------------------------------------------------------------
IF @@TRANCOUNT > 0 BEGIN COMMIT TRAN PRINT 'Synchronization is successfully finished.' END
GO
