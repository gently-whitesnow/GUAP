CREATE EXTENSION IF NOT EXISTS pgcrypto;

INSERT INTO patients (name, surname, patronymic)
SELECT
    generate_random_name() AS name,
    generate_random_surname() AS surname,
    generate_random_patronymic() AS patronymic

-- count of
FROM generate_series(1, 10000);

INSERT INTO patients (name, surname, patronymic)
SELECT
    'Петр' AS name,
    'Иванов' AS surname,
    'Иванович' AS patronymic;

SELECT setval('patients_id_seq', (SELECT max(id) FROM patients));

