CREATE TABLE [dbo].[t_user_code] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [user_id]      INT            NOT NULL,
    [user_account] NVARCHAR (50)  NOT NULL,
    [code_type]    INT            NOT NULL,
    [code]         NVARCHAR (6)   NOT NULL,
    [create_date]  DATETIME       NOT NULL,
    [expire_time]  DATETIME       NOT NULL,
    [remark]       NVARCHAR (100) NULL,
    CONSTRAINT [PK_t_user_code] PRIMARY KEY CLUSTERED ([id] ASC)
);

