#!/usr/bin/env bash

create_table() {
    local db_name=$1
    local schema_name=$2
    local table_name=$3
    local table_columns=$4

    local sql="CREATE TABLE IF NOT EXISTS \"$schema_name\".\"$table_name\" ($table_columns);"

    psql -d "$db_name" -c "$sql"

    echo "Table \"$table_name\" created successfully in schema \"$schema_name\" of database \"$db_name\"."
}
