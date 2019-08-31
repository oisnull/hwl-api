CREATE TABLE [dbo].[t_user_friend] (
    [id]                 INT           IDENTITY (1, 1) NOT NULL,
    [user_id]            INT           NOT NULL,
    [friend_user_id]     INT           NOT NULL,
    [friend_user_remark] NVARCHAR (50) NULL,
    [friend_first_spell] NVARCHAR (2)  NULL,
    [add_time]           DATETIME      NOT NULL,
    CONSTRAINT [PK_t_user_friend] PRIMARY KEY CLUSTERED ([id] ASC)
);

