-- создаем юзера
CREATE ROLE username WITH LOGIN PASSWORD 'password';

-- забираем все права
REVOKE ALL ON DATABASE current_database() FROM username;
REVOKE ALL ON ALL TABLES IN SCHEMA public FROM username;

-- выдаем права только на вызов процедур
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO username;

-- даем права на подключение к бд
GRANT CONNECT ON DATABASE current_database() TO username;
