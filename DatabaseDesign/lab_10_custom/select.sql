-- 1. Аллеи, на которых встречаются разные виды кленов (клен в названии)

SELECT a.name AS alley_name
FROM alley a
         JOIN alley_object ao ON a.alley_id = ao.alley_id
         JOIN tree t ON ao.object_id = t.object_id
         JOIN species s ON t.species_id = s.species_id
WHERE s.species_name LIKE 'Клен%'
GROUP BY a.name
HAVING COUNT(DISTINCT s.species_id) > 1;

-- 2. Аллеи, на которых есть и статуи, и фонтаны

SELECT a.name AS alley_name
FROM alley a
         JOIN alley_object ao ON a.alley_id = ao.alley_id
         JOIN park_object po ON ao.object_id = po.object_id
         LEFT JOIN statue st ON po.object_id = st.object_id
         LEFT JOIN fountain f ON po.object_id = f.object_id
GROUP BY a.name
HAVING COUNT(DISTINCT st.object_id) > 0 AND COUNT(DISTINCT f.object_id) > 0;

-- 3. Дерево, которое было посажено позже всех

SELECT t.object_id, t.location, s.species_name, t.planting_date
FROM tree t
         JOIN species s ON t.species_id = s.species_id
ORDER BY t.planting_date DESC
LIMIT 1;

-- 4. Порода деревьев, которой больше всего

SELECT s.species_name, COUNT(t.object_id) AS tree_count
FROM tree t
         JOIN species s ON t.species_id = s.species_id
GROUP BY s.species_name
ORDER BY tree_count DESC
LIMIT 1;

-- 5. Аллея, на которой нет фонтанов

SELECT a.name AS alley_name
FROM alley a
         LEFT JOIN alley_object ao ON a.alley_id = ao.alley_id
         LEFT JOIN park_object po ON ao.object_id = po.object_id
         LEFT JOIN fountain f ON po.object_id = f.object_id
GROUP BY a.name
HAVING COUNT(f.object_id) = 0;
