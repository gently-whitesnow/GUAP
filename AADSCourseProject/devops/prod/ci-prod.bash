#!/bin/bash

git -C ./GUAP pull

# build api image
docker build -t "howto-api" ./GUAP/AADSCourseProject/back/HowTo

# Перекладываем статику в монтируемое nginx место
sudo rm -rf /usr/share/nginx/html/*
sudo mkdir -p /usr/share/nginx/html
sudo cp -rf ~/build/* /usr/share/nginx/html
# Перекладываем конфигурационный файл для nginx
sudo rm -rf /etc/nginx/*
sudo mkdir -p /etc/nginx
sudo cp -rf ~/nginx.conf /etc/nginx

# Запускаем сервис (перезапускаем в фоновом режиме)
docker-compose down
docker-compose up -d

