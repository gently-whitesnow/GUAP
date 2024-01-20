import matplotlib.pyplot as plt


def plot(x_values, y_values, label):
    # Строим график функции распределения
    plt.plot(x_values, y_values, label=label)
    plt.axhline(0, color='black', linewidth=0.5)
    plt.axvline(0, color='black', linewidth=0.5)
    plt.grid(color='gray', linestyle='--', linewidth=0.5)
    plt.xlabel('x')
    plt.ylabel('CDF(x)')
    plt.legend()
    plt.show()
