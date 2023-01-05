#pragma once
#include <string>
#include <iostream>

void Print(const std::string& s);
void Print(const std::string& s, char end);

void print();

template<class T>
void print(const T& val) {
    std::cout << val;
    print();
}

template<class T, class ... Args>
void print(const T& val, Args... args) {
    std::cout << val << " ";
    print(args...);
}
