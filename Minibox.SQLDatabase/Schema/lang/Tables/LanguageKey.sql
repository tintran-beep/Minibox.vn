CREATE TABLE [lang].[LanguageKey] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Key]          NVARCHAR (250)   NOT NULL,
    [DefaultValue] NVARCHAR (1000)  NOT NULL,
    CONSTRAINT [PK_LanguageKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_LanguageKey_Key]
    ON [lang].[LanguageKey]([Key] ASC);

