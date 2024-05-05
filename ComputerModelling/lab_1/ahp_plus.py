import ahp

# Среднее значение в строке
def ahp_plus_weigth_rows(matrix):
    res = []
    normalized = ahp.normalize(matrix)
    for i in range(len(normalized)):
        sum = 0.0
        for j in range(len(normalized[0])):
            sum += normalized[i][j]
        res.append(sum / len(normalized[0]))
    
    max_weight = max(res)
    for i in range(len(res)):
        res[i] = res[i] / max_weight

    
    return res