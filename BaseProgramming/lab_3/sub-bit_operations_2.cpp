#include <iostream>
#include <cmath>

using namespace std;

unsigned short getValue(int b)
{
    unsigned int a;    
    while (true)
    {
        cin.clear();
        
        cout << "Enter a positive 16-value less than 0x" << hex << b << endl;
        cin >> hex >> a;
        if (a >= 0 && a < b)
            break;
        cout << "Bad input" << endl;
    }
    return a;
}

int main()
{

    unsigned short z = getValue(1 << 16);

    // поразрядная конъюнкция - поразрядное умножение
    int X = z & 1;
    int E = z >> 5 & 1;
    int W = z >> 6 & 1;
    int R = z >> 7 & 1;

    int U = z >> 8 & ((1 << 8) - 1);

    cout << dec << "X=" << X << endl;
    cout << dec << "E=" << E << endl;
    cout << dec << "W=" << W << endl;
    cout << dec << "R=" << R << endl;
    cout << dec << "U=" << U << endl;
}


