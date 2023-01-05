#include "print.h"

void print() {
    std::cout << std::endl;
}

void Print(const std::string& s) {
    std::cout << s << std::endl;
}

void Print(const std::string& s, char end) {
    std::cout << s << end;
}