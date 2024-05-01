import numpy as np

# Функция для вычисления коэффициента детерминации R^2
def r_squared(y_true, y_pred):
    # Вычисление среднего фактических значений
    y_mean = np.mean(y_true)
    # Сумма квадратов отклонений (SST)
    sst = np.sum((y_true - y_mean)**2)
    # Сумма квадратов остатков (SSE)
    sse = np.sum((y_true - y_pred)**2)
    # Вычисление R^2
    r2 = 1 - (sse / sst)
    return r2

# вычисления скорректированного R^2
# y - предсказанное значение
# x - значение
# n - кол-во измерений
# k - количество предикторов (линейная 1, полиномиальная degree+1)
def adjusted_r_squared_func(y_true, y_pred, n, k):
    r_squar = r_squared(y_true, y_pred)
    adj_r_squared = 1 - (1 - r_squar) * ((n - 1) / (n - k - 1))
    return adj_r_squared

X = [2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009]
Y = [16.0, 17.9, 18.6, 18.3, 19.0, 19.3, 19.2, 20.3, 21.1, 21.9]

import matplotlib.pyplot as plt

y_func_model = np.power(np.array(Y) + 1, 1 / 3)

plt.plot(X, y_func_model, label='f3 = (x+1)^(1/3)')
plt.xlabel('x')
plt.ylabel('y')
plt.legend()
plt.grid(True)
plt.show()