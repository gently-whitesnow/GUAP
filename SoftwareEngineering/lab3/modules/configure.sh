#!/usr/bin/env bash

configure_pg_hba() {
    local config_path=$1
    local db_name=$2
    local actual_hba_path
    actual_hba_path=$(psql -d $db_name -tAc "SHOW hba_file")

    echo "Configuring pg_hba.conf using $config_path..."
    cp "$config_path" "$actual_hba_path"
    brew services restart postgresql
    echo "Configuration applied to $actual_hba_path."
}
