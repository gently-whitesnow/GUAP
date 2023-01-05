#include <iostream>
#include <cmath>

using namespace std;

// Всего 30 вариантов
// (𝑛𝑛 − 1) 𝑚𝑜𝑑 𝐾 + 1, nn - номер студента
// я 3
int main()
{
    double x, y;
    cout << "Print two coordinates" << endl;

    cout << "x: ";
    cin >> x;
    cout << "y: ";
    cin >> y;

    if (x >= 0 && x <= 1 && y >= -1 && y <= 1 ||
        x >= -1 && x < 0 && y >= -sqrt(1 - x * x) && y <= sqrt(1 - x * x))
    {
        cout << "True" << endl;
    }
    else
    {
        cout << "False" << endl;
    }
}
