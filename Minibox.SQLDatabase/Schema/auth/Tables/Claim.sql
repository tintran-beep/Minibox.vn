CREATE TABLE [auth].[Claim] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Type]        NVARCHAR (100)   NOT NULL,
    [Value]       NVARCHAR (100)   NOT NULL,
    [Description] NVARCHAR (500)   NOT NULL,
    CONSTRAINT [PK_Claim] PRIMARY KEY CLUSTERED ([Id] ASC)
);

