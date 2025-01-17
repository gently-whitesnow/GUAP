import numpy as np
import matplotlib.pyplot as plt
from scipy.integrate import solve_ivp

# Определяем систему уравнений
def system(t, z):
    x, y = z
    dxdt = 2 * x - 3 * y
    dydt = 3 * x + 2 * y
    return [dxdt, dydt]

# Начальные условия
x0 = 0
y0 = -1
initial_conditions = [x0, y0]

# Диапазон времени
t_span = (0, 10)
t_eval = np.linspace(t_span[0], t_span[1], 1000)

# Решение системы уравнений
solution = solve_ivp(system, t_span, initial_conditions, t_eval=t_eval)

# Извлекаем решения
t = solution.t
x = solution.y[0]
y = solution.y[1]

# Построение графиков
plt.figure(figsize=(10, 6))
plt.plot(t, x, label="x(t)", linewidth=2)
plt.plot(t, y, label="y(t)", linewidth=2)
plt.axhline(0, color="black", linewidth=0.8, linestyle="--")
plt.title("Решение системы уравнений", fontsize=16)
plt.xlabel("Время t", fontsize=12)
plt.ylabel("Значения x(t) и y(t)", fontsize=12)
plt.legend(fontsize=12)
plt.grid()
plt.show()
