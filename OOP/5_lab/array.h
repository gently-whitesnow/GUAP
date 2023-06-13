#pragma once
#include <cmath>
#include <exception>
#include <iostream>
#include <sstream>
#include <string>

struct OutOfRangeException : public std::exception {
    const char* what() const noexcept override {
        return "Out of range Exception";
    }
};

class Array {
   public:
    Array(int* initArray, int len);

    void Print() const;

    int Min() const;
    int SumBetweenFirstNegativeValues() const;

    // Преобразовать массив таким образом, чтобы сначала располагались все
    // элементы, модуль которых не превышает единицу, а потом — все остальные.
    // с сохранением порядка элементов
    void NormalizeTransformSave();

    void NormalizeTransform();

    ~Array();

   private:
    int* _array = nullptr;
    const int _capacity = 13;
    int _length;
};

Array::Array(int* initArray, int len) {
    try {
        if (len < 0) {
            throw std::invalid_argument("len must be greater or equal 0");
        }

        if (len > _capacity) {
            throw OutOfRangeException();
        }
        _length = len;
        _array = new int[_length];

        for (size_t i = 0; i < _length; i++) {
            _array[i] = initArray[i];
        }
    } catch (const std::exception& e) {
        std::cout << "Exception caught in constructor: " << e.what()
                  << std::endl;
    }
}

void Array::Print() const {
    if (_array == nullptr) {
        std::cout << "Empty array" << std::endl;
        return;
    }
    std::cout << "Array: " << std::endl;
    for (size_t i = 0; i < _length; i++) {
        std::cout << _array[i] << " ";
    }
    std::cout << std::endl;
}

int Array::Min() const {
    int min = INT_MAX;
    for (size_t i = 0; i < _length; i++) {
        if (_array[i] < min) min = _array[i];
    }
    return min;
}
int Array::SumBetweenFirstNegativeValues() const {
    int sum = 0;
    auto isFirstMinValue = false;
    for (size_t i = 0; i < _length; i++) {
        if (_array[i] < 0) {
            if (!isFirstMinValue) {
                isFirstMinValue = true;
                continue;
            }
            break;
        }

        if (isFirstMinValue) sum += _array[i];
    }
    return sum;
}

// Преобразовать массив таким образом, чтобы сначала располагались все
// элементы, модуль которых не превышает единицу, а потом — все остальные.
// с сохранением порядка элементов
void Array::NormalizeTransformSave() {
    auto newArray = new int[_length];
    auto normCount = 0;
    for (size_t i = 0; i < _length; i++) {
        if (abs(_array[i]) <= 1) {
            newArray[normCount] = _array[i];
            normCount += 1;
        }
    }
    for (size_t i = 0; i < _length; i++) {
        if (abs(_array[i]) > 1) {
            newArray[normCount] = _array[i];
            normCount++;
        }
    }
    delete[] _array;
    _array = newArray;
}

void Array::NormalizeTransform() {
    int last = 0;
    for (size_t i = 0; i < _length; i++) {
        if (abs(_array[i]) <= 1) {
            std::swap(_array[last], _array[i]);
            ++last;
        }
    }
}

Array::~Array() {
    std::cout << "Destruction:" << std::endl;
    Print();
    if (_array != nullptr) {
        delete[] _array;
    }
}
