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
	[OriginUserName]	[nvarchar](256) COLLATE Cyrillic_General_CI_AS NULL,
	[OriginFirstName]	[nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL,
	[OriginLastName]	[nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL,
	[OriginMiddleName]	[nvarchar](255) COLLATE Cyrillic_General_CI_AS NULL
GO
IF @@ERROR <> 0 AND @@TRANCOUNT > 0 BEGIN PRINT 'step 2 is finished with errors' ROLLBACK TRAN END
GO
IF @@TRANCOUNT = 0 BEGIN PRINT 'step 2 is finished with errors' SET NOEXEC ON END
GO
----------------------------------------------------------------------
IF @@TRANCOUNT > 0 BEGIN COMMIT TRAN PRINT 'Synchronization is successfully finished.' END
GO
