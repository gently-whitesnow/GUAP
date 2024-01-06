#pragma once
#include <cmath>
#include <iostream>
#include <sstream>
#include <string>
#include <tuple>

class Complex {
   public:
    struct Algebraic {
        double real;
        double image;
    };

    struct Trigonometric {
        double radius;
        double angle;
    };

    Complex(Algebraic algebraic);

    Complex(Trigonometric trigonometric);

    std::string ToString() const;

    ~Complex();

    Algebraic GetAlgebraic() const;

    Trigonometric GetTrigonometric() const;

    const bool operator==(const Complex& rhs) const;

    friend Complex operator+(const Complex& lhs, const Complex& rhs);
    friend Complex operator-(const Complex& lhs, const Complex& rhs);
    friend Complex operator*(const Complex& lhs, const Complex& rhs);
    friend Complex operator/(const Complex& lhs, const Complex& rhs);

    friend std::ostream& operator<<(std::ostream&, const Complex& complex);

   private:
    Algebraic algebraic_;
    Trigonometric trigonometric_;

    Algebraic ToAlgebraic(const Trigonometric& trigonometric);

    Trigonometric ToTrigonometric(const Algebraic& algebraic);
};

Complex::Complex(Algebraic algebraic)
    : algebraic_(algebraic), trigonometric_(ToTrigonometric(algebraic_)) {}

Complex::Complex(Trigonometric trigonometric)
    : algebraic_(ToAlgebraic(trigonometric)), trigonometric_(trigonometric) {}

std::string Complex::ToString() const {
    std::stringstream ss;
    ss << "Complex(real: " << algebraic_.real << " image: " << algebraic_.image
       << " radius: " << trigonometric_.radius
       << " angle: " << trigonometric_.angle << ")";
    return ss.str();
}

Complex::~Complex() {
    std::cout << std::endl
              << "___ destruct: " << ToString() << " ___" << std::endl;
}

Complex::Algebraic Complex::GetAlgebraic() const { return algebraic_; }

Complex::Trigonometric Complex::GetTrigonometric() const { return trigonometric_; }

const bool Complex::operator==(const Complex& rhs) const {
    return std::tie(algebraic_.real, algebraic_.image) ==
           std::tie(rhs.algebraic_.real, rhs.algebraic_.image);
}

Complex::Algebraic Complex::ToAlgebraic(const Complex::Trigonometric& trigonometric) {
    return Algebraic{.real = trigonometric.radius * cos(trigonometric.angle),
                     .image = trigonometric.radius * sin(trigonometric.angle)};
}

Complex::Trigonometric Complex::ToTrigonometric(const Complex::Algebraic& algebraic) {
    auto x = algebraic.real;
    auto y = algebraic.image;
    return Trigonometric{.radius = sqrt(x * x + y * y), .angle = atan2(y, x)};
}

Complex operator+(const Complex& lhs, const Complex& rhs) {
    return Complex(Complex::Algebraic{
        .real = lhs.algebraic_.real + rhs.algebraic_.real,
        .image = lhs.algebraic_.image + rhs.algebraic_.image});
}

Complex operator-(const Complex& lhs, const Complex& rhs) {
    return Complex(Complex::Algebraic{
        .real = lhs.algebraic_.real - rhs.algebraic_.real,
        .image = lhs.algebraic_.image - rhs.algebraic_.image});
}

std::ostream& operator<<(std::ostream& stream, const Complex& complex) {
    return stream << complex.ToString();
}

Complex operator*(const Complex& lhs, const Complex& rhs) {
    return Complex(Complex::Algebraic{
        .real = lhs.algebraic_.real * rhs.algebraic_.real -
                lhs.algebraic_.image * rhs.algebraic_.image,
        .image = lhs.algebraic_.real * rhs.algebraic_.image +
                 lhs.algebraic_.image * rhs.algebraic_.real});
}

Complex operator/(const Complex& lhs, const Complex& rhs) {
    double denominator = rhs.algebraic_.real * rhs.algebraic_.real +
                         rhs.algebraic_.image * rhs.algebraic_.image;
    if (denominator == 0) {
        std::cout << "Division by zero" << std::endl;
        return Complex(Complex::Algebraic{.real = 0, .image = 0});
    }

    return Complex(Complex::Algebraic{
        .real = (lhs.algebraic_.real * rhs.algebraic_.real +
                 lhs.algebraic_.image * rhs.algebraic_.image) /
                denominator,
        .image = (lhs.algebraic_.image * rhs.algebraic_.real -
                  lhs.algebraic_.real * rhs.algebraic_.image) /
                 denominator});
}