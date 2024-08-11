INSERT INTO visits (cabinet_id, doctor_id, patient_id, visit_date_time)
SELECT
        floor(random() * (select count(*) from cabinets) + 1)::int AS cabinet_id,
        floor(random() * (select count(*) from doctors) + 1)::int AS doctor_id,
        floor(random() * (select count(*) from patients) + 1)::int AS patient_id,
        now() - (random() * 1000)::int * interval '1 day' AS visit_date_time

-- count of
FROM generate_series(1, 20000);

SELECT setval('visits_id_seq', (SELECT max(id) FROM visits));
