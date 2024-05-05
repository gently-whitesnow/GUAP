import ahp

# Вычисление весов по среднему, а потом по максимальному значению
def ahp_plus_weigth_rows(matrix):
    res = []
    normalized = ahp.normalize(matrix)
    for i in range(len(normalized)):
        s = 0.0
        for j in range(len(normalized[0])):
            s += normalized[i][j]
        res.append(s / len(normalized[0]))
    
    max_weight = max(res)
    for i in range(len(res)):
        res[i] = res[i] / max_weight

    # нормализуем
    sum_weights = sum(res)
    normalized_res = [r / sum_weights for r in res]
    
    return normalized_res