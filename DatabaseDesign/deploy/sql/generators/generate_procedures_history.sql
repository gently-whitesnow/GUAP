INSERT INTO procedures_history (procedure_id, doctor_id, patient_id, procedure_date_time, price, cabinet)
SELECT
        floor(random() * (select count(*) from procedures) + 1)::int AS procedure_id,
        floor(random() * (select count(*) from doctors) + 1)::int AS doctor_id,
        floor(random() * (select count(*) from patients) + 1)::int AS patient_id,
        now() - (random() * 1000)::int * interval '1 day' AS procedure_date_time,
        (random() * 1000)::numeric(10, 2) AS price,
        floor(random() * 100 + 1)::int AS cabinet

-- count of
FROM generate_series(1, 20000);

INSERT INTO procedures_history (procedure_id, doctor_id, patient_id, procedure_date_time, price, cabinet)
VALUES ((select max(id) from procedures as p where p.name = 'Consultation'),
        floor(random() * (select count(*) from doctors) + 1)::int,
           (select max(p.id) from patients as p
        where name = 'Петр' and surname = 'Иванов'),
        now() - (random() * 1000)::int * interval '1 day',
            (random() * 1000)::numeric(10, 2) ,
        floor(random() * 100 + 1)::int );


SELECT setval('procedures_history_id_seq', (SELECT max(id) FROM procedures_history));
