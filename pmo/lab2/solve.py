import numpy as np
from collections import deque

costs = np.array([[11, 5, 4, 2], [1, 4, 5, 9], [9, 8, 7, 10]])

supply = np.array([80, 170, 150])
demand = np.array([70, 60, 180, 90])

initial_plan = np.array([[70, 10, 0, 0], [0, 50, 60, 40], [0, 0, 140, 10]])

# Собираем координаты клеток с положительным планом
rows, cols = np.where(initial_plan > 0)
basis_cells = list(zip(rows, cols))  # 7 штук, нужно 6 в базисе (3+4-1)

# Поиск остовного дерева (убираем одну зависимую клетку)
# Простейший способ — оставить ребра по порядку, пока не образуется цикл.
n_sup, n_dem = initial_plan.shape
parent = list(range(n_sup + n_dem))  # для Union-Find


def find(x):
    if parent[x] != x:
        parent[x] = find(parent[x])
    return parent[x]


def union(x, y):
    rx, ry = find(x), find(y)
    if rx != ry:
        parent[ry] = rx
        return True
    return False


tree = []
for r, c in basis_cells:
    # Номерим поставщиков 0..(n_sup-1), потребителей n_sup..(n_sup+n_dem-1)
    if union(r, n_sup + c):
        tree.append((r, c))
    if len(tree) == n_sup + n_dem - 1:
        break

# Решаем систему потенциалов для остовных клеток
# u[i] + v[j] = costs[i,j] для (i,j) в tree
# Всего переменных u_i (3) + v_j (4) = 7, уравнений 6, фиксируем u[0] = 0
u = np.zeros(n_sup, dtype=float)
v = np.zeros(n_dem, dtype=float)
fixed = [False] * n_sup
fixed[0] = True

# Граф остовных клеток
adj_sup = [[] for _ in range(n_sup)]
adj_dem = [[] for _ in range(n_dem)]
for i, j in tree:
    adj_sup[i].append(j)
    adj_dem[j].append(i)

# Обход в ширину для определения потенциалов
q = deque([0])  # начинаем с u[0] = 0
while q:
    i = q.popleft()
    for j in adj_sup[i]:
        if not any(np.isclose(v[j], [costs[i, j] - u[i]])):
            v[j] = costs[i, j] - u[i]
            for i2 in adj_dem[j]:
                if not any(np.isclose(u[i2], [costs[i2, j] - v[j]])):
                    u[i2] = costs[i2, j] - v[j]
                    q.append(i2)

# Проверяем уменьшенные затраты для небазисных клеток
optimal = True
for i in range(n_sup):
    for j in range(n_dem):
        if (i, j) not in tree:  # небазисная клетка
            rc = costs[i, j] - (u[i] + v[j])
            print(
                f"cost[{i}][{j}] = {costs[i, j]} - u[{i}] v[{j}] = {u[i] + v[j]} == {rc}"
            )
            if rc < 0:
                optimal = False

print("План оптимален" if optimal else "План не оптимален")
