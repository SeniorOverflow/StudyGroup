SELECT title, deadline, homeworks.url_on_file, type , assessment
	from homeworks inner join  h_assessment on homeworks.id = h_assessment.id_homework
	WHERE id_user = 1

select groups.id, groups.title , groups.description , picture , group_roles.name 
	from groups inner join user_group on groups.id = user_group.id_group
	inner join group_roles on user_group.id_group_role = group_roles.id
	where id_user = 1