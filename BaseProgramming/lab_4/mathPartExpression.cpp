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

string evaluate(const double &a, const double &c, const double &x)
{
    string ans;
    if (c < 0 && a != 0)
    {
        ans = std::to_string(-a * x * x);
    }
    else if (c > 0 && a == 0)
    {
        ans = std::to_string((a - x) / c / x);
    }
    else
    {
        double e = x / c;
        if (!isinf(e))
            ans = std::to_string(x / c);
        else
            ans = "Zero division";
    }
    return ans;
}

int main()
{
    double a, c, xStart, xEnd, dx;
    a = getValue("a");
    c = getValue("c");
    xStart = getValue("xStart");
    xEnd = xStart - 1;
    while (xEnd <= xStart)
    {
        xEnd = getValue("xEnd should be more xStart");
    }
    dx = 0;
    while (dx <= 0)
    {
        dx = getValue("dx should be more 0");
    }

    cout << "___________________" << endl;
    for (double x = xStart; x <= xEnd; x += dx)
    {
        cout << x << "\t";

        cout << evaluate(a, c, x);

        cout << endl;
    }
}

// dx >= 0
//  xmin < xmax
//вычисления в отдельную функцию
//  деление на ноль вместо inf
