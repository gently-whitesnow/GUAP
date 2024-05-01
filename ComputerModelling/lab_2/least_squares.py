import math

# транспонирование матрицы A^T - замена строк нас столбцы
def transpose(matrix):
    transposed = [[0 for _ in range(len(matrix))] for _ in range(len(matrix[0]))]
    for i in range(len(matrix)):
        for j in range(len(matrix[0])):
            transposed[j][i] = matrix[i][j]
    return transposed

# определитель некоторой меньшей квадратной матрицы (2 x 2)
def minor(matrix, i, j):
    return [row[:j] + row[j + 1:] for row in (matrix[:i]+matrix[i+1:])]

# определитель матрицы |A|
def determinant(matrix):
    if len(matrix) == 2:
        return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0]

    det = 0
    for c in range(len(matrix)):
        det += ((-1) ** c) * matrix[0][c] * determinant(minor(matrix, 0, c))
    return det

# обратная матрица A^-1 - при умножении которой на исходную матрицу получается единичная матрица
def inverse(matrix):
    det = determinant(matrix)
    #случай для матрицы 2x2
    if len(matrix) == 2:
        return [[matrix[1][1] / det, -1 * matrix[0][1] / det],
                [-1 * matrix[1][0] / det, matrix[0][0] / det]]

    cofactors = []
    for r in range(len(matrix)):
        cofactorRow = []
        for c in range(len(matrix)):
            m = minor(matrix, r, c)
            cofactorRow.append(((-1) ** (r+c)) * determinant(m))
        cofactors.append(cofactorRow)
    cofactors = transpose(cofactors)
    for r in range(len(cofactors)):
        for c in range(len(cofactors)):
            cofactors[r][c] = cofactors[r][c] / det
    return cofactors

# возвращает массив , где каждое число возводится в степень
def pow(array, degree):
    return [x ** degree for x in array]

# возвращает массив единиц
def ones(length):
    return [1 for x in range(length)]

# строит матрицу из переданных массивов
def vstack(arr):
    result = []
    for j in range(len(arr)):
        line = []
        for i in range(len(arr[0])):
            line.append(arr[j][i])
        result.append(line)
    return result

# если это список, делает из него матрицу
def listToMatrix(matrix):
    if not isinstance(matrix[0], list):
        return [[e] for e in matrix]
    return matrix

# умножение матриц
def multiply(lmatrix, rmatrix):
    lmatrix = listToMatrix(lmatrix)
    rmatrix = listToMatrix(rmatrix)
    N = len(lmatrix)
    M = len(lmatrix[0])
    P = len(rmatrix[0])
    result = [[0 for _ in range(P)] for _ in range(N)]
    for i in range(N):
        for j in range(P):
            for k in range(M):
                result[i][j] += lmatrix[i][k] * rmatrix[k][j]

    return result

# Метод наименьших квадратов для решения системы линейных уравнений
# https://ru.wikipedia.org/wiki/%D0%9C%D0%B5%D1%82%D0%BE%D0%B4_%D0%BD%D0%B0%D0%B8%D0%BC%D0%B5%D0%BD%D1%8C%D1%88%D0%B8%D1%85_%D0%BA%D0%B2%D0%B0%D0%B4%D1%80%D0%B0%D1%82%D0%BE%D0%B2
# (A^T * A)^-1 * A^T * b
def least_squares_func(x, y):
    n = len(x)

    A = transpose(vstack([ones(n), x, pow(x, 2)]))
    b = y

    return multiply(multiply(inverse(multiply(transpose(A), A)), transpose(A)), b)