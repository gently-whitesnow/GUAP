import numpy as np


def linear_sum_assignment(cost_matrix, maximize=False):
    cost = np.array(cost_matrix, dtype=float)
    if maximize:
        cost = cost.max() - cost
    n, m = cost.shape
    dim = max(n, m)
    # Паддинг до квадратной матрицы
    padded_cost = np.full((dim, dim), cost.max() + 1)
    padded_cost[:n, :m] = cost

    # Шаг 1: вычитание минимума в строках и столбцах
    for i in range(dim):
        padded_cost[i] -= padded_cost[i].min()
    for j in range(dim):
        padded_cost[:, j] -= padded_cost[:, j].min()

    # 0: нет отметки, 1: "звёздочка", 2: "прямоугольник" (прайм)
    mask = np.zeros((dim, dim), dtype=int)
    row_covered = np.zeros(dim, dtype=bool)
    col_covered = np.zeros(dim, dtype=bool)

    # Шаг 2: начальное звездное покрытие
    for i in range(dim):
        for j in range(dim):
            if padded_cost[i, j] == 0 and not row_covered[i] and not col_covered[j]:
                mask[i, j] = 1
                row_covered[i] = True
                col_covered[j] = True
    row_covered[:] = False
    col_covered[:] = False

    def cover_columns_with_starred():
        for i in range(dim):
            for j in range(dim):
                if mask[i, j] == 1:
                    col_covered[j] = True

    cover_columns_with_starred()

    # Шаг 3: итеративное улучшение покрытия
    while col_covered.sum() < dim:
        while True:
            # Поиск не покрытого нуля
            found = False
            for i in range(dim):
                if not row_covered[i]:
                    for j in range(dim):
                        if not col_covered[j] and padded_cost[i, j] == 0:
                            z_r, z_c = i, j
                            found = True
                            break
                    if found:
                        break
            if not found:
                # Шаг 4: корректировка матрицы
                minval = np.min(padded_cost[~row_covered][:, ~col_covered])
                for i in range(dim):
                    if row_covered[i]:
                        padded_cost[i] += minval
                    for j in range(dim):
                        if not col_covered[j]:
                            padded_cost[i, j] -= minval
                continue
            # Присваиваем "прайм" найденному нулю
            mask[z_r, z_c] = 2
            star_in_row = np.where(mask[z_r] == 1)[0]
            if star_in_row.size:
                row_covered[z_r] = True
                col_covered[star_in_row[0]] = False
            else:
                # Шаг 5: находим чередующуюся цепочку
                path = [(z_r, z_c)]
                while True:
                    star_rows = np.where(mask[:, path[-1][1]] == 1)[0]
                    if star_rows.size == 0:
                        break
                    r_star = star_rows[0]
                    path.append((r_star, path[-1][1]))
                    prime_cols = np.where(mask[r_star] == 2)[0]
                    path.append((r_star, prime_cols[0]))
                # Модифицируем маску: инвертируем метки в цепочке
                for r, c in path:
                    mask[r, c] = 1 if mask[r, c] == 2 else 0
                row_covered[:] = False
                col_covered[:] = False
                mask[mask == 2] = 0
                cover_columns_with_starred()
                break
        if col_covered.sum() >= dim:
            break

    # Формирование результата для исходной матрицы
    row_ind, col_ind = [], []
    for i in range(n):
        for j in range(m):
            if mask[i, j] == 1:
                row_ind.append(i)
                col_ind.append(j)
                break
    return np.array(row_ind), np.array(col_ind)


def hungarian_algorithm(cost_matrix):
    row_ind, col_ind = linear_sum_assignment(cost_matrix)
    return row_ind, col_ind


# Пример использования:
if __name__ == "__main__":
    cost_matrix = np.array(
        [
            [14, 1, 1, 7, 2],
            [11, 12, 4, 3, 1],
            [10, 3, 10, 8, 12],
            [15, 10, 9, 1, 5],
            [15, 2, 12, 1, 10],
        ]
    )
    row_ind, col_ind = hungarian_algorithm(cost_matrix)
    optimal_cost = cost_matrix[row_ind, col_ind].sum()
    assignments = list(zip(row_ind + 1, col_ind + 1))
    print(optimal_cost)
    print(assignments)
