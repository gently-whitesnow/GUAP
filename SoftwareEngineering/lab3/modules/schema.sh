#!/usr/bin/env bash

create_schema() {
    local db_name=$1
    local schema=$2
    echo "Creating schema '$schema' in database '$db_name'..."
    psql -d "$db_name" -c "CREATE SCHEMA IF NOT EXISTS $schema;"
    echo "Schema '$schema' created."
}
