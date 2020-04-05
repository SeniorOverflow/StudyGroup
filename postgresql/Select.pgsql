
SELECT * FROM users WHERE mail = lower('haha@mail.ru') AND
                          password = crypt('12345', password);


SELECT name,price,picture_product.url_picture 
	from 
		product inner join picture_product on product.id = picture_product.id_product ;

SELECT first_name,roles.name
	from 
		users inner join user_role on users.id = user_role.id_user 
		inner join roles on user_role.id_role = roles.id;

SELECT category.name,category.description,picture_category.url_picture
	FROM 
		category INNER JOIN picture_category on category.id = picture_category.id_category ;

SELECT roles.name
	FROM 
		users INNER JOIN user_role on users.id = user_role.id_user 
		INNER JOIN roles on user_role.id_role = roles.id
	WHERE login = 'donenot';

SELECT login ,name_of_company 
	FROM users INNER JOIN user_dev on users.id = user_dev.id_user
			   INNER JOIN developers on user_dev.id_dev=developers.id;

SELECT product.id  FROM users INNER JOIN user_dev on users.id = user_dev.id_user 
							   INNER JOIN product on user_dev.id_dev = product.id_dev 
				   where users.id = 1 AND product.id = 3

SELECT * FROM ivent_product where id_product = 3;

SELECT label_name FROM label_product;

SELECT url_picture FROM picture_product WHERE id_product = 3;


SELECT name ,price,def_picture FROM product  INNER JOIN dlc_for_product on product.id = dlc_for_product.id_sub_product
WHERE dlc_for_product.id_product = 3; 


SELECT name_of_company  
	FROM developers inner join user_dev on developers.id = user_dev.id_dev 
	WHERE user_dev.id_user = 1 

select login, id_user , id_role from user_role inner join users on user_role.id_user = users.id


select sum(product.price*shopping_cart.count) from shopping_cart inner join  product on 
shopping_cart.id_product = product.id  where shopping_cart.id_user = 1






-- ЗАПРОСЫ в друзья 


SELECT users.id,first_name,second_name,login,lvl
	from users 
	inner join social_interconnection on 
		users.id = social_interconnection.id_user_first
	WHERE social_interconnection.id_user_second = 1  
		AND social_interconnection.id_social_status = 1

--Пользователи в чс
SELECT users.id,first_name,second_name,login,lvl
	from users 
	inner join social_interconnection on 
		users.id = social_interconnection.id_user_second
	WHERE users.id != 1  AND social_interconnection.id_social_status = 4
--