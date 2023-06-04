#include "medic.h"

void step(const std::string& description) {
    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << "=== " << description << " ===" << std::endl;
    std::cout << std::endl;
}

int main() {
    step("Create nurse and surgeone objects");
    Nurse nurse("Nadia", "Romashkova",
                "Vasilievna",
                "14.12.1990",
                "Plushkina street", "middle", 80.0);

    Surgeone surgeone("Oleg", "Romashkov",
                      "Petrov",
                      "11.12.1989",
                      "Vostania street", "high", 50.5);

    nurse.Print();
    surgeone.Print();
    step("Compute their salary");
    std::cout << "Nurse salary: " << nurse.GetSalary(40) << std::endl;
    std::cout << "Surgeone salary: " << surgeone.GetSalary(20) << std::endl;
}
