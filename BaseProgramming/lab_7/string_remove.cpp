#include <iostream>
#include <cmath>

using namespace std;

// Вариант 8
// Функция удаляет из строки заданное количество символов, начиная с заданной позиции.

int length(const char *array)
{
    int a = 0;
    while (array[a] != '\0')
    {
        a++;
    }
    return a;
}

void removeWithReallocate(char*& input, int &len, int start, int count)
{
    if (start + count > len)
    {
        cout << "Bad input" << endl;
        return;
    }
    int newLen = len - count;
    char *newArray = new char[newLen];
    int j = 0;
    for (size_t i = 0; i < len; i++)
    {
        if (i < start || i >= start + count)
        {
            newArray[j] = input[i];
            j++;
        }
    }
    len = newLen;
    newArray[len] = '\0';
    delete[] input;
    input = newArray;
}

int remove(char* input, int len, int start, int count) {
    int right = start + count;
    int left = start;
    int newLen = len - count; 
    for (; right < len; ++left, ++right) {
        input[left] = input[right];
    }
    input[newLen] = '\0';
    return newLen;
}

char* stringDuplicate(const char* other) {
    int len = length(other) + 1; // for \0
    char* dup = new char[len];
    if (dup) {
        for (size_t i = 0; i < len; i++)
        {
            dup[i] = other[i];
        }
    }
    return dup;
}

void print(const char* input, const int& len)
{
    for (size_t i = 0; i < len; i++)
    {
        cout << input[i];
    }
    cout << endl;
}

int main(int argc, char* argv[])
{
    char *input = stringDuplicate(argv[1]);
    int len = length(input);

    int start = stoi(argv[2]);
    int count = stoi(argv[3]);

    len = remove(input, len, start, count);

    print(input, len);

    delete[] input;
}


