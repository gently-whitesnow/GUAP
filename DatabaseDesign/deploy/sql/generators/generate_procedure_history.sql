INSERT INTO procedure_history (visit_id, procedure_id, price, cabinet_id)
SELECT
        floor(random() * (select count(*) from visits) + 1)::int AS visit_id,
        floor(random() * (select count(*) from procedures) + 1)::int AS procedure_id,
        (random() * 1000)::numeric(10, 2) AS price,
        floor(random() * (select count(*) from cabinets) + 1)::int AS cabinet_id

-- count of
FROM generate_series(1, 30000);

SELECT setval('procedure_history_id_seq', (SELECT max(id) FROM procedure_history));
