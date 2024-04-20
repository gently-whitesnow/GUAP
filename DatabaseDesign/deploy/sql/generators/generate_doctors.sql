CREATE EXTENSION IF NOT EXISTS pgcrypto;

INSERT INTO doctors (name, surname, patronymic)
SELECT
    substr(md5(random()::text), 1, 10) AS name,
    substr(md5(random()::text), 1, 15) AS surname,
    substr(md5(random()::text), 1, 13) AS patronymic
FROM generate_series(1, 10000);

SELECT setval('doctors_id_seq', (SELECT max(id) FROM doctors));

