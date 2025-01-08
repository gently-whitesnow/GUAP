#!/usr/bin/env bash

set -Eeuo pipefail
trap cleanup SIGINT SIGTERM ERR EXIT

# Подключение модулей
source ./modules/install.sh
source ./modules/configure.sh
source ./modules/database.sh
source ./modules/schema.sh
source ./modules/role.sh
source ./modules/table.sh

usage() {
    cat <<EOF
Usage: $(basename "${BASH_SOURCE[0]}") [options]

Options:
  -h, --help                 Show this help message and exit.
  -i, --install              Install PostgreSQL.
  -u, --uninstall            Uninstall PostgreSQL completely.
  -c, --config <path> <db-name>        Configure pg_hba.conf with the specified file path.
  -d, --db-name <name>       Create a database with the specified name.
  -b, --backup <db> <path>   Backup the specified database to the given path.
  -s, --schema <db> <schema> Create a schema in the specified database.
  -r, --role <name> <pwd> <db> 
                             Create a role with the specified name, password, and associate it with the database.
  -a, --assign <role> <db>   Assign permissions to the specified role in the given database.
  -t, --table <db> <schema> <table> <columns>
                             Create a table in the specified database and schema.
                             <columns> should be provided as column_name column_type (e.g., id SERIAL, name VARCHAR(255)).

Examples:
  $(basename "${BASH_SOURCE[0]}") --install
  $(basename "${BASH_SOURCE[0]}") --config ./pg_hba.default.conf my_database
  $(basename "${BASH_SOURCE[0]}") --db-name my_database
  $(basename "${BASH_SOURCE[0]}") --schema my_database public
  $(basename "${BASH_SOURCE[0]}") --table my_database public my_table 'id SERIAL, name VARCHAR(255)'
EOF
    exit 0
}

cleanup() {
    trap - SIGINT SIGTERM ERR EXIT
    # Cleanup actions
}

parse_params() {
    install_flag=false
    uninstall_flag=false
    config_path=''
    db_name=''
    has_create_db_flag=false
    backup_path=''
    schema_name=''
    has_role_flag=false
    role_name=''
    role_password=''
    has_assign_flag=false
    has_table_flag=false
    table_name=''
    table_columns=''

    while :; do
        case "${1-}" in
        -h | --help) usage ;;
        -i | --install) install_flag=true ;;
        -u | --uninstall) uninstall_flag=true ;;
        -c | --config)
            config_path="${2-}"
            db_name="${3-}"
            shift 2
            ;;
        -d | --db-name)
            has_create_db_flag=true
            db_name="${2-}"
            shift
            ;;
        -b | --backup)
            db_name="${2-}"
            backup_path="${3-}"
            shift 2
            ;;
        -s | --schema)
            db_name="${2-}"
            schema_name="${3-}"
            shift 2
            ;;
        -r | --role)
            has_role_flag=true
            role_name="${2-}"
            role_password="${3-}"
            db_name="${4-}"
            shift 3
            ;;
        -a | --assign)
            has_assign_flag=true
            role_name="${2-}"
            db_name="${3-}"
            shift 2
            ;;
        -t | --table)
            has_table_flag=true
            db_name="${2-}"
            schema_name="${3-}"
            table_name="${4-}"
            table_columns="${5-}"
            shift 4
            ;;
        -?*)
            echo "Unknown option: $1" >&2
            exit 1
            ;;
        *) break ;;
        esac
        shift
    done
}

main() {
    parse_params "$@"

    if [[ $install_flag == true ]]; then
        install_postgresql
    fi

    if [[ $uninstall_flag == true ]]; then
        uninstall_postgresql
    fi

    if [[ -n "$config_path" && -n "$db_name" ]]; then
        configure_pg_hba "$config_path" "$db_name"
    fi

    if [[ $has_create_db_flag == true && -n "$db_name" ]]; then
        create_database "$db_name"
    fi

    if [[ -n "$db_name" && -n "$backup_path" ]]; then
        backup_database "$db_name" "$backup_path"
    fi

    if [[ -n "$db_name" && -n "$schema_name" ]]; then
        create_schema "$db_name" "$schema_name"
    fi

    if [[ $has_role_flag == true && -n "$role_name" && -n "$role_password" && -n "$db_name" ]]; then
        create_role "$role_name" "$role_password" "$db_name"
    fi

    if [[ $has_assign_flag == true && -n "$role_name" && -n "$db_name" ]]; then
        assign_permissions "$role_name" "$db_name"
    fi

    if [[ $has_table_flag == true && -n "$db_name" && -n "$schema_name" && -n "$table_name" &&
        -n "$table_columns" ]]; then
        create_table "$db_name" "$schema_name" "$table_name" "$table_columns"
    fi

    if [[ $install_flag == false && $uninstall_flag == false && -z "$config_path" && -z "$db_name" &&
        -z "$schema_name" && -z "$role_name" ]]; then
        usage
    fi
}

main "$@"
