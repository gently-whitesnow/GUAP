#include "array.h"

void step(const std::string& description) {
    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << "=== " << description << " ===" << std::endl;
    std::cout << std::endl;
}

int main() {
    step("try create overflow array");
    int overflowArray[] = {1, 2, 3, 4, -1, 2, 3, 4, 5, -6, 0, 2, 0, 1, 2};
    auto len1 = sizeof(overflowArray) / sizeof(int);
    Array array1(overflowArray, len1);

    step("try create array with not valid length");
    int invalidLenArray[] = {1, 2, 3, 4, -1, 2, 3, 4, 5, -6, 0, 2, 0};
    Array array2(invalidLenArray, -1);

    step("Create array");
    int initArray[] = {1, 2, 3, 4, -1, 2, 3, 4, 5, -6, 0, 2, 0};
    auto len = sizeof(initArray) / sizeof(int);
    Array array(initArray, len);
    array.Print();

    step("Sum between first negative values");
    std::cout << array.SumBetweenFirstNegativeValues() << std::endl;

    step("Transform");
    array.NormalizeTransform();
}
