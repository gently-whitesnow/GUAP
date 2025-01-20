CREATE TABLE IF NOT EXISTS change_log
(
    id          SERIAL PRIMARY KEY,
    table_name  TEXT NOT NULL,
    operation   TEXT NOT NULL,
    old_data    TEXT,
    new_data    TEXT,
    changed_at  TIMESTAMP DEFAULT NOW()
);

-- Логируем добавление записи в procedure_history
CREATE OR REPLACE FUNCTION log_proc_hist_after_insert()
RETURNS TRIGGER AS $$
BEGIN
INSERT INTO change_log(table_name, operation, new_data)
VALUES('procedure_history', 'INSERT', row_to_json(NEW)::text);
RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log_proc_hist_after_insert
    AFTER INSERT
    ON procedure_history
    FOR EACH ROW
    EXECUTE FUNCTION log_proc_hist_after_insert();

-- тестирование
INSERT INTO procedure_history(visit_id, procedure_id, cabinet_id, price)
VALUES (1, 1, 1, 5000); -- добавит запись в change_log
