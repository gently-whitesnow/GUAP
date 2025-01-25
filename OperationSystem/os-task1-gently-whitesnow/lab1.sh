#!/usr/bin/env bash

# edit the code below and add your code
# отредактируйте код ниже и добавьте свой
awk '{print $4;}' dns-tunneling.log | sort | sed -n '1p;$p' | while read time; do
  seconds=${time%.*}  # Целая часть (секунды)
  micros=${time#*.}   # Дробная часть (микросекунды)
  nanos=$(printf "%-9s" "${micros}000" | cut -c1-9) # Преобразуем микросекунды в наносекунды
  formatted=$(date -u -d "@${seconds}" +"%Y-%m-%d %H:%M:%S") # Преобразование в UTC
  echo "${formatted}.${nanos}"
done > results.txt

# Переменная с номером варианта (константа):
TASKID=5

# Дополнительные переменные (должны вычисляться динамически):
VAR_1=$(wc -l < dns-tunneling.log)  # Количество строк в файле
VAR_2=$(awk '{print $4,$5;}' dns-tunneling.log | sort | awk '{print $2;}' | awk 'NR==1 {ip=$1} $1==ip {count++} END {print count}')  # Количество повторений первого IP

