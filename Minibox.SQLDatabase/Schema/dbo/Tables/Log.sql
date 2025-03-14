CREATE TABLE [dbo].[Log] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Timestamp]   DATETIME2 (7)    NOT NULL,
    [Level]       NVARCHAR (50)    NOT NULL,
    [Message]     NVARCHAR (MAX)   NOT NULL,
    [Exception]   NVARCHAR (MAX)   NULL,
    [StackTrace]  NVARCHAR (MAX)   NULL,
    [RequestPath] NVARCHAR (500)   NULL,
    [StatusCode]  INT              NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);

