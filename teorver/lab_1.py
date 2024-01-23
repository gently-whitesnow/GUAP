import numpy as np
from scipy import integrate, optimize
import math
import matplotlib.pyplot as plt


def plot(x_values, y_values, label):
    plt.plot(x_values, y_values, label=label)
    plt.axhline(0, color='black', linewidth=0.5)
    plt.axvline(0, color='black', linewidth=0.5)
    plt.grid(color='gray', linestyle='--', linewidth=0.5)
    plt.xlabel('x')
    plt.ylabel('CDF(x)')
    plt.legend()


def run():
    a = 1.7

    def func_by_x(x, c):
        return c * (x + a) if 0 <= x <= 1 else 0

    equation = lambda c: integrate.quad(func_by_x, 0, 1, args=(c))[0] - 1
    c_solution = optimize.fsolve(equation, 1.0)[0]

    print("Значение c:", c_solution)

    x_values = np.linspace(0, 1, 1000)
    y_values = [func_by_x(x, c_solution) for x in x_values]

    # 2) Построение графика
    plt.figure(figsize=(10, 5))
    plot(x_values, y_values, 'f(x)')

    # 3) Нахождение функции распределения
    c = c_solution

    def func_by_x_with_c(x):
        return c * (x + a) if 0 <= x <= 1 else 0

    def distribution_func_by_x(x):
        result, _ = integrate.quad(func_by_x_with_c, 0, x)
        return result

    distrib_y_values = [distribution_func_by_x(x) for x in x_values]

    # 4) Построение функции распределения
    plt.figure(figsize=(10, 5))
    plot(x_values, distrib_y_values, "Функция распределения")

    # 5) Вычисление значений
    math_expectation_func = lambda x: x * func_by_x_with_c(x)
    math_expectation, _ = integrate.quad(math_expectation_func, 0, 1)

    print("Мат. ожидание:", math_expectation)

    median_equation = lambda x: distribution_func_by_x(x) - 0.5
    median = optimize.fsolve(median_equation, 0.5)[0]

    print("Медиана:", median)

    max_y = max(y_values)
    mode = [x for x, y in zip(x_values, y_values) if y == max_y][0]

    print("Мода:", mode)

    variance_equation = lambda x: ((x - math_expectation) ** 2) * func_by_x_with_c(x)
    variance, _ = integrate.quad(variance_equation, 0, 1)

    print("Дисперсия:", variance)

    standard_deviation = math.sqrt(variance)
    print("Среднее квадратическое отклонение:", standard_deviation)

    # Отмечаем моду, медиану и среднее значение на графике
    plt.scatter([mode, median, math_expectation], [distribution_func_by_x(mode), distribution_func_by_x(median),
                                                   distribution_func_by_x(math_expectation)], color='red')
    plt.annotate('Mode', (mode, distribution_func_by_x(mode)), textcoords="offset points", xytext=(0, 10), ha='center',
                 fontsize=8, color='red')
    plt.annotate('Median', (median, distribution_func_by_x(median)), textcoords="offset points", xytext=(0, 10),
                 ha='center', fontsize=8, color='red')
    plt.annotate('Mean', (math_expectation, distribution_func_by_x(math_expectation)), textcoords="offset points",
                 xytext=(0, 10), ha='center', fontsize=8, color='red')

    # Выводим графики
    plt.show()

run()
