import numpy as np
import matplotlib.pyplot as plt
from scipy.integrate import solve_ivp

# определяем дифф уравнение y' = y / (x + y^3)
def diff_eq(x, y):
    return y / (x + y**3)

x_range = (0, 10)  # промежуток x = 0 по x = 10
y0 = [1]  # начальное условие y(0) = 1

# решение дифф уравнения dense_output - 
solution = solve_ivp(diff_eq, x_range, y0, dense_output=True)

# параметры для построения графика
x_vals = np.linspace(x_range[0], x_range[1], 1000)
y_vals = solution.sol(x_vals)

# параметры фазового портрета dy/dx = f(x, y)
x, y = np.meshgrid(np.linspace(0, 10, 20), np.linspace(-5, 5, 20))
dy = y / (x + y**3)
dx = np.ones_like(dy)

# нормализация векторов
magnitude = np.sqrt(dx**2 + dy**2)
dx /= magnitude
dy /= magnitude

# построение графика фазового портрета
plt.figure(figsize=(10, 6))
plt.quiver(x, y, dx, dy, color='blue', alpha=0.6)
plt.plot(x_vals, y_vals[0], color='red', label="Solution curve y(x)")
plt.title("Phase Portrait")
plt.xlabel("x")
plt.ylabel("y")
plt.legend()
plt.grid(True)
plt.show()

# построение графика решения во временной области
plt.figure(figsize=(10, 6))
plt.plot(x_vals, y_vals[0], color='green', label="y(x)")
plt.title("Solution in Time Domain")
plt.xlabel("x")
plt.ylabel("y")
plt.legend()
plt.grid(True)
plt.show()
