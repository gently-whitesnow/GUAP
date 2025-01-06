import numpy as np

# Матрица критериев
criteria_matrix = np.array([
    [1, 3, 5, 7, 9, 3, 5],
    [1/3, 1, 3, 5, 7, 3, 5],
    [1/5, 1/3, 1, 3, 5, 3, 5],
    [1/7, 1/5, 1/3, 1, 3, 3, 5],
    [1/9, 1/7, 1/5, 1/3, 1, 3, 5],
    [1/3, 1/3, 1/3, 1/3, 1/3, 1, 3],
    [1/5, 1/5, 1/5, 1/5, 1/5, 1/3, 1]
])

# 1. Сумма столбцов
column_sums = criteria_matrix.sum(axis=0)

# 2. Нормализация
normalized_matrix = criteria_matrix / column_sums

# 3. Веса критериев
weights_criteria = normalized_matrix.mean(axis=1)

# Матрица альтернатив (6 проектов)
alternatives_matrix = np.array([
    [3, 4, 5, 3, 4, 3, 4],
    [2, 3, 4, 5, 3, 4, 3],
    [4, 3, 2, 4, 5, 3, 4],
    [3, 4, 3, 2, 4, 5, 3],
    [4, 3, 4, 3, 2, 4, 5],
    [5, 4, 3, 4, 3, 2, 4]
])

# 4. Нормализация альтернатив
normalized_alternatives = alternatives_matrix / alternatives_matrix.sum(axis=0)

# 5. Учет разных весов по уровням (AHP+): уточнение весов
level_weights = [0.5, 0.3, 0.2]  # Пример весов для уровня важности
intermediate_weights = normalized_alternatives.dot(weights_criteria)

# 6. Итоговые оценки
scores = level_weights[0] * intermediate_weights + \
         level_weights[1] * intermediate_weights + \
         level_weights[2] * intermediate_weights

# Сортировка результатов
projects = [f"Проект {i+1}" for i in range(len(scores))]
ranking = sorted(zip(projects, scores), key=lambda x: x[1], reverse=True)

# Вывод результатов
print("Рейтинг проектов:")
for project, score in ranking:
    print(f"{project}: {score:.4f}")
