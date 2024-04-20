INSERT INTO procedures (name, price)
SELECT
        'Procedure ' || generate_series AS name,
        (random() * 1000)::numeric(10, 2) AS price
FROM generate_series(1, 998);

SELECT setval('procedures_id_seq', (SELECT max(id) FROM procedures));