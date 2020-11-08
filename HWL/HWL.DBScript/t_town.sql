CREATE TABLE [dbo].[t_town] (
    [id]      INT           IDENTITY (1, 1) NOT NULL,
    [name]    NVARCHAR (50) NOT NULL,
    [district_id] INT           NOT NULL,
    CONSTRAINT [PK_t_town] PRIMARY KEY CLUSTERED ([id] ASC)
);

