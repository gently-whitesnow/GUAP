import numpy as np

criteria_matrix = np.array([
    [1, 2, 4, 5, 5, 2, 4],  # Запрашиваемая сумма
    [1 / 2, 1, 2, 4, 5, 2, 4],  # Масштабируемость
    [1 / 4, 1 / 2, 1, 2, 4, 2, 4],  # Надежность исполнителя
    [1 / 5, 1 / 4, 1 / 2, 1, 2, 2, 4],  # Количество предоставляемых рабочих мест
    [1 / 5, 1 / 5, 1 / 4, 1 / 2, 1, 2, 4],  # Степень экологичности
    [1 / 2, 1 / 2, 1 / 2, 1 / 2, 1 / 2, 1, 2],  # Срок внедрения завода
    [1 / 4, 1 / 4, 1 / 4, 1 / 4, 1 / 4, 1 / 2, 1]  # Способность к сопровождению в процессе работы завода
])

# исходные данные
alternatives_matrix = np.array([
    [3, 4, 5, 3, 4, 3, 4],  # Проект 1
    [2, 3, 4, 5, 3, 4, 3],  # Проект 2
    [4, 3, 2, 4, 5, 3, 4],  # Проект 3
    [3, 4, 3, 2, 4, 5, 3],  # Проект 4
    [4, 3, 4, 3, 2, 4, 5],  # Проект 5
    [5, 4, 3, 4, 3, 2, 4]  # Проект 6
])

def print_matrix(matrix):
    if matrix.ndim == 1:  # Если одномерный массив
        print("\t".join(f"{value:.2f}" for value in matrix))
    else:  # Если двумерный массив
        for row in matrix:
            print("\t".join(f"{value:.2f}" for value in row))


# Функция для вычисления весов и нормализации их
def calculate_weights(matrix):
    print("геометрическое среднее строк:")
    print(matrix, "col")
    # np.prod - произведение всех элементов, axis=1 - строки, ** - возведение в степень (где matrix.shape[1] - 3 (количество столбцов))
    weights = np.prod(matrix, axis=1) ** (1 / matrix.shape[1]) 
    print_matrix(weights)

    normalized_weights = weights / weights.sum()
    
    print("нормализованные веса:")
    print_matrix(normalized_weights)   
    
    return normalized_weights

# Проверка согласованности
def check_consistency(weights, matrix):
    # сумма элементов по столбцам
    sum_columns = matrix.sum(axis=0)
    # умножение матриц весов на сумму столбцов
    ymax = sum_columns.T @ weights
    n = matrix.shape[0]
    # вычисление индекса согласованности
    IS = (ymax - n) / (n - 1)
    # Таблица случайной согласованности
    random_index = {1: 0.0, 2: 0.0, 3: 0.58, 4: 0.90, 5: 1.12, 6: 1.24, 7: 1.32, 8: 1.41, 9: 1.45, 10: 1.49}
    SS = random_index[n]
    
    # Отношение согласованности
    OS = IS / SS
    if(OS <= 0.1):
        print("согласовано ", OS)
    else:
        print("не согласовано ", OS)

print("Матрица попарных сравнений критериев:")
print_matrix(criteria_matrix)

criteria_weights = calculate_weights(criteria_matrix)
print("Проверка согласованности:")
check_consistency(criteria_weights, criteria_matrix)

print("Весовые коээфициенты критериев:")
print(criteria_weights)

# Функция для расчета локальных приоритетов альтернатив по каждому критерию
#[1, 2],
#[4, 5],
def calculate_alternative_priorities(alternatives_matrix):
    priorities = []
    for i in range(alternatives_matrix.shape[1]):  # Для каждого критерия (7 столбцов)
        # Создаем матрицу попарных сравнений
        ## выбираем столбец i ([1,4])
        criterion_column = alternatives_matrix[:, i]
        ## превращая вектор-строку в вектор-столбец ([[1],[4]])
        criterion_row = criterion_column[:, np.newaxis]
        ## делим вектор-столбец на вектор-строку 
        # [1/1, 1/4],  
        # [4/1, 4/4]
        comparison_matrix = criterion_row / criterion_column
        # Вычисляем веса для матрицы парных сравнений
        weights = calculate_weights(comparison_matrix)
        print(weights)
        # Проверяем согласованность
        check_consistency(weights, comparison_matrix)
        priorities.append(weights)
    
    # Возвращаем приоритеты в виде транспонированной матрицы
    return np.array(priorities).T


# Рассчитаем локальные приоритеты альтернатив
alternative_priorities = calculate_alternative_priorities(alternatives_matrix)

print("Локальные приоритеты альтернатив:")
print(alternative_priorities)

# Рассчитаем глобальные приоритеты (@ - матричное умножение)
global_priorities = alternative_priorities @ criteria_weights

print("Глобальные приоритеты альтернатив:")
print(global_priorities)

# Определим лучшую альтернативу
best_alternative = np.argmax(global_priorities) + 1  # Нумерация с 1
print(f"Лучшая альтернатива: Проект {best_alternative} - {global_priorities[best_alternative - 1]:.8f}")


