#include <iostream>
#include <cmath>

using namespace std;

bool isUIntNumber(const string &s)
{
    bool b = true;
    for (int i = 0; i < s.size(); i++)
    {
        if (!isdigit(s[i]))
            b =  false;
    }
    return b;
}

unsigned int getPositiveUIntValue()
{
    string a;
    unsigned int b;
    while (true)
    {
        cin.clear();
        cout << "Enter a positive int value: ";
        cin >> a;
        if (isUIntNumber(a) && stoi(a) > 0)
        {
            b = stoi(a);
            break;
        }
        cout << "Bad input" << endl;
    }
    return b;
}

bool isDoubleValue(string s)
{
    bool isDV = true;
    if (!s.empty() && s[0] == '-')
        s.erase(s.begin());
    if (s.find('.') != string::npos && s.find('.') != s.size() - 1)
        s.erase(s.find('.'));
    if (s.empty())
        isDV = false;
    for (int i = 0;i < s.size(); i++)
    {
        if (!isdigit(s[i]))
            isDV = false;
    }
    return isDV;
}

double getValue(const string &what)
{
    string a;
    double b;
    while (true)
    {
        cin.clear();
        cout << "Enter " << what << " value " << endl;
        cin >> a;
        if (isDoubleValue(a))
        {
            b = stod(a);
            break;
        }
        cout << "Bad input" << endl;
    }
    return b;
}

unsigned int indexOfMinElement(const double *array, unsigned int n)
{
    unsigned int result = 0;
    for (unsigned int i = 0; i < n; i++)
    {
        if (array[i] < array[result])
            result = i;
    }
    return result;
}

double sumBetweenFirstAndSecondNegativeElement(const double *array, unsigned int n)
{
    unsigned int first = 0;
    unsigned int second = 0;
    for (unsigned int i = 0; i < n; i++)
    {
        if (array[i] < 0)
        {
            first = i;
            break;
        }
    }
    for (unsigned int i = first + 1; i < n; i++)
    {
        if (array[i] < 0)
        {
            second = i;
            break;
        }
    }
    double sum = 0.0;
    for (unsigned int i = first + 1; i < second; i++)
    {
        sum += array[i];
    }

    return sum;
}

void sortABS(double *&array, unsigned int n)
{
    double *oldArray = array;
    array = new double[n];
    unsigned int next = 0;
    for (unsigned int i = 0; i < n; i++)
    {
        if (abs(oldArray[i]) <= 1.0)
        {
            array[next] = oldArray[i];
            ++next;
        }
    }
    for (unsigned int i = 0; i < n; i++)
    {
        if (abs(oldArray[i]) > 1.0)
        {
            array[next] = oldArray[i];
            ++next;
        }
    }
    delete[] oldArray;
}

int main()
{
    unsigned int n = getPositiveUIntValue();

    double *array = new double[n];

    for (unsigned int i = 0; i < n; i++)
    {
        array[i] = getValue(to_string(i));
    }

    cout << "Index of min element: " << indexOfMinElement(array, n) << endl;
    cout << "Sum elements beetwen first and second negative element: " 
         << sumBetweenFirstAndSecondNegativeElement(array, n) << endl;
    sortABS(array, n);
    cout << "Sorted array: ";
    for (unsigned int i = 0; i < n; i++)
    {
        cout << array[i] << " ";
    }
    cout << endl;

    delete [] array;
}


//Leaks в следующем семестре 