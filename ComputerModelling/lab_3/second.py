import numpy as np
import matplotlib.pyplot as plt

# определяем систему дифф уравнений
def discrete_system(x, y, a, b, c, d, n_steps):
    x_vals = [x]
    y_vals = [y]
    
    for _ in range(n_steps):
        next_x = a * x_vals[-1] - b * y_vals[-1]
        next_y = c * x_vals[-1] + d * y_vals[-1]
        x_vals.append(next_x)
        y_vals.append(next_y)
        
    return x_vals, y_vals

# начальные условия
x0 = 0
y0 = -1

# параметры для сравнения
parameter_sets = [
    (2, 3, 3, 2),  # оргинальные параметры
    (1.5, 2.5, 2.5, 1.5),  # доп параметры 1
    (3, 1, 1, 3)   # доп парамтеры 2
]
n_steps_list = [50, 100, 150]  # кол-во шагов для сравнения

# траектория для различных наборов параметров
plt.figure(figsize=(10, 6))
for params in parameter_sets:
    a, b, c, d = params
    x_vals, y_vals = discrete_system(x0, y0, a, b, c, d, n_steps_list[0])
    plt.plot(x_vals, y_vals, marker='o', label=f"a={a}, b={b}, c={c}, d={d}")

plt.title("Фазовое пространство для разных наборов параметров")
plt.xlabel("x")
plt.ylabel("y")
plt.grid(True)
plt.legend()
plt.show()

# траектории для разных шагов
plt.figure(figsize=(10, 6))
for n_steps in n_steps_list:
    x_vals, y_vals = discrete_system(x0, y0, *parameter_sets[0], n_steps) 
    plt.plot(x_vals, y_vals, marker='o', label=f"n_steps={n_steps}")

plt.title("Фазовое пространство для разных n_шагов")
plt.xlabel("x")
plt.ylabel("y")
plt.grid(True)
plt.legend()
plt.show()
