
-- количество процедур
select count(id) from procedures_history;

-- сколько заработала клиника
select sum(price) from procedures_history;

-- сколько в среднем приносит процедура
select avg(price) from procedures_history;

-- самая дешевая процедура
select min(price) from procedures_history;

-- самая дорогая процедура
select max(price) from procedures_history;