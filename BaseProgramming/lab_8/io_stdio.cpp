#include <iostream>

using namespace std;

// Вариант 8
// Написать программу, которая считывает текст из файла и записывает в другой файл только
// цитаты (предложения, заключѐнные в кавычки).

// 1. С использованием библиотек потокового ввода-вывода (<fstream>).
// 2. С использованием библиотек стандартного ввода-вывода (<cstdio>)

string readQuotes(FILE *inputFile){
    string quotesLine;
    bool startQuote = false;
    char c;
    string tempBuffer;
    while ((c = (char)fgetc(inputFile)) != EOF)
    {
        if (c == '"')
        {
            startQuote = !startQuote;
        }
        else if (startQuote)
            tempBuffer += c;

        if (tempBuffer.length() > 0 && !startQuote)
        {
            quotesLine += tempBuffer + "\n";
            tempBuffer = "";
        }
    }
    return quotesLine;
}

int main()
{
    cout<<"stdio"<<endl;
    FILE *inputFile = fopen("input.txt", "r");

    if (inputFile == NULL)
    {
        cerr << "Ошибка открытия файла на чтение" << std::endl;
        return 1;
    }

    string quotesLine = readQuotes(inputFile);

    if (quotesLine.length() != 0)
    {
        // для записи в файл
        FILE *outputFile = fopen("output_stdio.txt", "w");
        if (outputFile == NULL)
        {
            cerr << "Ошибка открытия файла на запись" << std::endl;
        }
        else
        {
            const char* data = quotesLine.c_str();
            fwrite(data, sizeof(char), quotesLine.length(), outputFile);
            fclose(outputFile);
        }
    }
    else
    {
        cout << "Цитат не обнаружено" << endl;
    }

    fclose(inputFile);

    return 0;
}
