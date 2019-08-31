CREATE TABLE [dbo].[t_province] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [name]       NVARCHAR (50) NOT NULL,
    [country_id] INT           NOT NULL,
    CONSTRAINT [PK_t_province] PRIMARY KEY CLUSTERED ([id] ASC)
);

