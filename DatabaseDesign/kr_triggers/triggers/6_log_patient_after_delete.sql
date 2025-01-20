-- Логируем удаление пациента
CREATE OR REPLACE FUNCTION log_patient_after_delete()
RETURNS TRIGGER AS $$
BEGIN
INSERT INTO change_log(table_name, operation, old_data)
VALUES('patients', 'DELETE', row_to_json(OLD)::text);
RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_patients_ad
    AFTER DELETE
    ON patients
    FOR EACH ROW
    EXECUTE FUNCTION log_patient_after_delete();

-- тестирование
DELETE FROM patients WHERE id = 1; -- запись об удалении в change_log
