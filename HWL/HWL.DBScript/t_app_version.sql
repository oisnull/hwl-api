CREATE TABLE [dbo].[t_app_version] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [app_name]     NVARCHAR (50)  NULL,
    [app_version]  NVARCHAR (50)  NOT NULL,
    [download_url] NVARCHAR (200) NOT NULL,
    [publish_time] DATETIME       NOT NULL,
    [update_time]  DATETIME       NOT NULL,
    CONSTRAINT [PK_t_app_version] PRIMARY KEY CLUSTERED ([id] ASC)
);

