CREATE TABLE [dbo].[t_near_circle] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [user_id]       INT            NOT NULL,
    [content_type]  INT            NOT NULL,
    [content_info]  NVARCHAR (2000) NULL,
    [image_urls]  NVARCHAR (2000) NULL,
    [link_title]    NVARCHAR (100) NULL,
    [link_url]      NVARCHAR (200) NULL,
    [link_image]    NVARCHAR (200) NULL,
    [pos_id]        INT            NOT NULL,
    [pos_desc]      NVARCHAR (50)  NULL,
    [lon]           FLOAT (53)     NOT NULL,
    [lat]           FLOAT (53)     NOT NULL,
    [image_count]   INT            NOT NULL,
    [comment_count] INT            NOT NULL,
    [like_count]    INT            NOT NULL,
    [publish_time]  DATETIME       NOT NULL,
    [update_time]   DATETIME       NULL,
    CONSTRAINT [PK_t_near_circle] PRIMARY KEY CLUSTERED ([id] ASC)
);

