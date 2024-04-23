-- Например данные по больнице собирались из разных систем
-- и мы хотим получить полную историю процедур

MERGE INTO procedures_history AS h
    USING (
        SELECT
            floor(random() * (select count(*) from procedures) + 1)::int AS procedure_id,
                floor(random() * (select count(*) from doctors) + 1)::int AS doctor_id,
                floor(random() * (select count(*) from patients) + 1)::int AS patient_id,
                now() - (random() * 1000)::int * interval '1 day' AS procedure_date_time,
                (random() * 1000)::numeric(10, 2) AS price,
                floor(random() * 100 + 1)::int AS cabinet
           FROM generate_series(1, 1)) AS data_source

    ON h.patient_id = data_source.patient_id
        and h.doctor_id = data_source.doctor_id
        and h.procedure_id = data_source.procedure_id

    WHEN NOT MATCHED THEN
        INSERT (procedure_id, doctor_id, patient_id, procedure_date_time, price, cabinet)
            VALUES (data_source.procedure_id, data_source.doctor_id, data_source.patient_id, data_source.procedure_date_time, data_source.price,
                    data_source.cabinet)

    WHEN MATCHED AND data_source.procedure_date_time > now() - interval '7 day'
    THEN UPDATE SET procedure_date_time = data_source.procedure_date_time