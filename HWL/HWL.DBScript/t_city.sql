CREATE TABLE [dbo].[t_city] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50) NOT NULL,
    [province_id] INT           NOT NULL,
    CONSTRAINT [PK_t_city] PRIMARY KEY CLUSTERED ([id] ASC)
);

