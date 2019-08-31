CREATE TABLE [dbo].[t_user] (
    [id]                INT            IDENTITY (1, 1) NOT NULL,
    [symbol]            NVARCHAR (20)  NULL,
    [name]              NVARCHAR (30)  NULL,
    [mobile]            NVARCHAR (11)  NOT NULL,
    [email]             NVARCHAR (50)  NOT NULL,
    [password]          NVARCHAR (50)  NOT NULL,
    [head_image]        NVARCHAR (100) NULL,
    [life_notes]        NVARCHAR (200) NULL,
    [sex]               INT            NOT NULL,
    [status]            INT            NOT NULL,
    [circle_back_image] NVARCHAR (100) NOT NULL,
    [register_country]  INT            NOT NULL,
    [register_province] INT            NOT NULL,
    [register_city]     INT            NOT NULL,
    [register_district] INT            NOT NULL,
    [register_date]     DATETIME       NOT NULL,
    [update_date]       DATETIME       NULL,
    CONSTRAINT [PK_t_user] PRIMARY KEY CLUSTERED ([id] ASC)
);

