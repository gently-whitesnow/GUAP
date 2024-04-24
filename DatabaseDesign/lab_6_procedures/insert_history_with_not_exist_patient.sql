CREATE OR REPLACE PROCEDURE insert_history_with_not_exist_patient(
    in_policy varchar,
    in_procedure_id int,
    in_doctor_id int,
    in_cabinet float,
    inout result_procedure_id int,
    in_name varchar = null,
    in_surname varchar = null,
    in_patronymic varchar = null
)
AS
$$
DECLARE
    current_patient_id int;
BEGIN
    current_patient_id := (SELECT id FROM patients WHERE policy = in_policy);

    IF current_patient_id IS NULL THEN
        INSERT INTO patients(name, surname, patronymic, policy)
        VALUES (in_name, in_surname, in_patronymic, in_policy)
        RETURNING id INTO current_patient_id;
    END IF;

    INSERT INTO procedures_history(patient_id, procedure_id, doctor_id, cabinet, procedure_date_time, price)
    VALUES (current_patient_id, in_procedure_id, in_doctor_id, in_cabinet, now(), (SELECT price FROM procedures WHERE id = in_procedure_id))
    RETURNING id INTO result_procedure_id;
END;
$$ LANGUAGE plpgsql;


-- calling
-- DO $$
--     DECLARE
--         result_procedure_id_out int;
--     BEGIN
--         CALL insert_history_with_not_exist_patient(
--                 '9f304b18d8',
--                 1,
--                 1,
--                 20,
--                 result_procedure_id_out, -- inout parameter
--                 'Call',
--                 'Callof',
--                 'Callorevich'
--             );
-- 
--         RAISE NOTICE 'result_procedure_id: %', result_procedure_id_out;
--     END $$;