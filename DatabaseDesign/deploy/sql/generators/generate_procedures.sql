INSERT INTO procedures (name, price)
SELECT
    generate_procedure_name_by_index(series_num) AS name,
    (random() * 1000)::numeric(10, 2) AS price

-- count of
FROM generate_series(1, 20) AS series_num;

SELECT setval('procedures_id_seq', (SELECT max(id) FROM procedures));