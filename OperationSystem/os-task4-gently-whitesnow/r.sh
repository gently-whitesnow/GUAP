#!/usr/bin/env bash
g++ lab4.cpp -o lab4 -std=c++11

if [ $? -ne 0 ]; then
    echo "Build failed. Exiting..."
    exit 1
fi

# Запуск только если компиляция успешна
echo "Build succeeded. Running the application..."

./lab4 2

# 0 1
# 0 2
# 0 3
# 0 4
# 0 5
# 0 6
# 0 6
# 0 7
# 0 8
# 0 9
# 0 10
# 0 11
# 0 12
