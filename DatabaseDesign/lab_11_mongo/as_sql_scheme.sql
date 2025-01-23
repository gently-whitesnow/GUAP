-- Пользовательский тип (пример - координаты)
CREATE TYPE coords AS (
    latitude  NUMERIC(9,6),
    longitude NUMERIC(9,6)
    );

-- Справочник пород деревьев
CREATE TABLE species (
                         species_id   SERIAL PRIMARY KEY,
                         species_name VARCHAR(100) NOT NULL
);

-- Аллея
CREATE TABLE alley (
                       alley_id SERIAL PRIMARY KEY,
                       name     VARCHAR(50) NOT NULL
);

-- Статуя
CREATE TABLE statue (
                        statue_id  SERIAL PRIMARY KEY,
                        location   coords NOT NULL,
                        statue_name VARCHAR(100) NOT NULL
);

-- Фонтан
CREATE TABLE fountain (
                          fountain_id  SERIAL PRIMARY KEY,
                          location     coords NOT NULL,
                          fountain_name VARCHAR(100) NOT NULL
);

-- Дерево
CREATE TABLE tree (
                      tree_id        SERIAL PRIMARY KEY,
                      location       coords NOT NULL,
                      species_id     INT NOT NULL,
                      planting_date  DATE NOT NULL,
                      trimming_date  DATE,
                      FOREIGN KEY (species_id) REFERENCES species(species_id)
);

-- Связь "Аллея – Объект" (многие ко многим)
-- Добавляем объект ID как обобщённый идентификатор объекта парка
CREATE TABLE alley_object (
                              alley_id   INT NOT NULL,
                              object_id  INT NOT NULL,
                              object_type VARCHAR(50) NOT NULL, -- Указываем тип объекта (statue, fountain, tree)
                              PRIMARY KEY (alley_id, object_id, object_type),
                              FOREIGN KEY (alley_id) REFERENCES alley(alley_id)
);