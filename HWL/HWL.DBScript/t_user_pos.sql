CREATE TABLE [dbo].[t_user_pos] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [user_id]     INT            NOT NULL,
    [pos_details] NVARCHAR (200) NOT NULL,
    [coordinate_type] NVARCHAR (20) NULL,
    [location_where] INT NULL,
    [location_type] NVARCHAR(10) NULL,
    [lon]         FLOAT (53)     NOT NULL,
    [lat]         FLOAT (53)     NOT NULL,
    [radius]         FLOAT     NULL,
    [geohash_key] NVARCHAR (20)  NOT NULL,
    [country_id]  INT            NOT NULL,
    [province_id] INT            NOT NULL,
    [city_id]     INT            NOT NULL,
    [district_id] INT            NOT NULL,
    [town_id] INT            NOT NULL DEFAULT 0,
    [create_date] DATETIME       NOT NULL,
    [update_date] DATETIME       NOT NULL,
    CONSTRAINT [PK_t_user_pos] PRIMARY KEY CLUSTERED ([id] ASC)
);

