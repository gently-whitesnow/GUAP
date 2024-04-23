CREATE EXTENSION IF NOT EXISTS pgcrypto;

INSERT INTO doctors (name, surname, patronymic)
SELECT
    generate_random_name() AS name,
    generate_random_surname() AS surname,
    generate_random_patronymic() AS patronymic

-- count of
FROM generate_series(1, 100);

INSERT INTO doctors (name, surname, patronymic)
SELECT
    'Иван' AS name,
    'Иванов' AS surname,
    'Иванович' AS patronymic;

SELECT setval('doctors_id_seq', (SELECT max(id) FROM doctors));

