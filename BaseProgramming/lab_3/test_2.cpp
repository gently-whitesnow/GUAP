#include <iostream>
#include <cmath>

using namespace std;


int main()
{

    
    unsigned short z ;
    cin >> hex >> z;

    short P = z & 1;
    short C = z >> 2 & int((pow(2, 9) - 1));
    short E = z >> 11 & 1;
    short N = z >> 12 & int((pow(2, 4) - 1));

    cout << dec << "P=" << P << endl;
    cout << dec << "C=" << C << endl;
    cout << dec << "E=" << E << endl;
    cout << dec << "N=" << N << endl;
}


