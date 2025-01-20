-- Не дадим обновить цену в прошлое (не ниже исходной):
CREATE OR REPLACE FUNCTION check_price_before_update()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.price < OLD.price THEN
        RAISE EXCEPTION 'New price cannot be lower than old price';
END IF;
RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_check_price_before_update
    BEFORE UPDATE
    ON procedure_history
    FOR EACH ROW
    EXECUTE FUNCTION check_price_before_update();

-- тестирование
UPDATE procedure_history
SET price = 1
WHERE id = 3; -- если < старой цены – выбросит ошибку
