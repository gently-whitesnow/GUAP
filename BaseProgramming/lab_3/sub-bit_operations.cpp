#include <iostream>
#include <cmath>
#include <bitset>

#include <string>

using namespace std;

bool isPositiveIntValue(string s)
{
    bool b = true;
    for (int i = 0; i < s.size(); i++)
    {
        if (!isdigit(s[i]))
            b =  false;
    }
    return b;
}

int getValue(int b, char what)
{
    string a;
    int i;
    while (true)
    {
        cin.clear();
        cout << "Enter a positive "<< what <<" value less than " << b << endl;
        cin >> a;
        if (isPositiveIntValue(a) && stoi(a) >= 0 && stoi(a) < b)
            {
                i =  stoi(a);
                break;
            }
        cout << "Bad input" << endl;
    }

    return i;
}

int main()
{
    int X = getValue(int(pow(2, 1)),'X');
    int E = getValue(int(pow(2, 1)),'E');
    int W = getValue(int(pow(2, 1)),'W');
    int R = getValue(int(pow(2, 1)),'R');
    int U = getValue(int(pow(2, 8)),'U');

    unsigned short res = X;

    //поразрядная дизъюнкция сложение

    res = res | E << 5;

    res = res | W << 6;

    res = res | R << 7;

    res = res | U << 8;

    bitset<16> y(res);

    cout << hex << res << endl; // вывод числа в шестнадцатеричном виде
    cout << y << endl;          // вывод числа в двоичном
}


