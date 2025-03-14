CREATE TABLE [dbo].[Media] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Type] INT              NOT NULL,
    [Url]  NVARCHAR (1000)  NOT NULL,
    CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([Id] ASC)
);

