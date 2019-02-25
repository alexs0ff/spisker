GO
SET NOCOUNT ON
SET NOEXEC OFF
SET ARITHABORT ON
SET XACT_ABORT ON
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRAN
GO
--step 2: add columns to Table dbo.List-------------------------------------------------------------
GO
ALTER TABLE [dbo].[List] ADD 
	[IsPublished]	[bit] NOT NULL CONSTRAINT [tmp_Def_26404] DEFAULT (1)
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 2 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 2 is finished with errors' SET NOEXEC ON END
GO
--step 3: dbo.List: drop default tmp_Def_26404------------------------------------------------------
GO
ALTER TABLE [dbo].[List] DROP CONSTRAINT [tmp_Def_26404]
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 3 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 3 is finished with errors' SET NOEXEC ON END
GO
----------------------------------------------------------------------
IF @@TRANCOUNT > 0 BEGIN COMMIT TRAN PRINT 'Synchronization is successfully finished.' END
GO