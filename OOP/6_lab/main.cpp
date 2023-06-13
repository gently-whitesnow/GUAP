#include "random_data.h"

void step(const std::string& description) {
    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << "=== " << description << " ===" << std::endl;
    std::cout << std::endl;
}

int main() {
    step("Create not valid len array");
    RandomData data1(20, 20, -1);
    data1.Print();

    step("Create not valid random range array");
    RandomData data2(20, -20, 10);
    data2.Print();

    step("Create normal array");
    RandomData data(-15, 15, 10);
    data.Print();

    step("After reverse");
    data.Reverse();
    data.Print();

    step("After pow transform");
    data.PowTransform();
    data.Print();

    step("Not zero count");
    std::cout << data.NotZeroCount() << std::endl;
}

