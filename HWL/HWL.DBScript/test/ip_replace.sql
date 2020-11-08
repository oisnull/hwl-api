
DECLARE @oldString VARCHAR(100)
DECLARE @newString VARCHAR(100)

--http://111.229.252.205:8085/upload/user-head/2020/temp20200111125218.png
SET @oldString = '10.61.8.23:8015'
SET @newString = '10.61.8.23:8081'

--replace user related imgs
UPDATE t_user set head_image=REPLACE(head_image,@oldString,@newString)
where head_image like '%'+@oldString+'%'

UPDATE t_user set circle_back_image=REPLACE(circle_back_image,@oldString,@newString)
where circle_back_image like '%'+@oldString+'%'

SELECT TOP 10 * from t_user 
where head_image like '%'+@newString+'%'


--replace circle related imgs
UPDATE t_circle set image_urls=REPLACE(image_urls,@oldString,@newString)
where image_urls like '%'+@oldString+'%'
UPDATE t_near_circle set image_urls=REPLACE(image_urls,@oldString,@newString)
where image_urls like '%'+@oldString+'%'

SELECT TOP 10 * from t_circle
where image_urls like '%'+@newString+'%'

SELECT TOP 10 * from t_near_circle
where image_urls like '%'+@newString+'%'