#!/usr/bin/env bash

create_database() {
    local db_name=$1
    if psql -lqt | cut -d\| -f 1 | grep -qw "$db_name"; then
        echo "Database '$db_name' already exists. Skipping creation."
    else
        echo "Creating database '$db_name'..."
        createdb "$db_name"
        echo "Database '$db_name' created."
    fi
}

backup_database() {
    local db_name=$1
    local backup_path=$2
    echo "Backing up database '$db_name' to '$backup_path'..."
    pg_dump "$db_name" >"$backup_path"
    echo "Backup completed."
}
