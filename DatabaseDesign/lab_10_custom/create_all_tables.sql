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

-- Базовый класс "Объект парка"
CREATE TABLE park_object (
                            object_id SERIAL PRIMARY KEY,
                            location  coords NOT NULL
);

-- Статуя с наследованием от park_object
CREATE TABLE statue (
                        statue_name VARCHAR(100) NOT NULL
)
    INHERITS (park_object);

-- Фонтан с наследованием от park_object
CREATE TABLE fountain (
                          fountain_name VARCHAR(100) NOT NULL
)
    INHERITS (park_object);

-- Дерево с наследованием от park_object
CREATE TABLE tree (
                      species_id     INT NOT NULL,
                      planting_date  DATE NOT NULL,
                      trimming_date  DATE
)
    INHERITS (park_object);

ALTER TABLE tree
    ADD CONSTRAINT fk_tree_species
        FOREIGN KEY (species_id) REFERENCES species(species_id);

-- Связь "Аллея – Объект" (многие ко многим)
CREATE TABLE alley_object (
                             alley_id  INT NOT NULL,
                             object_id INT NOT NULL,
                             PRIMARY KEY (alley_id, object_id),
                             FOREIGN KEY (alley_id)  REFERENCES alley(alley_id)
);
