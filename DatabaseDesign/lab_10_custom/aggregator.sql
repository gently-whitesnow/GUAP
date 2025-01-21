CREATE OR REPLACE FUNCTION min_coords_distance(
    state NUMERIC,
    current coords,
    reference coords
) RETURNS NUMERIC AS $$
BEGIN
    IF state IS NULL THEN
        RETURN sqrt(
                power(current.latitude - reference.latitude, 2) +
                power(current.longitude - reference.longitude, 2)
               );
    ELSE
        RETURN least(
                state,
                sqrt(
                        power(current.latitude - reference.latitude, 2) +
                        power(current.longitude - reference.longitude, 2)
                )
               );
    END IF;
END;
$$ LANGUAGE plpgsql IMMUTABLE;


CREATE AGGREGATE min_distance_from_point(coords, coords) (
    SFUNC = min_coords_distance,
    STYPE = NUMERIC
    );

SELECT min_distance_from_point(location, '(55.500000,37.500000)'::coords) AS min_dist
FROM park_object;