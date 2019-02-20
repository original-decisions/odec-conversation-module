begin tran
IF schema_id('conv') IS NULL
    EXECUTE('CREATE SCHEMA [conv]')
IF schema_id('AspNet') IS NULL
    EXECUTE('CREATE SCHEMA [AspNet]')
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[conv].[ConversationMessages]') AND type in (N'U'))
begin
CREATE TABLE [conv].[ConversationMessages] (
    [ConversationId] [int] NOT NULL,
    [MessageId] [int] NOT NULL,
    CONSTRAINT [PK_conv.ConversationMessages] PRIMARY KEY ([ConversationId], [MessageId])
)
CREATE INDEX [IX_ConversationId] ON [conv].[ConversationMessages]([ConversationId])
CREATE INDEX [IX_MessageId] ON [conv].[ConversationMessages]([MessageId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[conv].[Conversations]') AND type in (N'U'))
begin
CREATE TABLE [conv].[Conversations] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [ConversationTypeId] [int] NOT NULL,
    [UserStartedId] [int] NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_conv.Conversations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConversationTypeId] ON [conv].[Conversations]([ConversationTypeId])
CREATE INDEX [IX_UserStartedId] ON [conv].[Conversations]([UserStartedId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[conv].[ConversationTypes]') AND type in (N'U'))
begin
CREATE TABLE [conv].[ConversationTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_conv.ConversationTypes] PRIMARY KEY ([Id])
)
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[Users]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[Users] (
    [Id] [int] NOT NULL IDENTITY,
    [Rating] [decimal](18, 2) NOT NULL,
    [ProfilePicturePath] [nvarchar](max),
    [FirstName] [nvarchar](max),
    [LastName] [nvarchar](max),
    [Patronymic] [nvarchar](max),
    [DateUpdated] [datetime],
    [LastActivityDate] [datetime],
    [LastLogin] [datetime],
    [RemindInDays] [int] NOT NULL,
    [DateRegistration] [datetime] NOT NULL,
    [Description] [nvarchar](max),
    [Email] [nvarchar](256),
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](max),
    [SecurityStamp] [nvarchar](max),
    [PhoneNumber] [nvarchar](max),
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime],
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_AspNet.Users] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [UserNameIndex] ON [AspNet].[Users]([UserName])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserClaims]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[UserClaims] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [ClaimType] [nvarchar](max),
    [ClaimValue] [nvarchar](max),
    CONSTRAINT [PK_AspNet.UserClaims] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserClaims]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserLogins]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[UserLogins] (
    [LoginProvider] [nvarchar](128) NOT NULL,
    [ProviderKey] [nvarchar](128) NOT NULL,
    [UserId] [int] NOT NULL,
    CONSTRAINT [PK_AspNet.UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserLogins]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserRoles]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[UserRoles] (
    [UserId] [int] NOT NULL,
    [RoleId] [int] NOT NULL,
    CONSTRAINT [PK_AspNet.UserRoles] PRIMARY KEY ([UserId], [RoleId])
)
CREATE INDEX [IX_UserId] ON [AspNet].[UserRoles]([UserId])
CREATE INDEX [IX_RoleId] ON [AspNet].[UserRoles]([RoleId])
end

IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[conv].[Messages]') AND type in (N'U'))
begin
CREATE TABLE [conv].[Messages] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [Body] [nvarchar](max) NOT NULL,
    [Header] [nvarchar](250) NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_conv.Messages] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [conv].[Messages]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[conv].[ConversationUsers]') AND type in (N'U'))
begin
CREATE TABLE [conv].[ConversationUsers] (
    [UserId] [int] NOT NULL,
    [ConversationId] [int] NOT NULL,
    [JoinDate] [datetime] NOT NULL,
    CONSTRAINT [PK_conv.ConversationUsers] PRIMARY KEY ([UserId], [ConversationId])
)
CREATE INDEX [IX_UserId] ON [conv].[ConversationUsers]([UserId])
CREATE INDEX [IX_ConversationId] ON [conv].[ConversationUsers]([ConversationId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[Roles]') AND type in (N'U'))
begin
CREATE TABLE [AspNet].[Roles] (
    [Id] [int] NOT NULL IDENTITY,
    [InRoleId] [int],
    [Scope] [nvarchar](max),
    [Name] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_AspNet.Roles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_InRoleId] ON [AspNet].[Roles]([InRoleId])
CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNet].[Roles]([Name])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_conv.ConversationMessages_conv.Conversations_ConversationId')
		begin
ALTER TABLE [conv].[ConversationMessages] ADD CONSTRAINT [FK_conv.ConversationMessages_conv.Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [conv].[Conversations] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_conv.ConversationMessages_conv.Messages_MessageId')
		begin
ALTER TABLE [conv].[ConversationMessages] ADD CONSTRAINT [FK_conv.ConversationMessages_conv.Messages_MessageId] FOREIGN KEY ([MessageId]) REFERENCES [conv].[Messages] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_conv.Conversations_conv.ConversationTypes_ConversationTypeId')
		begin
ALTER TABLE [conv].[Conversations] ADD CONSTRAINT [FK_conv.Conversations_conv.ConversationTypes_ConversationTypeId] FOREIGN KEY ([ConversationTypeId]) REFERENCES [conv].[ConversationTypes] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_conv.Conversations_AspNet.Users_UserStartedId')
		begin
ALTER TABLE [conv].[Conversations] ADD CONSTRAINT [FK_conv.Conversations_AspNet.Users_UserStartedId] FOREIGN KEY ([UserStartedId]) REFERENCES [AspNet].[Users] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserClaims_AspNet.Users_UserId')
		begin
ALTER TABLE [AspNet].[UserClaims] ADD CONSTRAINT [FK_AspNet.UserClaims_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserLogins_AspNet.Users_UserId')
		begin
ALTER TABLE [AspNet].[UserLogins] ADD CONSTRAINT [FK_AspNet.UserLogins_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Users_UserId')
		begin
ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Roles_RoleId')
		begin
ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNet].[Roles] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_conv.Messages_AspNet.Users_UserId')
		begin
ALTER TABLE [conv].[Messages] ADD CONSTRAINT [FK_conv.Messages_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_conv.ConversationUsers_conv.Conversations_ConversationId')
		begin
ALTER TABLE [conv].[ConversationUsers] ADD CONSTRAINT [FK_conv.ConversationUsers_conv.Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [conv].[Conversations] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_conv.ConversationUsers_AspNet.Users_UserId')
		begin
ALTER TABLE [conv].[ConversationUsers] ADD CONSTRAINT [FK_conv.ConversationUsers_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end 
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.Roles_AspNet.Roles_InRoleId')
		begin
ALTER TABLE [AspNet].[Roles] ADD CONSTRAINT [FK_AspNet.Roles_AspNet.Roles_InRoleId] FOREIGN KEY ([InRoleId]) REFERENCES [AspNet].[Roles] ([Id])
end 

commit tran