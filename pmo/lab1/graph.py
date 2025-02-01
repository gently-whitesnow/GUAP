import matplotlib.pyplot as plt
import numpy as np

# Определяем диапазон значений x1 и x2
x1 = np.linspace(0, 10, 200)
x2_1 = (10 - 3 * x1) / 2  # 3x1 + 2x2 >= 10 -> x2 >= (10 - 3x1)/2
x2_2 = (20 + x1) / 4  # -x1 + 4x2 <= 20 -> x2 <= (20 + x1)/4
x2_3 = (16 - x1) / 2  # x1 + 2x2 <= 16 -> x2 <= (16 - x1)/2
x2_4 = (x1 - 4) / 3  # -x1 + 3x2 >= 4 -> x2 >= (4 + x1)/3

# График
plt.figure(figsize=(8, 6))

# Закрашиваем область допустимых решений
x1_fill = np.linspace(0, 10, 200)
x2_fill = np.maximum(np.maximum((10 - 3 * x1_fill) / 2, (4 + x1_fill) / 3), 0)
x2_fill_upper = np.minimum(
    np.minimum((20 + x1_fill) / 4, (16 - x1_fill) / 2), np.max(x2_fill)
)

plt.fill_between(
    x1_fill,
    x2_fill,
    x2_fill_upper,
    color="gray",
    alpha=0.3,
    label="Область допустимых решений",
)

# Линии ограничений
plt.plot(x1, x2_1, "r", label=r"$3x_1 + 2x_2 \geq 10$")
plt.plot(x1, x2_2, "g", label=r"$-x_1 + 4x_2 \leq 20$")
plt.plot(x1, x2_3, "b", label=r"$x_1 + 2x_2 \leq 16$")
plt.plot(x1, x2_4, "m", label=r"$-x_1 + 3x_2 \geq 4$")

# Отмечаем оптимальное решение
plt.scatter(4, 6, color="black", zorder=3, label="Оптимальное решение (4, 6)")

# Оформление графика
plt.xlim(0, 10)
plt.ylim(0, 10)
plt.xlabel(r"$x_1$")
plt.ylabel(r"$x_2$")
plt.title("Графическое решение задачи линейного программирования")
plt.legend()
plt.grid()

# Отображаем график
plt.show()
