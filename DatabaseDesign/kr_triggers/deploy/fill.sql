truncate table procedure_history RESTART IDENTITY cascade;
truncate table visits RESTART IDENTITY cascade;
truncate table cabinets RESTART IDENTITY cascade;
truncate table doctors RESTART IDENTITY cascade;
truncate table patients RESTART IDENTITY cascade;
truncate table procedures RESTART IDENTITY cascade;


INSERT INTO cabinets (room)
VALUES ('101'),
       ('102'),
       ('103'),
       ('104');

INSERT INTO doctors (name, surname, patronymic)
VALUES ('Иван', 'Иванов', 'Иванович'),
       ('Петр', 'Петров', 'Петрович'),
       ('Сергей', 'Сергеев', 'Сергеевич');

INSERT INTO patients (name, surname, patronymic)
VALUES ('Петр', 'Иванов', 'Петрович'),
       ('Алексей', 'Сидоров', 'Иванович'),
       ('Ольга', 'Петрова', 'Сергеевна');

INSERT INTO procedures (name, price)
VALUES ('Лазерная коррекция зрения', 5000.00),
       ('Лазерная эпиляция', 3000.00),
       ('УЗИ', 1500.00),
       ('Консультация', 1000.00);

INSERT INTO visits (cabinet_id, doctor_id, patient_id, visit_date_time)
VALUES (1, 1, 1, '2024-08-01 10:00:00'),
       (1, 1, 2, '2024-08-01 11:00:00'),
       (2, 2, 3, '2024-08-01 12:00:00'),
       (3, 3, 1, '2024-08-02 10:00:00');

INSERT INTO procedure_history (visit_id, procedure_id, cabinet_id, price)
VALUES (1, 1, 1, 5000.00), -- Лазерная коррекция зрения, Иванов Иван Иванович
       (1, 2, 1, 3000.00), -- Лазерная эпиляция, Иванов Иван Иванович
       (2, 3, 1, 1500.00), -- УЗИ, Иванов Иван Иванович
       (3, 2, 2, 3000.00); -- Лазерная эпиляция, Петров Петр Петрович


INSERT INTO procedure_history (visit_id, procedure_id, cabinet_id, price)
VALUES (2, 4, 1, 4000.00); -- Консультация, Иванов Иван Иванович


