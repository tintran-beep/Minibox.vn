CREATE TABLE [auth].[UserToken] (
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [Provider]   NVARCHAR (100)   NOT NULL,
    [TokenName]  NVARCHAR (100)   NOT NULL,
    [TokenValue] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED ([UserId] ASC, [Provider] ASC, [TokenName] ASC),
    CONSTRAINT [FK_UserToken_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);

