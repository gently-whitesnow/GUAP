CREATE OR REPLACE FUNCTION coords_distance(c1 coords, c2 coords)
    RETURNS NUMERIC AS $$
BEGIN
    RETURN sqrt(
            power(c1.latitude - c2.latitude, 2) +
            power(c1.longitude - c2.longitude, 2)
           );
END;
$$ LANGUAGE plpgsql IMMUTABLE;


-- Создание оператора `<->`
CREATE OPERATOR <-> (
    LEFTARG = coords,
    RIGHTARG = coords,
    PROCEDURE = coords_distance,
    COMMUTATOR = '<->'
    );

-- тестирование

SELECT
    p.object_id,
    p.location <-> '(55.500000,37.500000)'::coords AS distance
FROM
    park_object p
ORDER BY
    distance
LIMIT 5;

