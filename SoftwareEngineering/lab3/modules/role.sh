#!/usr/bin/env bash

create_role() {
    local role_name=$1
    local role_password=$2
    local db_name=$3

    if psql -d "$db_name" -tAc "SELECT 1 FROM pg_roles WHERE rolname='$role_name'" | grep -q 1; then
        echo "Role '$role_name' already exists in database '$db_name'. Skipping creation."
    else
        echo "Creating role '$role_name' in database '$db_name'..."
        psql -d "$db_name" -c "CREATE ROLE $role_name WITH LOGIN PASSWORD '$role_password';"
        echo "Role '$role_name' created in database '$db_name'."
    fi
}

assign_permissions() {
    local role_name=$1
    local db_name=$2
    echo "Assigning permissions to role '$role_name' in db $db_name ..."
    psql -d "$db_name" -c "GRANT ALL PRIVILEGES ON DATABASE $db_name TO $role_name;"
    echo "Permissions assigned."
}
