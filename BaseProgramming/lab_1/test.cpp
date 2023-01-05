#include <iostream>
#include <cmath>

using namespace std;

// 5 вариант (8 в журнале)
int main()
{
    double a, b;
    double pi = atan(1.0) * 4;
    cout << "Print two numbers:" << endl;
    cin >> a >> b;
    a = a * pi / 180;
    b = b * pi / 180;
    double ansA = 1 - 1 / 4 * pow(sin(2 * a), 2) + cos(2 * a);
    double ansB = pow(cos(b), 2) + pow(cos(b), 4);
    cout << ansA << endl
         << ansB << endl;
}


