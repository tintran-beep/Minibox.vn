CREATE TABLE [auth].[UserClaim] (
    [UserId]  UNIQUEIDENTIFIER NOT NULL,
    [ClaimId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([UserId] ASC, [ClaimId] ASC),
    CONSTRAINT [FK_UserClaim_Claim_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [auth].[Claim] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserClaim_ClaimId]
    ON [auth].[UserClaim]([ClaimId] ASC);

