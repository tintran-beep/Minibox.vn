CREATE TABLE [auth].[RoleClaim] (
    [RoleId]  UNIQUEIDENTIFIER NOT NULL,
    [ClaimId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED ([RoleId] ASC, [ClaimId] ASC),
    CONSTRAINT [FK_RoleClaim_Claim_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [auth].[Claim] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [auth].[Role] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleClaim_ClaimId]
    ON [auth].[RoleClaim]([ClaimId] ASC);

