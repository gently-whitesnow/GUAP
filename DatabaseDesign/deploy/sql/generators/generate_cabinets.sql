INSERT INTO cabinets (room)
SELECT
    series_num AS room

FROM generate_series(1, 300) AS series_num;

SELECT setval('procedures_id_seq', (SELECT max(id) FROM cabinets));