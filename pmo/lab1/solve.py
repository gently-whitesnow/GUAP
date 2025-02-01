import numpy as np


def simplex(c, A, b):
    """
    Реализация симплекс-метода для задачи линейного программирования.

    :param c: Коэффициенты целевой функции (список)
    :param A: Матрица ограничений (двумерный список)
    :param b: Правая часть ограничений (список)
    :return: Оптимальное решение и значение целевой функции
    """
    A = np.array(A, dtype=float)
    b = np.array(b, dtype=float)
    c = np.array(c, dtype=float)

    num_constraints, num_variables = A.shape

    # Добавляем базисные переменные (единичная матрица)
    A = np.hstack((A, np.eye(num_constraints)))

    # Обновляем коэффициенты целевой функции (добавляем 0 для базисных переменных)
    c = np.concatenate((c, np.zeros(num_constraints)))

    # Создаём симплекс-таблицу
    table = np.zeros((num_constraints + 1, num_variables + num_constraints + 1))

    # Заполняем таблицу
    table[:-1, :-1] = A
    table[:-1, -1] = b
    table[-1, :-1] = -c

    # Основной цикл симплекс-метода
    while np.any(table[-1, :-1] < 0):  # Пока в строке Z есть отрицательные коэффициенты
        # Выбираем вводимую переменную (столбец с наибольшим отрицательным значением в z-строке)
        pivot_col = np.argmin(table[-1, :-1])

        # Определяем исключаемую переменную по минимальному положительному отношению b/a
        ratios = table[:-1, -1] / table[:-1, pivot_col]
        ratios[table[:-1, pivot_col] <= 0] = (
            np.inf
        )  # Исключаем отрицательные и нулевые значения

        if np.all(ratios == np.inf):  # Проверка на отсутствие ограничений
            raise ValueError("Решение неограниченно")

        pivot_row = np.argmin(ratios)  # Строка ведущего элемента

        # Преобразуем ведущий элемент в 1
        table[pivot_row, :] /= table[pivot_row, pivot_col]

        # Обновляем таблицу методом Гаусса-Жордана
        for i in range(table.shape[0]):
            if i != pivot_row:
                table[i, :] -= table[i, pivot_col] * table[pivot_row, :]

    # Оптимальное решение
    solution = np.zeros(num_variables)
    for i in range(num_constraints):
        col = np.where(table[i, :-1] == 1)[0]
        if len(col) == 1 and col[0] < num_variables:
            solution[col[0]] = table[i, -1]

    return solution, table[-1, -1]


# Данные задачи
c = [1, 3]  # Коэффициенты целевой функции (z = x1 + 3x2)
A = [
    [-3, -2],  # 3x1 + 2x2 >= 10  -> -3x1 - 2x2 <= -10
    [-1, 4],  # -x1 + 4x2 <= 20
    [1, 2],  # x1 + 2x2 <= 16
    [1, -3],  # -x1 + 3x2 >= 4  -> x1 - 3x2 <= -4
]
b = [-10, 20, 16, -4]  # Правая часть ограничений

# Запуск симплекс-метода
solution, optimal_value = simplex(c, A, b)

# Вывод результата
print("Оптимальные значения переменных:", solution)
print("Оптимальное значение целевой функции:", optimal_value)
