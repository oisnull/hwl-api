CREATE TABLE [dbo].[t_circle_like] (
    [id]           INT      IDENTITY (1, 1) NOT NULL,
    [circle_id]    INT      NOT NULL,
    [like_user_id] INT      NOT NULL,
    [like_time]    DATETIME NOT NULL,
    [is_delete]    BIT      NOT NULL,
    CONSTRAINT [PK_t_circle_like] PRIMARY KEY CLUSTERED ([id] ASC)
);

