#include <iostream>
#include <cmath>

using namespace std;

int main()
{
    int a;
    int b;
    cout << "Print your list position:" << endl;
    cin >> a;
    cout << "Print amount variants:" << endl;
    cin >> b;
    int c= (a-1) % b +1;
    cout << c << endl;
}
