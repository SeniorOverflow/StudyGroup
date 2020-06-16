
CREATE EXTENSION pgcrypto;

CREATE TYPE type_file AS ENUM ('url', 'doc', 'photo' , 'video');

CREATE TYPE picture_type AS ENUM ('jpg', 'png', 'jpeg');

CREATE TABLE pictures (
	guid 	uuid not null,
	type_pic 	picture_type not null,
	primary key(guid)
);
CREATE TABLE files (
	guid uuid not null,
	file_tipe type_file not null,
	primary key(guid)
);

CREATE TABLE users (
	id serial,
	first_name 		varchar(50) 	default 'Не указанно' 	not null,
	second_name		varchar(50) 	default 'Не указанно'	not null,
	login			varchar(50) 	UNIQUE 					not null,
	password 		text 									not null,
	mail 			text 			UNIQUE					not null,
	id_pic 			uuid			REFERENCES pictures(guid)
		ON DELETE CASCADE ON UPDATE CASCADE			 		not null,
	primary key(id)
);



CREATE TABLE roles (
	id serial,
	name 				varchar(50) 	UNIQUE						not null,
	description 		text 			default'NaD'				not null,
	primary key(id)
);



CREATE TABLE user_role(
	id serial,
	id_user			 	int 	REFERENCES users(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 			 		not null,
	id_role 			int 	REFERENCES roles(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 	default '1' 	not null,
	primary key(id)	
);

CREATE TABLE abilities(
	id serial,
	name 				varchar(50) 	UNIQUE						not null,
	description 		text 			default'NaD'				not null,
	primary key(id)
);

CREATE TABLE role_abilities(
	id serial,
	id_abilities		int 	REFERENCES abilities(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 			 		not null,
	id_role 			int 	REFERENCES roles(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 	 				not null,
	primary key(id)	
);

CREATE TABLE groups (
	id serial,
	title			varchar(50) 							not null,
	description		text										null,
	id_pic 			uuid				REFERENCES pictures(guid)
		ON DELETE CASCADE ON UPDATE CASCADE			 		not null,
	id_founder			 	int 	REFERENCES users(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 			 		not null,
	primary key(id)
);



CREATE TABLE memberships (
	id serial,
	id_group			int					REFERENCES groups(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 			 		not null,
	users_count   		int 				default '20' 				not null,
	memory_size			int 				default '5'					not null,
	date_begin 			TIMESTAMP 										not null,
	price 				decimal(10,2) 		default '0.00'				not null,
	primary key(id)	
);

CREATE TABLE  group_roles (
	id serial,
	id_group			int					REFERENCES groups(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 			 	not null,
	name 				varchar(50) 	UNIQUE						not null,
	description 		text 			default'NaD'				not null,
	is_change			boolean 		default 'false'				not null,
	primary key(id)
);

CREATE TABLE abilities_group(
	id serial,
	name 				varchar(50) 	UNIQUE						not null,
	description 		text 			default'NaD'				not null,
	primary key(id)
);

CREATE TABLE gr_abilities(
	id serial,
	id_abilities		int 	REFERENCES abilities(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 			 		not null,
	id_group_roles		int 	REFERENCES group_roles(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 	 				not null,
	primary key(id)	
);

CREATE TABLE user_group (
	id serial,
	id_user 		int 		REFERENCES users(id)
		ON DELETE CASCADE ON UPDATE CASCADE								not null,
	id_group_role	int 		REFERENCES group_roles(id)
		ON DELETE CASCADE ON UPDATE CASCADE			 	 				not null,
	id_group		int			REFERENCES groups(id)				
		ON DELETE CASCADE ON UPDATE CASCADE			 	 				not null,
	score			int 		default '0'								not null,
	primary key(id)
);

CREATE TABLE block_user (
	id serial,
	id_user 			int 			REFERENCES users(id)
		ON DELETE CASCADE ON UPDATE CASCADE					 		not null,
	id_group			int				REFERENCES groups(id)		not null,
	date_begin 			TIMESTAMP 									not null,
	date_end 			TIMESTAMP 									not null,
	cause 				text 			default'NaC' 				not null,
	primary key(id)
);

CREATE TABLE confirmation_email(
	id serial,
	id_user 			int  			REFERENCES users(id)
		ON DELETE CASCADE ON UPDATE CASCADE							 not null,
	date_begin 			TIMESTAMP 									 not null,
	date_end 			TIMESTAMP 									 not null,
	primary key(id)
);


CREATE TABLE	materials 		(
	id serial,
	title 				varchar(50)									not null,
	url_on_file			varchar(100)								not null,
	id_file 			uuid				REFERENCES files(guid)
		ON DELETE CASCADE ON UPDATE CASCADE			 				not null,
	primary key(id)
);

CREATE TABLE	group_materials (
	id serial,
	id_group 			int 			REFERENCES groups(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	id_material			int 			REFERENCES materials(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	primary key(id)
);

CREATE TABLE	homeworks 		(
	id serial,
	title 				varchar(50)									not null,
	deadline 			TIMESTAMP									not null,
	id_file 			uuid				REFERENCES files(guid)
		ON DELETE CASCADE ON UPDATE CASCADE			 				not null,
	primary key(id)
);

CREATE TABLE	group_homeworks (
	id serial,
	id_group 			int 			REFERENCES groups(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	id_homework			int 			REFERENCES homeworks(id)
		On DELETE CASCADE ON UPDATE CASCADE							not null,
	primary key(id)
);

CREATE TABLE	h_assessment (
	id serial,
	id_user 			int 			REFERENCES users(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	id_homework 		int 			REFERENCES homeworks(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	assessment 			SMALLINT									not null,
	id_file 			uuid				REFERENCES files(guid)
		ON DELETE CASCADE ON UPDATE CASCADE			 				not null,
	primary key(id)
);

CREATE TABLE	tests (
	id serial, 
	title 			varchar(50)										not null,
	description		text											not null,
	primary key(id)
);

CREATE TABLE	group_tests		(
	id serial,
	id_group	int 			REFERENCES groups(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	id_test		int 			REFERENCES tests(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	primary key(id)
);

CREATE TABLE	test_question	(
	id serial,
	id_test 	int 			REFERENCES tests(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	question 	text 												not null,
	primary key(id)
);

CREATE TABLE	answer		(
	id serial,
	id_question		int 				REFERENCES test_question(id)
			ON DELETE CASCADE	ON UPDATE CASCADE						not null,
	is_true			boolean												not null,
	primary key(id)
);

CREATE TABLE	t_assessment	(

	id serial,
	id_user 		int 			REFERENCES users(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	id_test 		int 			REFERENCES tests(id)
		ON DELETE CASCADE ON UPDATE CASCADE							not null,
	assessment 			SMALLINT									not null,
	
	primary key(id)
);


CREATE TABLE discount (
	id serial,
	count			int 											not null,
	date_begin 		TIMESTAMP						default 'now()' not null,
	date_end 		TIMESTAMP										not null,
	primary key(id)
)