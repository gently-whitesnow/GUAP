import ahp

# исходные данные из презентации
# https://www.hse.ru/mirror/pubs/share/204810376
# могут немного отличаться, потому что находил ошибку в презентации 

# матрица попарных сравнений критериев (матрица важности(весов), какой критерий для нас имеет больший вес)
criteria_matrix = [
        [1, 3, 1, 1/2, 5], # цена
        [1/3, 1, 1/4, 1/7, 2], # размер
        [1, 4, 1, 1, 6], # команты 
        [2, 7, 1, 1, 8], # близость
        [1/5, 1/2, 1/6, 1/8, 1], # категория
]

# исходные данные 
alternatives_matrix = [
    # цена, размер, команты, близость, категория
    [4, 9, 4, 12, 4],                           # квартира 1
    [1, 18, 4, 36, 2],                          # квартира 2
    [8, 6, 2, 3, 20],                           # квартира 3
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
criteria_weight = ahp.weigth_rows(criteria_matrix)
print(criteria_weight)

print("веса альтернатив")
alternative_weights = [ahp.weigth_rows(c) for c in comparison]
for i in range(len(criteria_names)):
    print(criteria_names[i])
    print(alternative_weights[i])

# веса альтернатив с точки зрения достижения цели
goal_weights = ahp.dot(criteria_weight, alternative_weights)
print("веса альтернатив с точки зрения достижения цели")
print(goal_weights)
print("Контрольная сумма весов", sum(goal_weights))


# Модифицированный алгоритм AHP+
def ahp_plus(criteria_matrix, alternatives_matrix, criteria_priorities):
    # Вычисление весов критериев
    criteria_weights = ahp.weigth_rows(criteria_matrix)
    
    # Вычисление весов альтернатив с учётом весов критериев для каждой альтернативы
    alternative_weights = []
    for i in range(len(criteria_weights)):
        criteria_weight = criteria_weights[i]
        criteria_priority = criteria_priorities[i]
        weighted_criteria_weight = [weight * priority for weight in criteria_weight]
        alternative_weights.append(ahp.weigth_rows(weighted_criteria_weight))

    # Вычисление весов альтернатив с точки зрения достижения цели
    goal_weights = ahp.dot(criteria_weights, alternative_weights)
    
    return goal_weights

print("Веса альтернатив с точки зрения достижения цели (модифицированный AHP+):")
print(ahp_plus(criteria_matrix, alternatives_matrix, criteria_priorities))