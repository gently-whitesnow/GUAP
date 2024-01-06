#include <iostream>

#include "angle.h"

using namespace std;

int main() {
    Angle angle;

    cout << "valid example:" << endl;
    angle.SetGradus(170);
    angle.SetMin(57);
    angle.SetSec(22);
    cout << angle << endl;
    cout << "radians: " << angle.ToRadians() << endl;

    cout << "try set not valid minutes (140g, 220m, 33s):" << endl;
    angle.SetGradus(140);
    angle.SetMin(220);
    angle.SetSec(33);
    cout << angle << endl;
    cout << "radians: " << angle.ToRadians() << endl;

    cout << "try set not valid sec (330g, 10m, 330s):" << endl;
    angle.SetGradus(330);
    angle.SetMin(10);
    angle.SetSec(330);
    cout << angle << endl;
    cout << "radians: " << angle.ToRadians() << endl;
}