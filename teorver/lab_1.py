# Реализация на ЭВМ модели случайной величины и определение ее числовых характеристик.
# Вариант 8 а = 1,7

import numpy as np
from scipy import integrate, optimize
from utils import plot
import math



def run():

    # Задаем параметры
    a = 1.7

    # Определение функции F(x) = c * (x + a)
    def func_by_x(x, c):
        return c * (x + a) if 0 <= x <= 1 else 0

    # 1) находим значение константы "с"

    # Уравнение для интеграла
    equation = lambda c: integrate.quad(func_by_x, 0, 1, args=(c))[0] - 1

    # Найти значение c (где 1.0, наше началное предположение)
    c_solution = optimize.fsolve(equation, 1.0)

    print("Значение c:", c_solution[0])

    # 2) Построение графика

    # Создаем массив значений x от -0.1 до 1.1
    x_values = np.linspace(0, 1, 1200)
    y_values = [func_by_x(x, c_solution) for x in x_values]

    plot(x_values, y_values, 'f(x)')

    # 3) нахождение функции распределения

    # функция с учетом константы
    c = c_solution
    def func_by_x_with_c(x):
        return c * (x + a) if 0 <= x <= 1 else 0

    # Функция распределения F(x)
    def distribution_func_by_x(x):
        # от -1 чтобы много не интегрировать
        result, _ = integrate.quad(func_by_x_with_c, -1, x)
        return result

    # Получение значений функций распределения
    distrib_y_values = [distribution_func_by_x(x) for x in x_values]

    # 4) построение функции распределения
    plot(x_values, distrib_y_values, "distribution_func")

    # 5) вычисление значений

    ### мат ожидание - (среднее значение) - интеграл от произведения текущего значения х на плотность вероятности f(x)

    def math_expectation_func(x):
        return x * func_by_x_with_c(x)

    math_expectation, _  = integrate.quad(math_expectation_func, 0, 1)

    print("Мат. ожидание", math_expectation)

    ### медиана - 50-процентный квантиль
    median_equation = lambda x: distribution_func_by_x(x) - 0.5
    median = optimize.fsolve(median_equation, 0.5)

    print("Медиана:", median[0])

    ### Мода - наиболее вероятное значение (пиковое значение на графике плотности распределения)

    max_y = max(y_values)
    for index, y in enumerate(y_values):
        if y == max_y:
            print("Мода:", x_values[index])
            break

    ### Дисперсия - мера разброса значений случайной величины относительно её математического ожидания

    variance_equation = lambda x: ((x - math_expectation) ** 2) * func_by_x_with_c(x)
    variance, _ = integrate.quad(variance_equation, -np.inf, np.inf)

    print("Дисперсия:", variance)

    ### Среднее квадратическое отклонение - корень квадратный из дисперсии

    standard_deviation = math.sqrt(variance)

    print("Среднее квадратическое отклонение:", standard_deviation)