CREATE TABLE [dbo].[t_near_circle_comment] (
    [id]              INT            IDENTITY (1, 1) NOT NULL,
    [near_circle_id]  INT            NOT NULL,
    [comment_user_id] INT            NOT NULL,
    [content_info]    NVARCHAR (500) NULL,
    [reply_user_id]   INT            NOT NULL,
    [comment_time]    DATETIME       NOT NULL,
    CONSTRAINT [PK_t_near_circle_comment] PRIMARY KEY CLUSTERED ([id] ASC)
);

