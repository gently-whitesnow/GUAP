#!/usr/bin/env bash
x86_64-w64-mingw32-g++ -o app.exe lab3.cpp main.cpp -static -static-libgcc -static-libstdc++

if [ $? -ne 0 ]; then
    echo "Build failed. Exiting..."
    exit 1
fi

# Запуск только если компиляция успешна
echo "Build succeeded. Running the application..."

wine app.exe
