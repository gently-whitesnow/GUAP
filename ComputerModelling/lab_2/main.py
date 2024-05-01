import numpy as np
import matplotlib.pyplot as plt

# функция реализации МНК
from least_squares import least_squares_func
# функция скорректированное R^2
from adjusted_r_squared import adjusted_r_squared_func

# Исходные данные
X = [2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009]
Y = [16.0, 17.9, 18.6, 18.3, 19.0, 19.3, 19.2, 20.3, 21.1, 21.9]

# a. Реализация МНК для полинома второго порядка
coeffs = least_squares_func(X, Y)

a0 = coeffs[0][0]
a1 = coeffs[1][0]
a2 = coeffs[2][0]
print("Коэффициенты МНК для полинома второго порядка: ", a0, a1, a2)

# b. Построение графика c исходными данными
plt.figure(figsize=(10, 6))
plt.scatter(X, Y, label='Исходные данные')

# Берем производную с использованием наших коэффициентов
# a2x^2 + a1x + a0
derivative = np.poly1d([a2, a1, a0])
plt.plot(X, derivative(X), label='Полином 2-го порядка', color='red')
# подставляет в многочлен заданное значение для получения предсказаний на основе которых
# будет рассчитываться коэфф R^2
y_pred_poly2 = np.polyval([a2, a1, a0], X)
# Где k = degree + 1
adj_r_squared_poly2 = adjusted_r_squared_func(Y, y_pred_poly2, len(Y), 3)
print("Скорректированный R^2 для модели степени 2:", adj_r_squared_poly2)

# Перебираем различные степени полиномов, пропуская вторую
for degree in range(1, 4):
    if degree == 2:
        continue
    # теперь для расчета коэффициентов используем функцию библиотеки numpy
    coeffs = np.polyfit(X, Y, degree)
    derive = np.poly1d(coeffs)
    plt.plot(X, derive(X), label=f'Степень {degree}')

    y_pred = np.polyval(coeffs, X)
    # k = degree+1 степень полинома
    adj_r_squared = adjusted_r_squared_func(Y, y_pred, len(Y), degree + 1)
    print(f'Скорректированный R^2 для модели степени {degree}:', adj_r_squared)

# c. Дополнительная функциональная модель типа pow(x+1, 1/3)
additional_y = np.power(np.array(Y) + 1, 1 / 3)

plt.plot(X, additional_y, label='f3 = (x+1)^(1/3)')
plt.xlabel('x')
plt.ylabel('y')
plt.legend()
plt.grid(True)
plt.show()

# d. Определение функции для вычисления скорректированного R^2

# По ходу задачи уже вычислили этот коэффициент, осталось только для функциональной модели:

adj_r_squared_for_func_model = adjusted_r_squared_func(Y, additional_y, len(Y), 1)
print("Скорректированный R^2 для функциональной модели:", adj_r_squared_for_func_model)
