#!/bin/bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

# Билдим backend
# docker build -t "howto-api" $SCRIPT_DIR/../../back/HowTo
# Билдим статику
# npm run build --prefix $SCRIPT_DIR/../../front/how-to-ui

# Запускаем сервис (перезапускаем в фоновом режиме)
docker-compose up --force-recreate -d
docker image prune -f
