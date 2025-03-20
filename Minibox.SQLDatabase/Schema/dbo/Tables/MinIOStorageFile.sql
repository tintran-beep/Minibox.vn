CREATE TABLE [dbo].[MinIOStorageFile] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [FileName]      NVARCHAR (255)   NOT NULL,
    [FilePath]      NVARCHAR (500)   NOT NULL,
    [ContentType]   NVARCHAR (100)   NOT NULL,
    [FileSize]      BIGINT           NOT NULL,
    [StorageBucket] NVARCHAR (100)   NULL,
    [IsPublic]      BIT              NOT NULL,
    [UploadedBy]    UNIQUEIDENTIFIER NULL,
    [UploadedAt]    DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_MinIOStorageFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

