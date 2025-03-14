IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF SCHEMA_ID(N'auth') IS NULL EXEC(N'CREATE SCHEMA [auth];');

IF SCHEMA_ID(N'lang') IS NULL EXEC(N'CREATE SCHEMA [lang];');

CREATE TABLE [auth].[Claim] (
    [Id] uniqueidentifier NOT NULL,
    [Type] nvarchar(100) NOT NULL,
    [Value] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_Claim] PRIMARY KEY ([Id])
);

CREATE TABLE [lang].[Language] (
    [Id] uniqueidentifier NOT NULL,
    [Code] nvarchar(4) NOT NULL,
    [Value] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY ([Id])
);

CREATE TABLE [lang].[LanguageKey] (
    [Id] uniqueidentifier NOT NULL,
    [Key] nvarchar(250) NOT NULL,
    [DefaultValue] nvarchar(1000) NOT NULL,
    CONSTRAINT [PK_LanguageKey] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Log] (
    [Id] uniqueidentifier NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [Level] nvarchar(50) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [Exception] nvarchar(max) NULL,
    [StackTrace] nvarchar(max) NULL,
    [RequestPath] nvarchar(500) NULL,
    [StatusCode] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Media] (
    [Id] uniqueidentifier NOT NULL,
    [Type] int NOT NULL,
    [Url] nvarchar(1000) NOT NULL,
    CONSTRAINT [PK_Media] PRIMARY KEY ([Id])
);

CREATE TABLE [auth].[Role] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);

CREATE TABLE [lang].[LanguageTranslation] (
    [Id] uniqueidentifier NOT NULL,
    [LanguageKeyId] uniqueidentifier NOT NULL,
    [Code] nvarchar(4) NOT NULL,
    [Value] nvarchar(1000) NOT NULL,
    CONSTRAINT [PK_LanguageTranslation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LanguageTranslation_LanguageKey_LanguageKeyId] FOREIGN KEY ([LanguageKeyId]) REFERENCES [lang].[LanguageKey] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [auth].[User] (
    [Id] uniqueidentifier NOT NULL,
    [Username] nvarchar(100) NOT NULL,
    [NormalizedUsername] nvarchar(100) NOT NULL,
    [Fullname] nvarchar(100) NOT NULL,
    [NormalizedFullname] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [NormalizedEmail] nvarchar(100) NOT NULL,
    [EmailConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [PhoneNumber] nvarchar(15) NOT NULL,
    [PhoneNumberConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
    [PasswordHash] nvarchar(max) NOT NULL,
    [SecurityStamp] nvarchar(max) NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NOT NULL,
    [TwoFactorEnabled] bit NOT NULL DEFAULT CAST(0 AS bit),
    [AccessFailedCount] int NULL,
    [Status] int NOT NULL,
    [AvatarId] uniqueidentifier NULL,
    [DateOfBirth] datetime2 NULL,
    [LockoutEndDate_Utc] datetime2 NULL,
    [CreatedDate_Utc] datetime2 NOT NULL,
    [ModifiedDate_Utc] datetime2 NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_Media_AvatarId] FOREIGN KEY ([AvatarId]) REFERENCES [dbo].[Media] ([Id]) ON DELETE SET NULL
);

CREATE TABLE [auth].[RoleClaim] (
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_RoleClaim] PRIMARY KEY ([RoleId], [ClaimId]),
    CONSTRAINT [FK_RoleClaim_Claim_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [auth].[Claim] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [auth].[Role] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [auth].[UserClaim] (
    [UserId] uniqueidentifier NOT NULL,
    [ClaimId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY ([UserId], [ClaimId]),
    CONSTRAINT [FK_UserClaim_Claim_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [auth].[Claim] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [auth].[UserLogin] (
    [Provider] nvarchar(100) NOT NULL,
    [ProviderKey] nvarchar(100) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY ([Provider], [ProviderKey]),
    CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [auth].[UserRole] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [auth].[Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [auth].[UserToken] (
    [UserId] uniqueidentifier NOT NULL,
    [Provider] nvarchar(100) NOT NULL,
    [TokenName] nvarchar(100) NOT NULL,
    [TokenValue] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserToken] PRIMARY KEY ([UserId], [Provider], [TokenName]),
    CONSTRAINT [FK_UserToken_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_LanguageKey_Key] ON [lang].[LanguageKey] ([Key]);

CREATE INDEX [IX_LanguageTranslation_LanguageKeyId] ON [lang].[LanguageTranslation] ([LanguageKeyId]);

CREATE INDEX [IX_RoleClaim_ClaimId] ON [auth].[RoleClaim] ([ClaimId]);

CREATE INDEX [IX_User_AvatarId] ON [auth].[User] ([AvatarId]);

CREATE INDEX [IX_UserClaim_ClaimId] ON [auth].[UserClaim] ([ClaimId]);

CREATE INDEX [IX_UserLogin_UserId] ON [auth].[UserLogin] ([UserId]);

CREATE INDEX [IX_UserRole_RoleId] ON [auth].[UserRole] ([RoleId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250314041158_Initial', N'9.0.0');

COMMIT;
GO

