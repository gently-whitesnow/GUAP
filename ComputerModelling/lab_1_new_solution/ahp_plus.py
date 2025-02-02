import numpy as np

###############################################################################
# 0. Исходные данные
###############################################################################
criteria_matrix = np.array([
    [1,    2,    4,    5,    5,    2,    4],
    [1/2,  1,    2,    4,    5,    2,    4],
    [1/4,  1/2,  1,    2,    4,    2,    4],
    [1/5,  1/4,  1/2,  1,    2,    2,    4],
    [1/5,  1/5,  1/4,  1/2,  1,    2,    4],
    [1/2,  1/2,  1/2,  1/2,  1/2,  1,    2],
    [1/4,  1/4,  1/4,  1/4,  1/4,  1/2,  1]
])

alternatives_matrix = np.array([
    [3, 4, 5, 3, 4, 3, 4],  # Проект 1
    [2, 3, 4, 5, 3, 4, 3],  # Проект 2
    [4, 3, 2, 4, 5, 3, 4],  # Проект 3
    [3, 4, 3, 2, 4, 5, 3],  # Проект 4
    [4, 3, 4, 3, 2, 4, 5],  # Проект 5
    [5, 4, 3, 4, 3, 2, 4]   # Проект 6
])

###############################################################################
# 1. Веса критериев c_s из парной матрицы (геом. среднее + нормировка)
###############################################################################
row_geom = np.prod(criteria_matrix, axis=1)**(1 / criteria_matrix.shape[1])
c = row_geom / row_geom.sum()
print("Веса критериев c:", c)

###############################################################################
# 2. Для каждого критерия s считаем локальные приоритеты w_i^s (по AHP)
###############################################################################
m, g = alternatives_matrix.shape  # m проектов, g критериев
W = np.zeros((m, g))              # тут будет w_i^s

for s in range(g):
    # Матрица попарных сравнений альтернатив (m x m)
    pairwise_alt = np.zeros((m, m))
    for i in range(m):
        for j in range(m):
            pairwise_alt[i, j] = (
                alternatives_matrix[i, s] / alternatives_matrix[j, s]
                if alternatives_matrix[j, s] != 0 else 0
            )
    # Локальные приоритеты (геом. метод AHP)
    row_geom_alt = np.prod(pairwise_alt, axis=1)**(1/m)
    w_s = row_geom_alt / row_geom_alt.sum()
    W[:, s] = w_s

###############################################################################
# 3. Рассчитываем w_ij^s(i) = w_i^s / (w_i^s + w_j^s) для всех i,j,s
###############################################################################
w_ij_s = np.zeros((m, m, g))
for i in range(m):
    for j in range(m):
        for s in range(g):
            denom = W[i, s] + W[j, s]
            w_ij_s[i, j, s] = W[i, s] / denom if denom != 0 else 0

print("w_ij_s", w_ij_s)
###############################################################################
# 4. Затем сворачиваем по критериям с учётом весов c_s:
#    w_ij(i) = Σ_s [ c_s * w_ij^s(i) ]
###############################################################################
w_ij = np.sum(c * w_ij_s, axis=2)  # суммируем по s
print("w_ij", w_ij)
###############################################################################
# 5. Итоговая оценка F(i) = (1 / m) * Σ_j [ w_ij(i) ]
###############################################################################
F = w_ij.mean(axis=1)  # среднее по всем j
print("Итоговые F(i):", F)
F_norm = F/np.sum(F)
print("Нормализованный F(i):", F_norm)

best_idx = np.argmax(F_norm)
print("Наилучший проект (1-based):", best_idx + 1)