GO
INSERT INTO [dbo].[t_admin]
           ([login_name]
           ,[login_pwd]
           ,[real_name]
           ,[create_date])
     VALUES
           ('admin','hwl.123456','Super Admin',GETDATE())
GO