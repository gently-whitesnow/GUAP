-- Логируем изменения в visits (например, если меняется дата визита):
CREATE OR REPLACE FUNCTION log_visits_after_update()
RETURNS TRIGGER AS $$
BEGIN
INSERT INTO change_log(table_name, operation, old_data, new_data)
VALUES(
          'visits',
          'UPDATE',
          row_to_json(OLD)::text,
          row_to_json(NEW)::text
      );
RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log_visits_after_update
    AFTER UPDATE
    ON visits
    FOR EACH ROW
    EXECUTE FUNCTION log_visits_after_update();

-- тестирование
UPDATE visits
SET visit_date_time = '2024-09-01 10:00:00'
WHERE id = 1; -- запись об изменении в change_log
