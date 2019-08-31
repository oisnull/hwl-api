CREATE TABLE [dbo].[t_near_circle_image] (
    [id]                  INT            IDENTITY (1, 1) NOT NULL,
    [near_circle_id]      INT            NOT NULL,
    [near_circle_user_id] INT            NOT NULL,
    [image_url]           NVARCHAR (200) NOT NULL,
    [width]               INT            NOT NULL,
    [height]              INT            NOT NULL,
    CONSTRAINT [PK_t_near_circle_image] PRIMARY KEY CLUSTERED ([id] ASC)
);

