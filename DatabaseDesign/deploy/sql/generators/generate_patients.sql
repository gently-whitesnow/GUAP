CREATE EXTENSION IF NOT EXISTS pgcrypto;

INSERT INTO patients (name, surname, patronymic, policy)
SELECT
    generate_random_name() AS name,
    generate_random_surname() AS surname,
    generate_random_patronymic() AS patronymic,
    substr(md5(random()::text), 1, 10) as policy

-- count of
FROM generate_series(1, 10000);

INSERT INTO patients (name, surname, patronymic, policy)
SELECT
    'Петр' AS name,
    'Иванов' AS surname,
    'Иванович' AS patronymic,
    substr(md5(random()::text), 1, 10) as policy;

SELECT setval('patients_id_seq', (SELECT max(id) FROM patients));

