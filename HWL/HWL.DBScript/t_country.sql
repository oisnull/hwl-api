CREATE TABLE [dbo].[t_country] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_t_country] PRIMARY KEY CLUSTERED ([id] ASC)
);

