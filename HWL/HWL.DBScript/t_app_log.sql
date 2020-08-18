CREATE TABLE [dbo].[t_app_log] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [app_type]     NVARCHAR (20)  NULL,
    [app_version]  NVARCHAR (10)  NULL,
    [crash_info] NVARCHAR (500) NULL,
    [crash_details] TEXT NULL,
    [create_time] DATETIME       NOT NULL,
    CONSTRAINT [PK_t_app_log] PRIMARY KEY CLUSTERED ([id] ASC)
);

