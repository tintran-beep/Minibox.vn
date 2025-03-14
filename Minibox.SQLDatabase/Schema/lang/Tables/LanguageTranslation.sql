CREATE TABLE [lang].[LanguageTranslation] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [LanguageKeyId] UNIQUEIDENTIFIER NOT NULL,
    [Code]          NVARCHAR (4)     NOT NULL,
    [Value]         NVARCHAR (1000)  NOT NULL,
    CONSTRAINT [PK_LanguageTranslation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LanguageTranslation_LanguageKey_LanguageKeyId] FOREIGN KEY ([LanguageKeyId]) REFERENCES [lang].[LanguageKey] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LanguageTranslation_LanguageKeyId]
    ON [lang].[LanguageTranslation]([LanguageKeyId] ASC);

