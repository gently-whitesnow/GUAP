import numpy as np
from scipy.optimize import linprog

# Исходные данные
supply = [40, 50, 30]  # Запасы поставщиков
demand = [60, 30, 40]  # Потребности потребителей

# Проверяем баланс
if sum(supply) != sum(demand):
    fake_supply = sum(demand) - sum(supply)
    supply.append(fake_supply)
    print("Добавлен фиктивный поставщик с запасом:", fake_supply)

# Создаём матрицу стоимости перевозок
costs = np.array(
    [[8, 6, 10], [9, 12, 7], [14, 9, 16], [0, 0, 0]]  # Фиктивный поставщик
)

# Преобразуем в линейную форму
c = costs.flatten()
A_eq = []
b_eq = []

# Ограничения по поставщикам
for i in range(len(supply)):
    row = [0] * len(c)
    for j in range(len(demand)):
        row[i * len(demand) + j] = 1
    A_eq.append(row)
    b_eq.append(supply[i])

# Ограничения по потребителям
for j in range(len(demand)):
    row = [0] * len(c)
    for i in range(len(supply)):
        row[i * len(demand) + j] = 1
    A_eq.append(row)
    b_eq.append(demand[j])

# Решение задачи
res = linprog(c, A_eq=A_eq, b_eq=b_eq, method="highs")
print("Оптимальное распределение:", res.x.reshape(len(supply), len(demand)))
