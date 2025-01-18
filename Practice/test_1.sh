#!/bin/bash

# Проверяем наличие всех аргументов
if [ "$#" -ne 2 ]; then
    echo "Использование: ./benchmark.sh <путь_к_папке> <поисковый_запрос>"
    exit 1
fi

FOLDER=$1
QUERY=$2
INDEX_FILE="index.json"

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

# Тестируем grep
echo "Тестирование grep..."
START_GREP=$(date +%s.%N)
grep -rn -m 10 "$QUERY" "$FOLDER" >/dev/null
END_GREP=$(date +%s.%N)
GREP_TIME=$(echo "$END_GREP - $START_GREP" | bc -l)

# Тестируем searcher.py
echo "Тестирование searcher.py..."
START_SEARCH=$(date +%s.%N)
python3 searcher.py "$QUERY" >/dev/null
END_SEARCH=$(date +%s.%N)
SEARCH_TIME=$(echo "$END_SEARCH - $START_SEARCH" | bc -l)

# Вывод результатов
echo "Результаты:"
printf "Grep: %.6f секунд\n" "$GREP_TIME"
printf "Searcher.py: %.6f секунд\n" "$SEARCH_TIME"
