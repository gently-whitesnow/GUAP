#include <iostream>
#include <cmath>

using namespace std;
int getValue(int b);

// Всего 30 вариантов
// (𝑛𝑛 − 1) 𝑚𝑜𝑑 𝐾 + 1, nn - номер студента = 8
// я 3
int main()
{
    int P = getValue(int(pow(2, 1)));
    int C = getValue(int(pow(2, 9)));
    int E = getValue(int(pow(2, 1)));
    int N = getValue(int(pow(2, 4)));

    unsigned short x = P;
    x = x | C << 2;

    x = x | E << 11;

    x = x | N << 12;

    cout << hex << x << endl; // вывод числа в шестнадцатеричном виде
}

int getValue(int b)
{
    int a = -1;
    while (true)
    {
        cout << "Enter a positive value less than " << b << endl;
        cin >> a;
        if (a >= 0 && a < b)
            return a;
    }
}

// Слово состояния канала в вычислительной системе представляется в виде:
// № разряда 15 14 13 12 11 10 09 08 07 06 05 04 03 02 01 00
// Значение N N N N E C C C C C C C C C 0 P
// где 𝑁 … 𝑁 – номер канала,𝐸 – признак ошибки, 𝐶 … 𝐶– код причини прерывания, 𝑃– признак
// завершения программы в канале.
// int P = getValue(int(pow(2, 1)));
//     int C = getValue(int(pow(2, 9)));
//     int E = getValue(int(pow(2, 1)));
//     int N = getValue(int(pow(2, 4)));

//     unsigned short x = P;
//     bitset<16> y(x);
//     cout << y << endl; // вывод числа в двоичном виде

//     x = x | C << 2 ;
//     y = x;
//     cout << y << endl; // вывод числа в двоичном виде

//     x = x | E << 11 ;
//     y = x;
//     cout << y << endl; // вывод числа в двоичном виде

//     x = x | N << 12;
//     y = x;
//     cout << hex << x << endl; // вывод числа в шестнадцатеричном виде
//     cout << dec << x << endl; // вывод числа в десятичном виде
//     cout << y << endl; // вывод числа в двоичном виде