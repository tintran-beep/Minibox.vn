CREATE TABLE [auth].[User] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [Username]             NVARCHAR (100)   NOT NULL,
    [NormalizedUsername]   NVARCHAR (100)   NOT NULL,
    [Fullname]             NVARCHAR (100)   NOT NULL,
    [NormalizedFullname]   NVARCHAR (100)   NOT NULL,
    [Email]                NVARCHAR (100)   NOT NULL,
    [NormalizedEmail]      NVARCHAR (100)   NOT NULL,
    [EmailConfirmed]       BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [PhoneNumber]          NVARCHAR (15)    NOT NULL,
    [PhoneNumberConfirmed] BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)   NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX)   NOT NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)   NOT NULL,
    [TwoFactorEnabled]     BIT              DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [AccessFailedCount]    INT              NULL,
    [Status]               INT              NOT NULL,
    [AvatarId]             UNIQUEIDENTIFIER NULL,
    [DateOfBirth]          DATETIME2 (7)    NULL,
    [LockoutEndDate_Utc]   DATETIME2 (7)    NULL,
    [CreatedDate_Utc]      DATETIME2 (7)    NOT NULL,
    [ModifiedDate_Utc]     DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Media_AvatarId] FOREIGN KEY ([AvatarId]) REFERENCES [dbo].[Media] ([Id]) ON DELETE SET NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_User_AvatarId]
    ON [auth].[User]([AvatarId] ASC);

