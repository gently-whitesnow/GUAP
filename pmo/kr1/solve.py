import matplotlib.pyplot as plt
import numpy as np

# Define the constraints
x1 = np.linspace(0, 10, 400)

# Inequalities
x2_1 = (4 - x1) / 2  # x1 + 2x2 <= 4
x2_2 = 6 - 2 * x1  # 2x1 + x2 <= 6
x2_3 = 8 - x1  # x1 + x2 >= 8

# Feasible region
plt.figure(figsize=(8, 8))
plt.plot(x1, x2_1, label=r"$x_1 + 2x_2 \leq 4$", color="blue")
plt.plot(x1, x2_2, label=r"$2x_1 + x_2 \leq 6$", color="green")
plt.plot(x1, x2_3, label=r"$x_1 + x_2 \geq 8$", color="red")

# Feasible region (shading)
plt.fill_between(
    x1,
    np.maximum(0, x2_3),
    np.minimum(x2_1, x2_2),
    where=(np.minimum(x2_1, x2_2) >= np.maximum(0, x2_3))
    & (np.minimum(x2_1, x2_2) >= 0),
    color="grey",
    alpha=0.3,
)

# Plot settings
plt.xlim(0, 10)
plt.ylim(0, 10)
plt.axhline(0, color="black", linewidth=0.8)
plt.axvline(0, color="black", linewidth=0.8)
plt.xlabel(r"$x_1$")
plt.ylabel(r"$x_2$")
plt.title("Graphical Solution of Linear Programming")
plt.legend()
plt.grid()
plt.show()
