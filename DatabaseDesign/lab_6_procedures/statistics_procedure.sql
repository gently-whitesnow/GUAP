CREATE OR REPLACE PROCEDURE insert_history_with_not_exist_patient(
    inout count_of_procedures int,
    inout total_price numeric,
    inout avg_price numeric,
    inout min_price numeric,
    inout max_price numeric
)
AS
$$
BEGIN
    -- количество процедур
    count_of_procedures := (select count(id) from procedure_history);
    
    -- сколько заработала клиника
    total_price := (select sum(price) from procedure_history);
    
    -- сколько в среднем приносит процедура
    avg_price := (select avg(price) from procedure_history);
    
    -- самая дешевая процедура
    min_price := (select min(price) from procedure_history);
    
    -- самая дорогая процедура
    max_price := (select max(price) from procedure_history);

END;
$$ LANGUAGE plpgsql;
   
-- calling
-- DO $$
--     DECLARE
--         count_of_procedures int;
--         total_price numeric;
--         avg_price numeric;
--         min_price numeric;
--         max_price numeric;
--     BEGIN
--         CALL insert_history_with_not_exist_patient(
--                 count_of_procedures,
--                 total_price,
--                 avg_price,
--                 min_price,
--                 max_price
--             );
-- 
--         RAISE NOTICE 'count: %, total: %, avg: %, min: %, max: %,', count_of_procedures, total_price, avg_price, min_price, max_price;
--     END $$;