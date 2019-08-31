CREATE TABLE [dbo].[t_user_pos] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [user_id]     INT            NOT NULL,
    [pos_details] NVARCHAR (200) NOT NULL,
    [lon]         FLOAT (53)     NOT NULL,
    [lat]         FLOAT (53)     NOT NULL,
    [geohash_key] NVARCHAR (20)  NOT NULL,
    [country_id]  INT            NOT NULL,
    [province_id] INT            NOT NULL,
    [city_id]     INT            NOT NULL,
    [district_id] INT            NOT NULL,
    [create_date] DATETIME       NOT NULL,
    [update_date] DATETIME       NOT NULL,
    CONSTRAINT [PK_t_user_pos] PRIMARY KEY CLUSTERED ([id] ASC)
);

