create TABLE IF NOT EXISTS antivirus
(
    id           serial
        primary key,
    name         varchar(255) not null,
    price        money        not null,
    developer    varchar(255) not null,
    release_year integer      not null
);

create TABLE IF NOT EXISTS features
(
    id               serial
        primary key,
    antivirus_id     integer      not null
        references antivirus(id)
            on delete cascade,
    feature_name     varchar(255) not null,
    description      text,
    importance_level integer      not null
);

truncate table antivirus RESTART IDENTITY cascade;
truncate table features RESTART IDENTITY cascade;

-- Вставка данных в таблицу antivirus
INSERT INTO antivirus (name, price, developer, release_year)
VALUES
    ('Kaspersky', 2000, 'Kaspersky Lab', 1997),
    ('Avast', 0, 'Avast Software', 1988),
    ('SecureList', 2500, 'SecureList Inc.', 2010);

-- Вставка данных в таблицу features
INSERT INTO features (antivirus_id, feature_name, description, importance_level)
VALUES
    -- Особенности для Kaspersky
    ((SELECT id FROM antivirus WHERE name = 'Kaspersky'), 'Высокий уровень защиты', 'Эффективная защита от вирусов и шпионского ПО', 5),
    ((SELECT id FROM antivirus WHERE name = 'Kaspersky'), 'Защита от фишинга', 'Блокировка сайтов, крадущих данные', 4),

    -- Особенности для Avast
    ((SELECT id FROM antivirus WHERE name = 'Avast'), 'Базовая защита', 'Бесплатная защита от основных угроз', 3),

    -- Особенности для SecureList
    ((SELECT id FROM antivirus WHERE name = 'SecureList'), 'Эвристический анализ', 'Обнаружение угроз на основе поведения программ', 5),
    ((SELECT id FROM antivirus WHERE name = 'SecureList'), 'Минимальная нагрузка на систему', 'Низкие системные требования', 4);