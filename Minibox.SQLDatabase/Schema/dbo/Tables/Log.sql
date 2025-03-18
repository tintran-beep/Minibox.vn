CREATE TABLE [dbo].[Log] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Timestamp]     DATETIME2 (7)    NOT NULL,
    [Level]         NVARCHAR (50)    NOT NULL,
    [Message]       NVARCHAR (MAX)   NOT NULL,
    [Exception]     NVARCHAR (MAX)   NULL,
    [StackTrace]    NVARCHAR (MAX)   NULL,
    [Properties]    NVARCHAR (MAX)   NULL,
    [RequestPath]   NVARCHAR (500)   NULL,
    [StatusCode]    INT              NULL,
    [UserId]        UNIQUEIDENTIFIER NULL,
    [RequestId]     NVARCHAR (100)   NULL,
    [SourceContext] NVARCHAR (255)   NULL,
    [MachineName]   NVARCHAR (255)   NULL,
    [IPAddress]     NVARCHAR (50)    NULL,
    [CreatedAt]     DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);
