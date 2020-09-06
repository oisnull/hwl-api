select * from t_user
where head_image like '%2.210:8033%'

update t_user set head_image=REPLACE(head_image,'2.210:8033','2.223:8033')
where head_image like '%2.210:8033%'
update t_user set circle_back_image=REPLACE(circle_back_image,'2.210:8033','2.223:8033')
where circle_back_image like '%2.210:8033%'



select * from t_circle
where image_urls like '%2.210:8033%'

select * from t_near_circle
where image_urls like '%2.210:8033%'

update t_circle set image_urls=REPLACE(image_urls,'2.210:8033','2.223:8033')
where image_urls like '%2.210:8033%'
update t_near_circle set image_urls=REPLACE(image_urls,'2.210:8033','2.223:8033')
where image_urls like '%2.210:8033%'