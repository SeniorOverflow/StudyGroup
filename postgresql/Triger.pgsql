CREATE OR REPLACE FUNCTION set_def_group_role() RETURNS TRIGGER AS $$
    BEGIN
        INSERT INTO public.group_roles(
	    id_group, name, description, is_change)
	    VALUES ( new.id, 'Пользователь', 'Может просматривать группу и матерьялы', 'false');
        return new;
    END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER set_def_group_role
AFTER  INSERT ON  groups
    FOR EACH ROW EXECUTE PROCEDURE  set_def_group_role();