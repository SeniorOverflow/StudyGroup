INSERT INTO homeworks(title, deadline, url_on_file, type)
	VALUES ('1', now(), '121', 'doc' );

	INSERT INTO public.h_assessment(
	 id_user, id_homework, assessment, url_on_file)
	VALUES (2, 1, 0,  'doc');