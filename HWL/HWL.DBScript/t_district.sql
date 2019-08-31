CREATE TABLE [dbo].[t_district] (
    [id]      INT           IDENTITY (1, 1) NOT NULL,
    [name]    NVARCHAR (50) NOT NULL,
    [city_id] INT           NOT NULL,
    CONSTRAINT [PK_t_district] PRIMARY KEY CLUSTERED ([id] ASC)
);

