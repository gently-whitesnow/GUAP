#include <iostream>
#include <cmath>
#include <fstream>

using namespace std;

// Вариант 8
// Написать программу, которая считывает текст из файла и записывает в другой файл только
// цитаты (предложения, заключѐнные в кавычки).

// 1. С использованием библиотек потокового ввода-вывода (<fstream>).
// 2. С использованием библиотек стандартного ввода-вывода (<cstdio>)

string readQuotes(ifstream& inputFile){
    string quotesLine;
    bool startQuote = false;
    char c;
    string tempBuffer;
    while (inputFile.get(c))
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
    cout<<"fstream"<<endl;
    // класс предоставляет возможности для чтения файлов.
    ifstream inputFile;
    inputFile.open("input.txt");

    if (!inputFile.is_open())
    {
        cerr << "Ошибка открытия файла на чтение" << std::endl;
        return 1;
    }

    string quotesLine = readQuotes(inputFile);
    
    if (quotesLine.length() != 0)
    {
        // для записи в файл
        ofstream outputFile;
        outputFile.open("output_fstream.txt");
        if (!outputFile.is_open())
        {
            cerr << "Ошибка открытия файла на запись" << std::endl;
        }
        else
        {
            outputFile << quotesLine << endl;
            outputFile.close();
        }
    }
    else
    {
        cout << "Цитат не обнаружено" << endl;
    }

    inputFile.close();

    return 0;
}
