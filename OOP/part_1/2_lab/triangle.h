#pragma once
#include <cmath>
#include <iostream>

using namespace std;

class Triangle {
   public:
    Triangle();

    Triangle(const double& a, const double& b, const double& c);

    Triangle(const Triangle& triangle);

    ~Triangle();

    double* Angles() const;

    void SetSides(const double& a, const double& b, const double& c);

    string ToString() const;

   private:
    double _a;
    double _b;
    double _c;
    const double _pi = 3.1415926535;

    bool IsNotValidTriangle(const double& a, const double& b,
                            const double& c) const;

    double GetAngle(const double& searcheableSide,
                    const double& firstAnotherSide,
                    const double& secondAnotherSide) const;
};

Triangle::Triangle() {
    _a = 1;
    _b = 1;
    _c = 1;
}

Triangle::Triangle(const double& a, const double& b, const double& c) {
    if (IsNotValidTriangle(a, b, c)) return;

    _a = a;
    _b = b;
    _c = c;
}

Triangle::Triangle(const Triangle& triangle) {
    _a = triangle._a;
    _b = triangle._b;
    _c = triangle._c;
}

Triangle::~Triangle() {
    cout << "___ destruct: " << ToString() << " ___" << endl;
}

double* Triangle::Angles() const {
    static double angles[3];
    if (IsNotValidTriangle(_a, _b, _c)) return angles;

    angles[0] = GetAngle(_a, _b, _c);
    angles[1] = GetAngle(_b, _a, _c);
    angles[2] = GetAngle(_c, _a, _b);

    return angles;
}

void Triangle::SetSides(const double& a, const double& b, const double& c) {
    if (IsNotValidTriangle(a, b, c)) return;

    _a = a;
    _b = b;
    _c = c;
}

string Triangle::ToString() const {
    return "a: " + std::to_string(_a) + " b: " + std::to_string(_b) +
           " c: " + std::to_string(_c);
}

bool Triangle::IsNotValidTriangle(const double& a, const double& b,
                                  const double& c) const {
    if (a <= 0 || b <= 0 || c <= 0) {
        std::cerr << "Wrong side, required value > 0 !"
                  << "{" + std::to_string(a) + std::to_string(b) +
                         std::to_string(c) + "}"
                  << std::endl;
        return true;
    }

    if (a + b < c || c + a < b || b + c < a) {
        std::cerr << "The sum of two sides must be larger than the third!"
                  << "{" + std::to_string(a) + ", " + std::to_string(b) + ", " +
                         std::to_string(c) + "}"
                  << std::endl;
        return true;
    }

    return false;
}

double Triangle::GetAngle(const double& searcheableSide,
                          const double& firstAnotherSide,
                          const double& secondAnotherSide) const {
    auto radians = acos((pow(firstAnotherSide, 2) + pow(secondAnotherSide, 2) -
                         pow(searcheableSide, 2)) /
                        (2 * firstAnotherSide * secondAnotherSide));
    return radians * 180 / _pi;
}