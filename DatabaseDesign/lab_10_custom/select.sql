-- Выборка с условием к таблице-предку и потомку:
-- Получим все объекты парка с их типами и дополнительной информацией из потомков
SELECT
    po.object_id,
    po.location,
    CASE
        WHEN st.statue_name IS NOT NULL THEN 'Statue'
        WHEN ft.fountain_name IS NOT NULL THEN 'Fountain'
        WHEN tr.species_id IS NOT NULL THEN 'Tree'
        END AS object_type,
    COALESCE(st.statue_name, ft.fountain_name, sp.species_name) AS object_details
FROM park_object po
         LEFT JOIN statue st ON po.object_id = st.object_id
         LEFT JOIN fountain ft ON po.object_id = ft.object_id
         LEFT JOIN tree tr ON po.object_id = tr.object_id
         LEFT JOIN species sp ON tr.species_id = sp.species_id;

-- Выборка только из потомков:
-- Получить все деревья с информацией о породе

SELECT
    t.object_id,
    t.location,
    s.species_name,
    t.planting_date,
    t.trimming_date
FROM tree t
         JOIN species s ON t.species_id = s.species_id;

-- Выборка только из предка:
-- Получить координаты всех объектов парка:

INSERT INTO park_object (location) VALUES ('(55.123456,37.123456)'::coords);
SELECT
    object_id,
    location
FROM ONLY park_object;



-- использование оператора only
SELECT
    object_id,
    location
FROM ONLY park_object;