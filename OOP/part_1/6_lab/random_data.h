#pragma once
#include <algorithm>
#include <iostream>
#include <random>
#include <sstream>

class RandomData {
   public:
    RandomData(int m1, int m2, int length);

    void Print() const;

    void Reverse();

    // x = (x<0) ? 0 : x*2
    void PowTransform();

    int NotZeroCount();

    ~RandomData();

   private:
    std::vector<int> _vector;
};

RandomData::RandomData(int m1, int m2, int length) {
    if (length < 0) {
        std::cout << "length must be greater or equal 0" << std::endl;
        return;
    }
    if (m1 > m2) {
        std::cout << "m2 must be greater then m1" << std::endl;
        return;
    }

    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<int> uni(m1, m2);
    _vector.reserve(length);
    for (int i = 0; i < length; ++i) {
        _vector.emplace_back(uni(gen));
    }
}

void RandomData::Print() const {
    std::cout << "Data: " << std::endl;
    for (auto val : _vector) {
        std::cout << val << "\t";
    }
    std::cout << std::endl;
}

void RandomData::Reverse() { std::reverse(_vector.begin(), _vector.end()); }

// x = (x<0) ? 0 : x*2
void RandomData::PowTransform() {
    std::for_each(_vector.begin(), _vector.end(),
                  [](auto& val) { val = val < 0 ? 0 : val * val; });
}

int RandomData::NotZeroCount() {
    return std::count_if(_vector.begin(), _vector.end(),
                         [](auto val) { return val != 0; });
}

RandomData::~RandomData() {
    std::cout << "Destruction:" << std::endl;
    Print();
}
