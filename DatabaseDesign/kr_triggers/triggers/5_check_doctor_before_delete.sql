-- Например, не даём удалить врача, если у него есть предстоящие визиты (упростим — вообще любые визиты):
CREATE OR REPLACE FUNCTION check_doctor_before_delete()
RETURNS TRIGGER AS $$
DECLARE
v_count INTEGER;
BEGIN
SELECT COUNT(*) INTO v_count FROM visits as v WHERE doctor_id = OLD.id and v.visit_date_time < now();
IF v_count > 0 THEN
        RAISE EXCEPTION 'Cannot delete doctor with existing visits';
END IF;
RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_check_doctor_before_delete
    BEFORE DELETE
    ON doctors
    FOR EACH ROW
    EXECUTE FUNCTION check_doctor_before_delete();

-- тестирование
DELETE FROM doctors WHERE id = 1; -- если есть visits, выбросит ошибку

