#include "complex.h"

using namespace std;

void step(const string& description) {
    cout << endl;
    cout << endl;
    cout << "=== " << description << " ===" << endl;
    cout << endl;
}

int main() {
    step("Create complex number from trigonometric and algebraic form");
    Complex complexByTrigonometric(
        Complex::Trigonometric{.radius = 10, .angle = 10});
    Complex complexByAlgebraic(Complex::Algebraic{.real = 10, .image = 10});
    cout << "complexByTrigonometric: " << complexByTrigonometric << endl;
    cout << "complexByAlgebraic: " << complexByAlgebraic << endl;

    step("Sum thix complex numbers");
    Complex complexSum = complexByTrigonometric + complexByAlgebraic;
    cout << "complexSum: " << complexSum << endl;

    step("Difference this complex numbers");
    Complex complexDifference = complexByTrigonometric - complexByAlgebraic;
    cout << "complexDifference: " << complexDifference << endl;

    step("Multiplication this complex numbers");
    Complex complexMultiplication = complexByTrigonometric * complexByAlgebraic;
    cout << "complexMultiplication: " << complexMultiplication << endl;

    step("Division this complex numbers");
    Complex complexDivision = complexByTrigonometric / complexByAlgebraic;
    cout << "complexDivision: " << complexDivision << endl;
}
