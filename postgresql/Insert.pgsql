INSERT INTO users(first_name, second_name ,login,password ,mail)
VALUES
 ('igor', 'popov','donenot',crypt('12345', gen_salt('bf', 8)),'haha@mail.ru');

--


INSERT INTO roles(name, description)
VALUES
 ('user', 'just user '),
 ('Admin', 'it`s god  '),
 ('Moderator', 'less then admin and more then user');


INSERT INTO abilities(
	id, name)
	VALUES
	 (1,'управление категориями'),
	 (2,'управление разработчиками'),
	 (3,'управление продуктами'),
	 (4,'управление ролями'),
	 (5,'управление подписками'),
	 (6,'управление транзакциями'),
	 (7,'управление	пользователями'),
	 (8,'управление	коментракиями'),
	 (9,'редактирование подписок');


INSERT INTO role_abilities(
	 id_abilities, id_role)
	VALUES
	 (1, 2), -- Admin
	 (2, 2),
	 (3, 2),
	 (4, 2),
	 (5, 2),
	 (6, 2),
	 (7, 2),
	 (8, 2),
	 (9, 2),

	 (1, 3), -- Moderator
	 (3, 3),
	 (5, 3),
	 (8, 3),
	 (9, 3);


INSERT INTO category(name, description )
VALUES
 ('FPS', 		'Firs Person Shooter'),
 ('RPG', 		'Role Play Game'),
 ('Horror',   	'Horror game'),
 ('Strategy', 	'Strategy game'),
 ('Simulator', 	'Simulated life in the game '),
 ('Action', 	'Action game'),
 ('Adventure',  'Adventure game'),
 ('Casual', 	'Casual game'),
 ('Puzzle', 	'Puzzle game'),
 ('SandBox', 	'You Creator'),
 ('OpenWorld', 	'Open World'),
 ('MMO', 		'Massively multiplayer online'),
 ('Racing', 	'Racing game'),
 ('Platformer', 'Platformer game'),
 ('Arcade', 	'Arcade game'),
 ('Third Person', 'Third Person game'),
 ('Software', 'Software'),
 ('Mobile software ', 'Mobile software '),
 ('Mobile Game ', 'Mobile Game'),
 ('Building', 'Building'),
 ('Management', 'Management');

 INSERT INTO type_of_subscription(name, count_days, price)
	VALUES ( 'Week', 		7, 		'49.99'),
		   ( 'Month', 		30, 	'129.99'),
		   ( '3 Month',		91,		'249.99'),
		   ( '6 Month',		182,	'499.99'),
		   ( '12 Month', 	365,	'899.99');


INSERT INTO users(first_name, second_name ,login,password ,mail)
VALUES
 ('Mr', 'Admin','admin',crypt('admin', gen_salt('bf', 8)),'2010igorpopov2010@gmail.com');

 INSERT INTO user_role (id_user, id_role)
	VALUES(1,2)

--

 INSERT INTO activity_status(name, description)
	VALUES ('online',''),('offline',''),('away',''),('play','');

INSERT INTO social_status(
	id, name, description)
	VALUES 	(1, 'add request', 			'add  request  to selected person'),
			(2, 'cancel request', 		'selected person canceled your add request'),
			(3, 'confirm request', 		'selected person confirmed your add requeest '),
			(4, 'add to block list', 	'you or any person added person to block list'),
			(5, 'remove friend', 		'you or any person removed person from friend list');
	

	
INSERT INTO public.social_interconnection(                         					-- add to friend
	id_user_first, id_user_second, id_status)
	VALUES (1, 2, 1);

select * from social_interconnection where id_user_second = 1 and id_status =1   	-- select add request 

UPDATE public.social_interconnection
	SET  id_status=2
	WHERE id_user_first = 2 and id_user_second = 1;                              	-- cancel  request

UPDATE public.social_interconnection
	SET  id_status=3
	WHERE id_user_first = 2 and id_user_second = 1;                              	-- confirm request

UPDATE public.social_interconnection
	SET  id_status=4
	WHERE id_user_first = 2 and id_user_second = 1;                              	-- add to block list


UPDATE public.social_interconnection
	SET  id_status=5
	WHERE id_user_first = 2 and id_user_second = 1;                              	-- remove friend

--

INSERT INTO product(name, description ,id_dev,id_category ,price,url_on_product)
VALUES
 ('minerush', 'it`s my new game ',1,1,'500.50','it is url');


INSERT INTO public.ivent_product(
	id_product, ivent_name, date_ivent, description)
	VALUES (3, 'Start', '13-12-2018', 'It is start day for product placement');


INSERT INTO public.dlc_for_product(
	id_product, id_sub_product)
	VALUES (,);

INSERT INTO user_role (id_user, id_role)
	VALUES(1,2)