print("start")

import matplotlib.pyplot as plt
import math

X = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
Y = [6.7, 6.6, 6.8, 7.0, 7.0, 7.5, 7.7, 8.2, 8.6, 8.8]

def y(x):
    return 0.9337 * math.log(x, math.e) + 6.0797

def A(x, y):
    n = len(x)
    xy = []
    x2 = []
    for i in range(n):
        xy.append(x[i] * y[i])
    for i in x:
        x2.append(i ** 2)
    return (n * sum(xy) - sum(x) * sum(y)) / (n * sum(x2) - sum(x) ** 2)


def B(a, x, y):
    return (sum(y) - a * sum(x)) / len(x)


# погрешности аппроксимации
def sigma(Y, F):
    delta = []
    for i in range(len(Y)):
        delta.append((Y[i] - F[i]) ** 2)
    return sum(delta)
    # y= ax+b


a = A(X, Y)
b = B(a, X, Y)
F = []  # аналитическая
Fl = []  # по мнк
for i in range(len(X)):
    F.append(y(X[i]))
    Fl.append(a * X[i] + b)
print('y =' + str(a) + 'x +' + str(b))
sl = sigma(Y, F)  # аналитическая
s2 = sigma(Y, Fl)  # по мнк
plt.figure(1)
plt.plot(X, Y, 'o')
plt.plot(X, F, 'k')
plt.plot(X, Fl, 'r')
plt.show()

# 0.2575757575757587x +6.073333333333326