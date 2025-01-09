import numpy as np
# Построение матрицы попарных сравнений
# 4.2 Сравнение альтернатив по критериям.
# Вход - матрица оценок сущностей(квартир, проектов)
# Выход - массив матриц попарных сравнений (1 - как относятся квартиры друг к другу по цене, 2 как относятся по площади и т.д.)
# Сколько критериев, столько и матриц на выходе
def get_alternative_comparison(matrix):

    # Количество критериев (число столбцов)
    criteria_count = len(matrix[0])
    # Список для хранения матриц парных сравнений
    matrices = []

    # Для каждого критерия создаем матрицу парных сравнений
    for i in range(criteria_count):
        # Инициализация пустой матрицы для парных сравнений текущего критерия
        comparison_matrix = [[0 for _ in range(len(matrix))] for _ in range(len(matrix))]

        # Заполнение матрицы парных сравнений для альтернатив
        for j in range(len(matrix)):  # Проходим по строкам
            for k in range(len(matrix)):  # Проходим по столбцам
                # Элемент матрицы — отношение значений j-й и k-й альтернатив по i-му критерию
                comparison_matrix[j][k] = matrix[j][i] / matrix[k][i]

        # Добавляем текущую матрицу в список
        matrices.append(comparison_matrix)

    return matrices


# Сумма по столбцам
def col_sum(matrix):
    res = []
    for i in range(len(matrix[0])):
        sum = 0.0
        for j in range(len(matrix)):
            sum += matrix[j][i]              
        res.append(sum)
    return res

# Нормализация матрицы
def normalize(matrix):
    cs = col_sum(matrix)
    res = [[0 for _ in matrix[0]] for _ in matrix]
    for i in range(len(matrix[0])):
        for j in range(len(matrix)):
            res[j][i] = matrix[j][i] / cs[i]
    return res

# Вычисление средневзешенного значения строк
def middle_value_weigth_rows(matrix):
    res = []
    normalized = normalize(matrix)
    for i in range(len(normalized)):
        sum = 0.0
        for j in range(len(normalized[0])):
            sum += normalized[i][j]
        res.append(sum / len(normalized[0]))
    return res

# произведение матриц
def dot(lmatrix, rmatrix):
    lmatrix = np.array(lmatrix)
    rmatrix = np.array(rmatrix)

    return np.dot(np.transpose(lmatrix), rmatrix)

# Красивый вывод матрицы
def print_matrix(matrix):
    for j in range(len(matrix)):
        for k in range(len(matrix[j])):
            print("%.2f\t" % (matrix[j][k]), end='')
        print()