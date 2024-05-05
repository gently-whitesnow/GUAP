import ahp
import ahp_plus

# исходные данные из 5 варианта

# матрица попарных сравнений критериев (матрица важности(весов), какой критерий для нас имеет больший вес)
# беру из головы, так как у меня это не задано
criteria_matrix = [
        [1, 3, 5, 7, 9, 3, 5], # Запрашиваемая сумма
         [1 / 3, 1, 3, 5, 7, 3, 5], # Масштабируемость
         [1 / 5, 1 / 3, 1, 3, 5, 3, 5], # Надежность исполнителя
         [1 / 7, 1 / 5, 1 / 3, 1, 3, 3, 5], # Количество предоставляемых рабочих мест
         [1 / 9, 1 / 7, 1 / 5, 1 / 3, 1, 3, 5], # Степень экологичности
         [1 / 3, 1 / 3, 1 / 3, 1 / 3, 1 / 3, 1, 3], # Срок внедрения завода
         [1 / 5, 1 / 5, 1 / 5, 1 / 5, 1 / 5, 1 / 3, 1]] # Способность к сопровождению в процессе работы завода

# # исходные данные, тоже придумываю 
alternatives_matrix = [
    [3, 4, 5, 3, 4, 3, 4],  # Проект 1
    [2, 3, 4, 5, 3, 4, 3],  # Проект 2
    [4, 3, 2, 4, 5, 3, 4],  # Проект 3
    [3, 4, 3, 2, 4, 5, 3],  # Проект 4
    [4, 3, 4, 3, 2, 4, 5],  # Проект 5
    [5, 4, 3, 4, 3, 2, 4]  # Проект 6
]


print("матрица попарных сравнений для критериев")
ahp.print_matrix(criteria_matrix)

# матрица попарных сравнений для исходных данных
print("матрица попарных сравнений для исходных данных")
comparison = ahp.get_alternative_comparison(alternatives_matrix)
criteria_names = ["цена", "размер", "команты",
                  "близость", "категория"]
for i in range(len(criteria_names)):
    print(criteria_names[i])
    ahp.print_matrix(comparison[i])

print("веса критериев")
criteria_weight = ahp.middle_value_weigth_rows(criteria_matrix)
print(criteria_weight)

print("веса альтернатив")
alternative_weights = [ahp_plus.ahp_plus_weigth_rows(c) for c in comparison]
for i in range(len(criteria_names)):
    print(criteria_names[i])
    print(alternative_weights[i])

# веса альтернатив с точки зрения достижения цели
goal_weights = ahp.dot(criteria_weight, alternative_weights)
print("веса альтернатив с точки зрения достижения цели")
print(goal_weights)
print("Контрольная сумма весов", sum(goal_weights))

print("Нормализация")
sum_weights = sum(goal_weights)
normalized_res = [r / sum_weights for r in goal_weights]
print(normalized_res)
