CREATE TABLE [dbo].[t_group_user] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [group_guid] NVARCHAR (50) NOT NULL,
    [user_id]    INT           NOT NULL,
    [add_date]   DATETIME      NOT NULL,
    CONSTRAINT [PK_t_group_user] PRIMARY KEY CLUSTERED ([id] ASC)
);

