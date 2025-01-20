-- Проверка что цена процедуры не отрицательная
CREATE OR REPLACE FUNCTION check_price_before_insert()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.price < 0 THEN
        RAISE EXCEPTION 'Price cannot be negative';
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_check_price_before_insert
    BEFORE INSERT
    ON procedure_history
    FOR EACH ROW
    EXECUTE FUNCTION check_price_before_insert();

-- тестирование
INSERT INTO procedure_history(visit_id, procedure_id, cabinet_id, price)
VALUES (1, 1, 1, -100); -- выбросит ошибку
