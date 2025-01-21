truncate table species RESTART IDENTITY cascade;
truncate table alley RESTART IDENTITY cascade;
truncate table park_object RESTART IDENTITY cascade;
truncate table statue RESTART IDENTITY cascade;
truncate table fountain RESTART IDENTITY cascade;
truncate table tree RESTART IDENTITY cascade;
truncate table alley_object RESTART IDENTITY cascade;

-- Предполагается, что схема уже создана, включая наследование и тип coords.
-- Далее — пример наполнения таблиц тестовыми данными:

-- Виды деревьев
INSERT INTO species (species_name) VALUES
                                       ('Клен красный'),
                                       ('Клен серебристый'),
                                       ('Дуб'),
                                       ('Сосна'),
                                       ('Клен сахарный');

-- Аллеи
INSERT INTO alley (name) VALUES
                             ('Аллея 1'),
                             ('Аллея кленов'),
                             ('Вторая аллея'),
                             ('Аллея дубов');

-- Статуи
INSERT INTO statue (location, statue_name)
VALUES ('(55.123456,37.123456)'::coords, 'Памятник Пушкину');
INSERT INTO statue (location, statue_name)
VALUES ('(55.234567,37.234567)'::coords, 'Памятник Лермонтову');

-- Фонтаны
INSERT INTO fountain (location, fountain_name)
VALUES ('(55.345678,37.345678)'::coords, 'Большой фонтан');
INSERT INTO fountain (location, fountain_name)
VALUES ('(55.456789,37.456789)'::coords, 'Фонтан Ручеек');

-- Деревья
INSERT INTO tree (location, species_id, planting_date, trimming_date)
VALUES ('(55.987654,37.987654)'::coords, 1, '2020-05-10', '2021-05-10');  -- Клен красный
INSERT INTO tree (location, species_id, planting_date, trimming_date)
VALUES ('(55.876543,37.876543)'::coords, 2, '2020-05-15', NULL);        -- Клен серебристый
INSERT INTO tree (location, species_id, planting_date, trimming_date)
VALUES ('(55.765432,37.765432)'::coords, 3, '2019-04-20', '2020-04-20'); -- Дуб
INSERT INTO tree (location, species_id, planting_date, trimming_date)
VALUES ('(55.654321,37.654321)'::coords, 4, '2021-06-01', NULL);        -- Сосна
INSERT INTO tree (location, species_id, planting_date, trimming_date)
VALUES ('(55.543210,37.543210)'::coords, 5, '2022-06-05', NULL);        -- Клен сахарный
INSERT INTO tree (location, species_id, planting_date, trimming_date)
VALUES ('(55.432109,37.432109)'::coords, 1, '2023-04-15', NULL);        -- Клен красный

-- Связи объектов с аллеями
-- Предположим, что ID объектов идут в порядке их вставки:
-- 1,2 (статуи), 3,4 (фонтан), 5..10 (деревья)
-- Аллея 1: статуя 1, фонтан 3, дерево (сосна) 8
INSERT INTO alley_object (alley_id, object_id) VALUES
                                                   (1, 1),
                                                   (1, 3),
                                                   (1, 8);

-- Аллея кленов (ID=2): три разных клена — объекты 5,6,9
INSERT INTO alley_object (alley_id, object_id) VALUES
                                                  (2, 5),
                                                  (2, 6),
                                                  (2, 9);

-- Вторая аллея (ID=3): фонтан 4, дерево (клен красный) 10
INSERT INTO alley_object (alley_id, object_id) VALUES
                                                  (3, 4),
                                                  (3, 10);

-- Аллея дубов (ID=4): статуя 2, дуб 7
INSERT INTO alley_object (alley_id, object_id) VALUES
                                                  (4, 2),
                                                  (4, 7);
