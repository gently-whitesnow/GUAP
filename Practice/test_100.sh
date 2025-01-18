#!/bin/bash

# Проверяем наличие всех аргументов
if [ "$#" -ne 2 ]; then
    echo "Использование: ./benchmark.sh <путь_к_папке> <поисковый_запрос>"
    exit 1
fi

FOLDER=$1
QUERY=$2
INDEX_FILE="index.json"
REPEATS=100 # Количество повторений

# Создаем индекс, если он еще не существует
if [ ! -f "$INDEX_FILE" ]; then
    echo "Индекс не найден, создаем новый..."
    python3 indexer.py "$FOLDER"
    if [ $? -ne 0 ]; then
        echo "Ошибка при создании индекса"
        exit 1
    fi
fi

echo "Сравнение скорости grep и searcher.py для запроса: \"$QUERY\""
echo "Количество повторений для замера: $REPEATS"

# Тестируем grep
echo "Тестирование grep..."
GREP_TOTAL=0
for i in $(seq 1 $REPEATS); do
    START_GREP=$(date +%s.%N)
    grep -rn -m 10 "$QUERY" "$FOLDER" >/dev/null
    END_GREP=$(date +%s.%N)
    GREP_TOTAL=$(echo "$GREP_TOTAL + $END_GREP - $START_GREP" | bc -l)
done
GREP_TIME=$(echo "$GREP_TOTAL / $REPEATS" | bc -l)

# Тестируем searcher.py
echo "Тестирование searcher.py..."
SEARCH_TOTAL=0
for i in $(seq 1 $REPEATS); do
    START_SEARCH=$(date +%s.%N)
    python3 searcher.py "$QUERY" >/dev/null
    END_SEARCH=$(date +%s.%N)
    SEARCH_TOTAL=$(echo "$SEARCH_TOTAL + $END_SEARCH - $START_SEARCH" | bc -l)
done
SEARCH_TIME=$(echo "$SEARCH_TOTAL / $REPEATS" | bc -l)

# Вывод результатов
echo "Результаты (среднее время за $REPEATS повторений):"
printf "Grep: %.10f секунд\n" "$GREP_TIME"
printf "Searcher.py: %.10f секунд\n" "$SEARCH_TIME"
