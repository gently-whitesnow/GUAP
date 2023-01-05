#include <iostream>
#include <cmath>

using namespace std;

// Варинат 8
// Характеристикой столбца целочисленной матрицы назовѐм сумму модулей его
// отрицательных нечѐтных элементов. Переставляя столбцы заданной матрицы, расположить их в
// соответствии с ростом характеристик.
// Найти сумму элементов в тех столбцах, которые содержат хотя бы один отрицательный
// элемент.

bool isIntNumber(string s)
{
    if (!s.empty() && s[0] == '-')
        s.erase(s.begin());
    bool b = true;
    for (int i = 0; i < s.size(); i++)
    {
        if (!isdigit(s[i]))
            b = false;
    }
    return b;
}

int getIntValue(string s, bool isNotUint = true)
{
    string a;
    int b;
    while (true)
    {
        cin.clear();
        cout << "For " << s << endl;
        cout << "Enter int value: ";
        cin >> a;
        if (isIntNumber(a) && (stoi(a) > 0 || isNotUint))
        {
            b = stoi(a);
            break;
        }
        cout << "Bad input" << endl;
    }
    cout << endl;
    return b;
}

int main()
{
    // запросить размер матрицы w и h
    unsigned int w = getIntValue("matrix width", false);
    unsigned int h = getIntValue("matrix height", false);

    // Создаем массив указателей - количество строк
    // Также выделим +1 памяти под задачи варианта
    int** matrix = new int*[h+1];
    // запросить значения матрицы
    for (size_t i = 0; i < h; i++)
    {
        // для каждого указателя на строку 
        // выделяем память под массив значений столбца
        matrix[i] = new int[w];
        for (size_t j = 0; j < w; j++)
        {
            matrix[i][j] = getIntValue("column " + to_string(i) + " row " + to_string(j));
        }
    }

    // для каждого столбца посчитать характеристику

    // сортируем

    // печатаем (если не null)

    // считаем 2 задачу (остальные столбцы проставляем в null)

    // печатаем (если не null)
    
    cout << "Your matrix:" << endl;
    for (size_t i = 0; i < h; i++)
    {
        for (size_t j = 0; j < w; j++)
        {
            cout << matrix[i][j];
        }
        cout << endl;
    }

    //Освобождаем указатель каждой строки от ее столбцов
    for(int i = 0; i < w; ++i) {
        delete[] matrix[w];   
    }
    // Освобождаем сами указатели строк
    delete[] matrix;

    return 0; 
}
