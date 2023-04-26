#include <iostream>
#include <utility>
#include "angle.h"

using namespace std;

int main()
{
    Angle angle;

    cout << "valid example:" << endl;
    angle.SetGradus(170);
    angle.SetMin(57);
    angle.SetSec(22);
    cout << angle << endl;
    cout << angle.ToRadians() << endl;

    cout << "try set not valid minutes:" << endl;
    angle.SetGradus(140);
    angle.SetMin(220);
    angle.SetSec(33);
    cout << angle << endl;
    cout << angle.ToRadians() << endl;

    cout << "try set not valid sec:" << endl;
    angle.SetGradus(330);
    angle.SetMin(10);
    angle.SetSec(330);
    cout << angle << endl;
    cout << angle.ToRadians() << endl;
}