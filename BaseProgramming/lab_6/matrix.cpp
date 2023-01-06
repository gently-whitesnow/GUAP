#include <iostream>
#include <cmath>
#include <limits>

using namespace std;

// Варинат 8
// Характеристикой столбца целочисленной матрицы назовѐм сумму модулей его
// отрицательных нечѐтных элементов. Переставляя столбцы заданной матрицы, расположить их в
// соответствии с ростом характеристик.
// Найти сумму элементов в тех столбцах, которые содержат хотя бы один отрицательный
// элемент.

static const int INVALID_VALUE = std::numeric_limits<int>::min();

bool isIntNumber(const string &s)
{
    int start = 0;
    if (!s.empty() && s[0] == '-')
        ++start;

    for (int i = start; i < s.size(); i++)
    {
        if (!isdigit(s[i]))
            return false;
    }
    return true;
}

int getIntValue(const string &s, bool isNotUint = true)
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

void print(int **matrix, int h, int w)
{
    cout << "Your matrix:" << endl;
    for (size_t i = 0; i < h; i++)
    {
        for (size_t j = 0; j < w; j++)
        {
            if (matrix[i][j] != INVALID_VALUE)
                cout << matrix[i][j] << "\t";
            else
                cout << "\t";
        }
        cout << endl;
    }
}

void evaluateCharacters(int **matrix, int h, int w)
{
    for (size_t i = 0; i < h; i++)
    {
        for (size_t j = 0; j < w; j++)
        {
            int val = matrix[i][j];
            if (val % 2 != 0 && val < 0)
            {
                matrix[h][j] += abs(val);
            }
        }
    }
}
int partition(int **matrix, int h, int first, int last)
{
    int average = matrix[h][(first + last) / 2];
    int i = first;
    int j = last;
    while (i <= j)
    {
        while (matrix[h][i] < average)
            i++;
        while (matrix[h][j] > average)
            j--;
        if (i >= j)
            break;
        for (int k = 0; k <= h; k++)
        {
            if (matrix[h][i] != matrix[h][j])
                swap(matrix[k][j], matrix[k][i]);
        }
        i++;
        j--;
    }
    return j;
}

void quickSort(int **matrix, int h, int first, int last)
{
    if (first < last)
    {
        int half = partition(matrix, h, first, last);
        quickSort(matrix, h, first, half);
        quickSort(matrix, h, half + 1, last);
    }
}

void sumIfNegative(int **matrix, int h, int w)
{
    for (size_t i = 0; i < w; i++)
    {
        bool haveNegative = false;
        // зануляем коэффициенты после предыдущей задачи
        matrix[h][i] = 0; 
        for (size_t j = 0; j < h; j++)
        {
            int val = matrix[j][i];
            matrix[h][i] += val;
            if (val < 0)
                haveNegative = true;
        }
        if (!haveNegative)
            matrix[h][i] = INVALID_VALUE;
    }
}

int main()
{

    // запросить размер матрицы w и h
    unsigned int w = getIntValue("matrix width", false);
    unsigned int h = getIntValue("matrix height", false);

    // Создаем массив указателей - количество строк
    // Также выделим +1 памяти под задачи варианта
    int **matrix = new int *[h + 1];
    // запросить значения матрицы
    for (size_t i = 0; i < h + 1; i++)
    {
        // для каждого указателя на строку
        // выделяем память под массив значений столбца
        matrix[i] = new int[w];
        for (size_t j = 0; j < w; j++)
        {
            if (i != h)
                matrix[i][j] = getIntValue("column " + to_string(j) + " row " + to_string(i));
            else
                matrix[i][j] = 0;
        }
    }

    // Исходная матрица
    print(matrix, h + 1, w);

    // для каждого столбца посчитать характеристику
    evaluateCharacters(matrix, h, w);
    print(matrix, h + 1, w);

    // сортируем
    quickSort(matrix, h, 0, w - 1);
    print(matrix, h + 1, w);

    // считаем 2 задачу (остальные столбцы проставляем в INVALID_VALUE)
    sumIfNegative(matrix, h, w);
    print(matrix, h + 1, w);

    //Освобождаем указатель каждой строки от ее столбцов
    for (int i = 0; i <= h; ++i)
    {
        delete[] matrix[i];
    }
    // Освобождаем сами указатели строк
    delete[] matrix;

    return 0;
}
