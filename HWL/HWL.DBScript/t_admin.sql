CREATE TABLE [dbo].[t_admin] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [login_name]  NVARCHAR (50) NOT NULL,
    [login_pwd]   NVARCHAR (50) NOT NULL,
    [real_name]   NVARCHAR (30) NOT NULL,
    [create_date] DATETIME      NOT NULL,
    CONSTRAINT [PK_t_admin] PRIMARY KEY CLUSTERED ([id] ASC)
);

