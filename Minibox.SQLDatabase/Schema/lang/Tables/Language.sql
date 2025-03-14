CREATE TABLE [lang].[Language] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Code]  NVARCHAR (4)     NOT NULL,
    [Value] NVARCHAR (100)   NOT NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED ([Id] ASC)
);

