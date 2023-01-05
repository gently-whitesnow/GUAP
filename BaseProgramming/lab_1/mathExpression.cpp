#include <iostream>
#include <cmath>

using namespace std;

bool isDoubleValue(string s)
{
    bool isDV = true;
    if (!s.empty() && s[0] == '-')
        s.erase(s.begin());
    if (s.find('.') != string::npos && s.find('.') != s.size() - 1)
        s.erase(s.find('.'));
    if (s.empty())
        isDV = false;
    for (int i = 0; i < s.size(); i++)
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

// 8 вариант
int main()
{
    double x, y;
    double pi = atan(1.0) * 4;
    x = getValue("X double");
    y = getValue("Y double");

    x = x * pi / 180;
    y = y * pi / 180;
    double ansA = pow(cos(x), 4) + pow(sin(y), 2) + 1.0 / 4.0 * pow(sin(2 * x), 2) - 1;
    double ansB = sin(y + x) * sin(y - x);

    if (ansA == ansB)
        cout << "true" << endl;
    else
        cout << "false" << endl;

    cout << "Z1 = " << ansA << endl
         << "Z2 = " << ansB << endl;
}

// по методичке отчет