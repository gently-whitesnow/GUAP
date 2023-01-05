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

int main()
{
    cout << "Print two coordinates" << endl;
    double x;
    double y;

    x = getValue("X double");
    y = getValue("Y double");
    

    if ((x >= -1 && x <= 0 && y >= -sqrt(1 - x * x) && y <= sqrt(1 - x * x)) ||
        (x <= 1 && x > 0 && y >= 0 && y <= sqrt(1 - x * x)))
    {
        cout << "True" << endl;
    }
    else
    {
        cout << "False" << endl;
    }
}
// блок схемы
