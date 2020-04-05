CREATE OR REPLACE FUNCTION cr_picture_product() RETURNS TRIGGER AS $$
    BEGIN
        INSERT INTO picture_product (id_product)
            VALUES (new.id);
		return new;
    END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER picture_product
AFTER  INSERT ON  product
    FOR EACH ROW EXECUTE PROCEDURE  cr_picture_product();

--
CREATE OR REPLACE FUNCTION set_def_role() RETURNS TRIGGER AS $$
    BEGIN
     
   	  INSERT INTO user_role (id_user)
        VALUES (new.id);
		return new;
    END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER set_def_role
AFTER  INSERT ON  users
    FOR EACH ROW EXECUTE PROCEDURE  set_def_role();


--
CREATE OR REPLACE FUNCTION set_def_cat_pic() RETURNS TRIGGER AS $$
    BEGIN
     
   	  INSERT INTO picture_category (id_category)
        VALUES (new.id);
		return new;
    END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER set_def_cat_pic
AFTER  INSERT ON  category
    FOR EACH ROW EXECUTE PROCEDURE  set_def_cat_pic();