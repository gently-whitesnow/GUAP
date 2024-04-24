CREATE
OR REPLACE PROCEDURE cascade_delete_history_by_patient(
    in_patient_id int
)
AS
$$

BEGIN
    delete
    from procedures_history as h
    where patient_id = in_patient_id;

    delete
    from patients as p
    where id = in_patient_id;
END;
$$
LANGUAGE plpgsql;
   
-- calling
-- DO $$
--     declare
-- patient_id int := 10002;
-- BEGIN
-- CALL cascade_delete_history_by_patient(
--                 patient_id
--             );
-- 
-- RAISE NOTICE 'patient % deleted', patient_id;
-- END $$;