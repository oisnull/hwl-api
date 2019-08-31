CREATE TABLE [dbo].[t_circle_comment] (
    [id]             INT             IDENTITY (1, 1) NOT NULL,
    [circle_id]      INT             NOT NULL,
    [circle_user_id] INT             NOT NULL,
    [com_user_id]    INT             NOT NULL,
    [com_content]    NVARCHAR (1000) NOT NULL,
    [reply_user_id]  INT             NOT NULL,
    [comment_time]   DATETIME        NOT NULL,
    CONSTRAINT [PK_t_circle_comment] PRIMARY KEY CLUSTERED ([id] ASC)
);

