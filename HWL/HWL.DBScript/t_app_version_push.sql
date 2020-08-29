CREATE TABLE [dbo].[t_app_version_push] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [app_version_id]     INT  NOT NULL,
    [pushed_users]  NVARCHAR (200)  NOT NULL,
    [push_date] DATETIME       NOT NULL,
    CONSTRAINT [PK_t_app_version_push] PRIMARY KEY CLUSTERED ([id] ASC)
);

