CREATE TABLE [dbo].[RecoveryLogin] (
	[RecoveryLoginID]				[uniqueidentifier]	 NOT NULL,
	[LoginName]						[nvarchar](50)		 COLLATE Cyrillic_General_CI_AS NOT NULL,
	[UTCEventDateTime]				[datetime]			 NOT NULL,
	[UTCEventDate]					[datetime]			 NOT NULL,
	[RecoveryEmail]					[nvarchar](250)		 COLLATE Cyrillic_General_CI_AS NOT NULL,
	[RecoveryClientIdentifier]		[nvarchar](50)		 COLLATE Cyrillic_General_CI_AS NOT NULL,
	[IsRecovered]					[bit]				 NOT NULL,
	[RecoveredClientIdentifier]		[nvarchar](50)		 COLLATE Cyrillic_General_CI_AS NULL,
	[SentNumber]					[nvarchar](255)		 COLLATE Cyrillic_General_CI_AS NOT NULL,
	[UTCRecoveredDateTime]			[datetime]			 NULL,
	[TryCount]						[int]				 NOT NULL,
	[LastUTCTryRecoverDateTime]		[datetime]			 NULL,
	CONSTRAINT [Key9] PRIMARY KEY CLUSTERED ([RecoveryLoginID])
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [RecoveryLogin_Index] ON [dbo].[RecoveryLogin]([LoginName]) WITH ( ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON ) ON [PRIMARY]
GO