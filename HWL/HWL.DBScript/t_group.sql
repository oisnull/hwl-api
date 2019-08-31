CREATE TABLE [dbo].[t_group] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [group_guid]       NVARCHAR (50)  NOT NULL,
    [group_name]       NVARCHAR (50)  NOT NULL,
    [group_user_count] INT            NOT NULL,
    [group_note]       NVARCHAR (200) NULL,
    [build_user_id]    INT            NOT NULL,
    [build_date]       DATETIME       NOT NULL,
    [update_date]      DATETIME       NULL,
    CONSTRAINT [PK_t_group] PRIMARY KEY CLUSTERED ([id] ASC)
);

