# Реализация на ЭВМ модели случайной величины и определение ее числовых характеристик.
# Вариант 8 а = 1,7

import numpy as np
import matplotlib.pyplot as plt
from scipy.integrate import quad


def run():

    # Задаем параметры
    a = 1.7

    # Определение функции F(x)
    def f(x, c, a):
        return c * (x + a) if 0 <= x <= 1 else 0

    # Определение функции плотности вероятности (PDF)
    def pdf(x, c, a):
        return c if 0 <= x <= 1 else 0

    # Вычисление интеграла функции плотности вероятности для получения функции распределения
    def cdf(x, c, a):
        result, _ = quad(pdf, 0, x, args=(c, a))
        return result

    # Задаем параметр c
    c = 1 / quad(pdf, 0, 1, args=(1, a))[0]

    # Создаем массив значений x от 0 до 1
    x_values = np.linspace(0, 1, 1000)

    # Вычисляем значения функции распределения CDF(x)
    cdf_values = [cdf(x, c, a) for x in x_values]

    # Строим график функции распределения
    plt.plot(x_values, cdf_values, label='CDF(x)')
    plt.axhline(0, color='black', linewidth=0.5)
    plt.axvline(0, color='black', linewidth=0.5)
    plt.grid(color='gray', linestyle='--', linewidth=0.5)
    plt.xlabel('x')
    plt.ylabel('CDF(x)')
    plt.legend()
    plt.show()
