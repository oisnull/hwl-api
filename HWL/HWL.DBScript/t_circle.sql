CREATE TABLE [dbo].[t_circle] (
    [id]             INT             IDENTITY (1, 1) NOT NULL,
    [user_id]        INT             NOT NULL,
    [content_type]   INT             NOT NULL,
    [content_info]  NVARCHAR (2000) NULL,
    [image_urls]  NVARCHAR (2000) NULL,
    [pos_desc]       NVARCHAR (50)   NULL,
    [lat]            FLOAT (53)      NOT NULL,
    [lon]            FLOAT (53)      NOT NULL,
    [link_url]       NVARCHAR (200)  NULL,
    [link_title]     NVARCHAR (300)  NULL,
    [link_image]     NVARCHAR (200)  NULL,
    [image_count]    INT             NOT NULL,
    [comment_count]  INT             NOT NULL,
    [like_count]     INT             NOT NULL,
    [publish_time]   DATETIME        NOT NULL,
    [update_time]    DATETIME        NULL,
    CONSTRAINT [PK_t_circle] PRIMARY KEY CLUSTERED ([id] ASC)
);

