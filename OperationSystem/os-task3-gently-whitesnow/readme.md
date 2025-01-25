### использование wine

установка

```bash
brew install --cask wine-stable
brew install mingw-w64
```

компиляция

```bash
x86_64-w64-mingw32-g++ -o app.exe lab3.cpp main.cpp -static -static-libgcc -static-libstdc++
```

запуск

```bash
wine app.exe
```
