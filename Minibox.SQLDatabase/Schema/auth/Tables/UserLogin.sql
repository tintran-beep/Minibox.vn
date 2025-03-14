CREATE TABLE [auth].[UserLogin] (
    [Provider]    NVARCHAR (100)   NOT NULL,
    [ProviderKey] NVARCHAR (100)   NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserLogin_UserId]
    ON [auth].[UserLogin]([UserId] ASC);

