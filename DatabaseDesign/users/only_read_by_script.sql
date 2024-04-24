CREATE ROLE username WITH LOGIN PASSWORD 'password';

-- кастомные права
GRANT SELECT ON ALL TABLES IN SCHEMA public TO username;

-- забираются только дефолтные или унаследованные права
REVOKE ALL ON DATABASE current_database() FROM username;

-- кастомные права
GRANT CONNECT ON DATABASE current_database() TO username;