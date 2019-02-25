/*
Created: 02/05/2017
Modified: 19/01/2018
Model: StartEnum
Database: MS SQL Server 2012
*/


-- Create tables section -------------------------------------------------

-- Table dbo.List

CREATE TABLE [dbo].[List]
(
 [ListID] Uniqueidentifier NOT NULL,
 [CreateEventTimeUTC] Datetime NOT NULL,
 [Name] Nvarchar(500) NOT NULL,
 [LikeCount] Bigint NOT NULL,
 [RepostCount] Bigint NOT NULL,
 [IsDeleted] Bit NOT NULL,
 [PublicID] Bigint IDENTITY NOT NULL,
 [PortalUserID] Uniqueidentifier NULL,
 [OriginalListID] Uniqueidentifier NULL,
 [OriginalPortalUserID] Uniqueidentifier NULL,
 [OriginUserName] Nvarchar(256) NULL,
 [OriginFirstName] Nvarchar(255) NULL,
 [OriginLastName] Nvarchar(255) NULL,
 [OriginMiddleName] Nvarchar(255) NULL,
 [ListKindID] Int NOT NULL,
 [ListCheckItemKindID] Int NOT NULL,
 [IsPublished] Bit NOT NULL
)
go

-- Create indexes for table dbo.List

CREATE INDEX [PortalUser_Relationship] ON [dbo].[List] ([PortalUserID])
go

CREATE INDEX [ListKind_Relationship] ON [dbo].[List] ([ListKindID])
go

CREATE INDEX [ListCheckItemKind_Relationship] ON [dbo].[List] ([ListCheckItemKindID])
go

-- Add keys for table dbo.List

ALTER TABLE [dbo].[List] ADD CONSTRAINT [Key2] PRIMARY KEY ([ListID])
go

-- Table dbo.ListItem

CREATE TABLE [dbo].[ListItem]
(
 [ListItemID] Uniqueidentifier NOT NULL,
 [Content] Nvarchar(max) NOT NULL,
 [OrderPosition] Int NOT NULL,
 [CreateEventTimeUTC] Datetime NOT NULL,
 [EditEventTimeUTC] Datetime NOT NULL,
 [LikeCount] Bigint NOT NULL,
 [ListID] Uniqueidentifier NOT NULL,
 [IsChecked] Bit NOT NULL,
 [PortalUserID] Uniqueidentifier NOT NULL
)
go

-- Create indexes for table dbo.ListItem

CREATE INDEX [List_Relationship] ON [dbo].[ListItem] ([ListID])
go

-- Add keys for table dbo.ListItem

ALTER TABLE [dbo].[ListItem] ADD CONSTRAINT [Key3] PRIMARY KEY ([ListItemID])
go

-- Table dbo.ListUserLikeMap

CREATE TABLE [dbo].[ListUserLikeMap]
(
 [ListUserLikeMapID] Uniqueidentifier NOT NULL,
 [CreateEventTimeUTC] Datetime NOT NULL,
 [ListID] Uniqueidentifier NOT NULL,
 [PortalUserID] Uniqueidentifier NULL
)
go

-- Create indexes for table dbo.ListUserLikeMap

CREATE INDEX [List_Relationship] ON [dbo].[ListUserLikeMap] ([ListID])
go

CREATE INDEX [PortalUser_Relationship] ON [dbo].[ListUserLikeMap] ([PortalUserID])
go

-- Add keys for table dbo.ListUserLikeMap

ALTER TABLE [dbo].[ListUserLikeMap] ADD CONSTRAINT [Key4] PRIMARY KEY ([ListUserLikeMapID])
go

-- Table dbo.ListItemUserLikeMap

CREATE TABLE [dbo].[ListItemUserLikeMap]
(
 [ListItemUserLikeMapID] Uniqueidentifier NOT NULL,
 [CreateEventTimeUTC] Datetime NOT NULL,
 [ListItemID] Uniqueidentifier NOT NULL,
 [PortalUserID] Uniqueidentifier NULL
)
go

-- Create indexes for table dbo.ListItemUserLikeMap

CREATE INDEX [ListItem_Relationship] ON [dbo].[ListItemUserLikeMap] ([ListItemID])
go

CREATE INDEX [PortalUser_Relationship] ON [dbo].[ListItemUserLikeMap] ([PortalUserID])
go

-- Add keys for table dbo.ListItemUserLikeMap

ALTER TABLE [dbo].[ListItemUserLikeMap] ADD CONSTRAINT [Key5] PRIMARY KEY ([ListItemUserLikeMapID])
go

-- Table dbo.PortalUser

CREATE TABLE [dbo].[PortalUser]
(
 [PortalUserID] Uniqueidentifier NOT NULL,
 [Email] Nvarchar(256) NOT NULL,
 [EmailConfirmed] Bit NOT NULL,
 [PasswordHash] Nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 [SecurityStamp] Nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 [PhoneNumber] Nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 [PhoneNumberConfirmed] Bit NOT NULL,
 [TwoFactorEnabled] Bit NOT NULL,
 [LockoutEndDateUtc] Datetime NULL,
 [LockoutEnabled] Bit NOT NULL,
 [AccessFailedCount] Int NOT NULL,
 [UserName] Nvarchar(256) NOT NULL,
 [FirstName] Nvarchar(255) NOT NULL,
 [LastName] Nvarchar(255) NOT NULL,
 [MiddleName] Nvarchar(255) NULL,
 [IsAdmin] Bit NOT NULL
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
go

-- Create indexes for table dbo.PortalUser

CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[PortalUser] ([UserName])
 ON [PRIMARY]
go

CREATE INDEX [EmailIndex] ON [dbo].[PortalUser] ([Email])
go

-- Add keys for table dbo.PortalUser

ALTER TABLE [dbo].[PortalUser] ADD CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([PortalUserID])
 ON [PRIMARY]
go

-- Table dbo.UserRoleMap

CREATE TABLE [dbo].[UserRoleMap]
(
 [PortalUserId] Uniqueidentifier NOT NULL,
 [RoleId] Uniqueidentifier NOT NULL
)
ON [PRIMARY]
go

-- Create indexes for table dbo.UserRoleMap

CREATE INDEX [IX_UserId] ON [dbo].[UserRoleMap] ([PortalUserId])
 ON [PRIMARY]
go

CREATE INDEX [IX_RoleId] ON [dbo].[UserRoleMap] ([RoleId])
 ON [PRIMARY]
go

-- Add keys for table dbo.UserRoleMap

ALTER TABLE [dbo].[UserRoleMap] ADD CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([PortalUserId],[RoleId])
 ON [PRIMARY]
go

-- Table dbo.UserLogin

CREATE TABLE [dbo].[UserLogin]
(
 [LoginProvider] Nvarchar(128) NOT NULL,
 [ProviderKey] Nvarchar(128) NOT NULL,
 [PortalUserId] Uniqueidentifier NOT NULL
)
ON [PRIMARY]
go

-- Create indexes for table dbo.UserLogin

CREATE INDEX [IX_UserId] ON [dbo].[UserLogin] ([PortalUserId])
 ON [PRIMARY]
go

-- Add keys for table dbo.UserLogin

ALTER TABLE [dbo].[UserLogin] ADD CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider],[ProviderKey],[PortalUserId])
 ON [PRIMARY]
go

-- Table dbo.UserClaim

CREATE TABLE [dbo].[UserClaim]
(
 [UserClaimId] Int IDENTITY(1,1) NOT NULL,
 [UserId] Uniqueidentifier NOT NULL,
 [ClaimType] Nvarchar(max) NULL,
 [ClaimValue] Nvarchar(max) NULL
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
go

-- Create indexes for table dbo.UserClaim

CREATE INDEX [IX_UserId] ON [dbo].[UserClaim] ([UserId])
 ON [PRIMARY]
go

-- Add keys for table dbo.UserClaim

ALTER TABLE [dbo].[UserClaim] ADD CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([UserClaimId])
 ON [PRIMARY]
go

-- Table dbo.UserRole

CREATE TABLE [dbo].[UserRole]
(
 [UserRoleId] Uniqueidentifier NOT NULL,
 [Name] Nvarchar(256) NOT NULL
)
ON [PRIMARY]
go

-- Create indexes for table dbo.UserRole

CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[UserRole] ([Name])
 ON [PRIMARY]
go

-- Add keys for table dbo.UserRole

ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([UserRoleId])
 ON [PRIMARY]
go

-- Table PortalUserProfile

CREATE TABLE [PortalUserProfile]
(
 [PortalUserProfileID] Uniqueidentifier NOT NULL,
 [StatusText] Nvarchar(500) NULL,
 [AvatarID] Uniqueidentifier NULL,
 [ListCount] Bigint NOT NULL,
 [FollowerCount] Bigint NOT NULL,
 [FollowingCount] Bigint NOT NULL,
 [PortalUserID] Uniqueidentifier NOT NULL
)
go

-- Create indexes for table PortalUserProfile

CREATE UNIQUE INDEX [IX_PortalUser_PortalUserProfile_1] ON [PortalUserProfile] ([PortalUserID])
go

-- Add keys for table PortalUserProfile

ALTER TABLE [PortalUserProfile] ADD CONSTRAINT [PortalUserProFileKey] PRIMARY KEY ([PortalUserProfileID])
go

-- Table UserFollowingMap

CREATE TABLE [UserFollowingMap]
(
 [UserFollowingMapID] Uniqueidentifier NOT NULL,
 [CreateDate] Datetime NOT NULL,
 [MasterID] Uniqueidentifier NOT NULL,
 [ChildID] Uniqueidentifier NULL
)
go

-- Create indexes for table UserFollowingMap

CREATE INDEX [MasterUser_Relationship] ON [UserFollowingMap] ([MasterID])
go

CREATE INDEX [ChildUser_Relationship] ON [UserFollowingMap] ([ChildID])
go

-- Add keys for table UserFollowingMap

ALTER TABLE [UserFollowingMap] ADD CONSTRAINT [Key6] PRIMARY KEY ([UserFollowingMapID])
go

-- Table ListKind

CREATE TABLE [ListKind]
(
 [ListKindID] Int NOT NULL,
 [Title] Nvarchar(255) NOT NULL
)
go

-- Add keys for table ListKind

ALTER TABLE [ListKind] ADD CONSTRAINT [Key7] PRIMARY KEY ([ListKindID])
go

-- Table ListCheckItemKind

CREATE TABLE [ListCheckItemKind]
(
 [ListCheckItemKindID] Int NOT NULL,
 [Title] Nvarchar(255) NOT NULL
)
go

-- Add keys for table ListCheckItemKind

ALTER TABLE [ListCheckItemKind] ADD CONSTRAINT [Key8] PRIMARY KEY ([ListCheckItemKindID])
go

-- Table StoredFile

CREATE TABLE [StoredFile]
(
 [StoredFileID] Uniqueidentifier NOT NULL,
 [StoreAddress] Nvarchar(max) NOT NULL,
 [FileName] Nvarchar(max) NOT NULL,
 [Extention] Nvarchar(255) NOT NULL,
 [FileSize] Bigint NOT NULL,
 [CreateDate] Datetime NOT NULL,
 [UserID] Uniqueidentifier NULL
)
go

-- Add keys for table StoredFile

ALTER TABLE [StoredFile] ADD CONSTRAINT [StoredFileKey] PRIMARY KEY ([StoredFileID])
go

-- Table RecoveryLogin

CREATE TABLE [RecoveryLogin]
(
 [RecoveryLoginID] Uniqueidentifier NOT NULL,
 [LoginName] Nvarchar(50) NOT NULL,
 [UTCEventDateTime] Datetime NOT NULL,
 [UTCEventDate] Datetime NOT NULL,
 [RecoveryEmail] Nvarchar(250) NOT NULL,
 [RecoveryClientIdentifier] Nvarchar(50) NOT NULL,
 [IsRecovered] Bit NOT NULL,
 [RecoveredClientIdentifier] Nvarchar(50) NULL,
 [SentNumber] Nvarchar(255) NOT NULL,
 [UTCRecoveredDateTime] Datetime NULL,
 [TryCount] Int NOT NULL,
 [LastUTCTryRecoverDateTime] Datetime NULL
)
go

-- Create indexes for table RecoveryLogin

CREATE INDEX [RecoveryLogin_Index] ON [RecoveryLogin] ([LoginName])
go

-- Add keys for table RecoveryLogin

ALTER TABLE [RecoveryLogin] ADD CONSTRAINT [Key9] PRIMARY KEY ([RecoveryLoginID])
go

-- Create relationships section ------------------------------------------------- 

ALTER TABLE [dbo].[ListItem] ADD CONSTRAINT [List_ListItem_1] FOREIGN KEY ([ListID]) REFERENCES [dbo].[List] ([ListID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [dbo].[ListUserLikeMap] ADD CONSTRAINT [List_ListUserLikeMap_1] FOREIGN KEY ([ListID]) REFERENCES [dbo].[List] ([ListID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [dbo].[ListItemUserLikeMap] ADD CONSTRAINT [ListItem_ListItemUserLikeMap_1] FOREIGN KEY ([ListItemID]) REFERENCES [dbo].[ListItem] ([ListItemID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [dbo].[UserRoleMap] ADD CONSTRAINT [PortalUser_UserRoleMap_1] FOREIGN KEY ([PortalUserId]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE CASCADE
go

ALTER TABLE [dbo].[UserRoleMap] ADD CONSTRAINT [UserRole_UserRoleMap_1] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRole] ([UserRoleId]) ON UPDATE NO ACTION ON DELETE CASCADE
go

ALTER TABLE [dbo].[UserLogin] ADD CONSTRAINT [PortalUser_UserLogin_1] FOREIGN KEY ([PortalUserId]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE CASCADE
go

ALTER TABLE [dbo].[UserClaim] ADD CONSTRAINT [PortalUser_UserClaim_1] FOREIGN KEY ([UserId]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE CASCADE
go

ALTER TABLE [dbo].[ListUserLikeMap] ADD CONSTRAINT [PortalUser_ListUserLikeMap_1] FOREIGN KEY ([PortalUserID]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [dbo].[ListItemUserLikeMap] ADD CONSTRAINT [PortalUser_ListItemUserLikeMap_1] FOREIGN KEY ([PortalUserID]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [dbo].[List] ADD CONSTRAINT [PortalUser_List_1] FOREIGN KEY ([PortalUserID]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [PortalUserProfile] ADD CONSTRAINT [PortalUser_PortalUserProfile_1] FOREIGN KEY ([PortalUserID]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [UserFollowingMap] ADD CONSTRAINT [PortalUser_UserFollowingMap_1] FOREIGN KEY ([MasterID]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [UserFollowingMap] ADD CONSTRAINT [PortalUser_UserFollowingMap_2] FOREIGN KEY ([ChildID]) REFERENCES [dbo].[PortalUser] ([PortalUserID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [dbo].[List] ADD CONSTRAINT [ListKind_List_1] FOREIGN KEY ([ListKindID]) REFERENCES [ListKind] ([ListKindID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go

ALTER TABLE [dbo].[List] ADD CONSTRAINT [ListCheckItemKind_List_1] FOREIGN KEY ([ListCheckItemKindID]) REFERENCES [ListCheckItemKind] ([ListCheckItemKindID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



