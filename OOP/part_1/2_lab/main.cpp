#include "triangle.h"

using namespace std;

void step(const string &description)
{
    cout << endl;
    cout << "=== " << description << " ===" << endl;
    cout << endl;
}

int main()
{
    step("Create triangle1 (6,8,10)");
    Triangle triangle1(6, 8, 10);
    cout << "triangle1: " << triangle1.ToString() << endl;

    auto angels = triangle1.Angles();
    std::cout << "angle A: " << std::to_string(angels[0]) << std::endl;
    std::cout << "angle B: " << std::to_string(angels[1]) << std::endl;
    std::cout << "angle C: " << std::to_string(angels[2]) << std::endl;

    step("Copy to triangle2 and change triangle1 to (3,4,5)");
    Triangle triangle2(triangle1);
    triangle1.SetSides(3, 4, 5);
    cout << "triangle1: " << triangle1.ToString() << endl;
    cout << "triangle2: " << triangle2.ToString() << endl;
    angels = triangle1.Angles();
    std::cout << "triangle1 angle A: " << std::to_string(angels[0]) << std::endl;
    std::cout << "triangle1 angle B: " << std::to_string(angels[1]) << std::endl;
    std::cout << "triangle1 angle C: " << std::to_string(angels[2]) << std::endl;

    step("Create triangle3 with default");
    Triangle triangle3;
    cout << "triangle3 default: " << triangle3.ToString() << endl;
    angels = triangle3.Angles();
    std::cout << "triangle3 angle A: " << std::to_string(angels[0]) << std::endl;
    std::cout << "triangle3 angle B: " << std::to_string(angels[1]) << std::endl;
    std::cout << "triangle3 angle C: " << std::to_string(angels[2]) << std::endl;
}
