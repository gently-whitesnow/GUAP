import numpy as np

criteria_matrix = np.array([
    [1, 3, 5, 7, 9, 3, 5],
    [1/3, 1, 3, 5, 7, 3, 5],
    [1/5, 1/3, 1, 3, 5, 3, 5],
    [1/7, 1/5, 1/3, 1, 3, 3, 5],
    [1/9, 1/7, 1/5, 1/3, 1, 3, 5],
    [1/3, 1/3, 1/3, 1/3, 1/3, 1, 3],
    [1/5, 1/5, 1/5, 1/5, 1/5, 1/3, 1]
])

# Суммируем столбцы
column_sums = criteria_matrix.sum(axis=0)
# Нормализация матрицы
normalized_matrix = criteria_matrix / column_sums
# Вычисляем веса критериев
weights = normalized_matrix.mean(axis=1)


alternatives_matrix = np.array([
    [3, 4, 5, 3, 4, 3, 4],
    [2, 3, 4, 5, 3, 4, 3],
    [4, 3, 2, 4, 5, 3, 4],
    [3, 4, 3, 2, 4, 5, 3],
    [4, 3, 4, 3, 2, 4, 5],
    [5, 4, 3, 4, 3, 2, 4]
])

# Нормализация матрицы альтернатив
normalized_alternatives = alternatives_matrix / alternatives_matrix.sum(axis=0)
# Оценка альтернатив
scores = normalized_alternatives.dot(weights)


# Нумерация проектов
projects = [f"Проект {i+1}" for i in range(len(scores))]
# Сортировка по убыванию
ranking = sorted(zip(projects, scores), key=lambda x: x[1], reverse=True)

print(ranking)