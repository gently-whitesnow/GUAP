#!/usr/bin/env bash

install_postgresql() {
    if command -v psql &>/dev/null; then
        echo "PostgreSQL is already installed. Version:"
        psql --version
    else
        echo "Installing PostgreSQL via Homebrew..."
        brew update
        brew install postgresql
        echo "Creating necessary directories..."
        sudo mkdir -p /usr/local/var/postgres
        sudo chown -R $(whoami) /usr/local/var
        echo "Initializing PostgreSQL..."
        initdb /usr/local/var/postgres
        brew services start postgresql
        echo "PostgreSQL installed and started."
    fi
}

uninstall_postgresql() {
    if command -v psql &>/dev/null; then
        echo "Stopping PostgreSQL service..."
        brew services stop postgresql

        echo "Uninstalling PostgreSQL..."
        brew uninstall postgresql

        echo "Removing PostgreSQL data and configuration..."
        rm -rf /usr/local/var/postgres

        echo "PostgreSQL has been completely removed."
    else
        echo "PostgreSQL is not installed. Nothing to uninstall."
    fi
}
