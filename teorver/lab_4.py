import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import os
import math
from scipy import stats

# вариант
array_data = np.array([
    1.18, 1.40, 1.49, 1.50, 1.54, 1.16, 1.39, 1.49, 1.51, 1.54,
    1.30, 1.61, 1.49, 1.52, 0.54, 1.38, 1.61, 1.50, 1.52, 1.52,
    1.54, 1.50, 1.50, 1.49, 1.51, 1.55, 1.51, 1.49, 1.49, 1.52,
    1.50, 1.49, 1.49, 1.51, 1.54, 1.48, 1.50, 1.49, 1.51, 1.54,
    1.59, 1.50, 1.49, 1.50, 1.44, 1.53, 1.50, 1.49, 1.49, 1.44,
    1.64, 1.49, 1.50, 1.46, 1.47, 1.68, 1.49, 1.49, 1.46, 1.47,
    1.52, 1.49, 1.50, 1.51, 1.46, 1.48, 1.49, 1.50, 1.52, 1.49,
    1.47, 1.49, 1.49, 1.50, 1.47, 1.45, 1.49, 1.49, 1.50, 1.48,
    1.50, 1.50, 1.55, 1.54, 1.52, 1.55, 1.50, 1.75, 1.54, 1.53,
    1.56, 1.49, 1.51, 1.48, 1.54, 1.59, 1.49, 1.49, 1.48, 1.55
])
a = 0.1


def hist(q):
    # размах
    data_diff = np.max(array_data) - np.min(array_data)

    # ширина интервала
    width = data_diff / q
    print(f"Ширина интервала: ", width)

    # Задаем ширину интервалов
    bins = np.arange(np.min(array_data), np.max(array_data) + width, width)

    # Строим гистограмму
    plt.hist(array_data, bins=bins, edgecolor='black')
    plt.title(f'q = {q}')
    plt.xlabel('Значения')
    plt.ylabel('Частота')
    plt.show()

    # данные гистограммы
    hist, bin_edges = np.histogram(array_data, bins=q)
    # Таблица с границами интервалов и количеством значений в интервале
    table = pd.DataFrame({'Нижняя граница': bin_edges[:-1],
                          'Верхняя граница': bin_edges[1:],
                          'Количество значений': hist})
    print(f'Таблица гистограммы')
    print(table)


def compute(q):
    # точечная оценка математического ожидания (среднее арифметическое)
    print("\nТочечная оценка математического ожидания: ", np.mean(array_data))

    # нахождение медианы - значение, расположенное посередине в вариационном ряду.
    print("Медиана:", np.median(array_data))

    # мода - любая точка максимума ( значение, которое встречается наиболее часто в выборке)
    print("Мода:", pd.Series(array_data).mode()[0])

    # Определение точечной оценки дисперсии
    # меру разброса или изменчивости значений в выборке
    print("Точечная оценка дисперсии :", np.var(array_data))

    # Среднее квадратическое отклонение
    # насколько сильно каждое значение отклоняется от среднего значения
    print("Среднее квадратическое отклонение:", math.sqrt(np.var(array_data)))

    # Коэффициент асимметрии - количественная характеристика степени скошенности (асимметрии) распределения
    mean_value = np.mean(array_data)
    std_dev = np.std(array_data)
    skewness = np.sum((array_data - mean_value) ** 3) / (len(array_data) * std_dev ** 3)
    print("Коэффициент асимметрии:", skewness)
    # -5 указывает на большую неоднородность данных, смещение

    # Эксцесс (коэффициент эксцесса) – количественная характеристика островершинности плотности распределения
    mean_value = np.mean(array_data)
    std_dev = np.std(array_data)
    kurtosis = np.sum((array_data - mean_value) ** 4) / (len(array_data) * std_dev ** 4) - 3

    print("Эксцесс:", kurtosis)

    # Стандартная ошибка среднего
    # Рассчитываем стандартное отклонение
    std_dev = np.std(array_data)

    # Рассчитываем стандартную ошибку среднего
    sem = std_dev / np.sqrt(len(array_data))

    print("Стандартная ошибка среднего:", sem)

    # предельная ошибка

    # Выбираем уровень доверия
    confidence_level = 1 - a

    # Рассчитываем критическое значение
    z_critical = np.abs(stats.norm.ppf((1 - confidence_level) / 2))

    # Рассчитываем предельную ошибку
    moe = z_critical * sem

    print("Предельная ошибка:", moe)

    # Рассчитываем доверительный интервал
    # вероятность того, что истинное среднее находится в этом интервале
    mean_value = np.mean(array_data)
    confidence_interval = (mean_value - z_critical * sem, mean_value + z_critical * sem)

    print(f"{confidence_level * 100}% Доверительный интервал для среднего значения: {confidence_interval}")


def compute_hi(q):
    ### Проверка гипотезы о нормальности распределения
    interval_bounds = np.linspace(array_data.min(), array_data.max(), q + 1)

    # Создание объекта нормального распределения
    mean, std_dev = np.mean(array_data), np.std(array_data)
    norm_dist = stats.norm(loc=mean, scale=std_dev)

    # Вычисление теоретических вероятностей для каждого интервала
    theoretical_probabilities = np.diff(norm_dist.cdf(interval_bounds))
    print("theor", sum(theoretical_probabilities))
    print()
    # Вывод результатов
    print("Границы интервалов:", interval_bounds)
    hist, _ = np.histogram(array_data, bins=q)
    print("Значения интервалов:", hist)
    print("теоретические вероятности попадания нормально распределенной случайной величины в частичный интервал",
          theoretical_probabilities)
    # знаменатель умножаем на 100 (кол-во элементов)
    denominator = []
    for i in range(q):
        denominator.append(theoretical_probabilities[i] * 100)
    print("Знаменатель N * p", denominator)

    # числитель (n_j - N*p)^2
    # данные гистограммы
    hist_values, values = np.histogram(array_data, bins=q)
    print("pract",values)
    numerator = []
    for i in range(q):
        numerator.append((hist_values[i] - denominator[i]) ** 2)
    print("Числитель (n_j - N*p)^2", numerator)

    # хи
    hi = []
    for i in range(q):
        hi.append(numerator[i] / denominator[i])
    print("Хи значения", hi)

    print("Хи сумма квадратов: ", sum(hi))

    
    # Применение критерия 𝜒 2
    # , для проверки гипотезы о
    # нормальности распределения предполагает наличие в каждом
    # частичном интервале не менее пяти элементов, в противном
    # случае желательно объединять эти интервалы с соседними.
    # насколько хорошо наблюдаемые частоты соответствуют ожидаемым частотам на основе некоторой гипотезы.


q_min = math.ceil(0.55 * 100 ** 0.4 + 1)  # +-1 по заданию
q_max = math.ceil(1.25 * 100 ** 0.4 - 1)  #

print("q_min: ", q_min)
print("q_max: ", q_max)

print(f'\nГистограмма для q={q_min}')
hist(q_min)
print(f'\nГистограмма для q={q_max}')
hist(q_max)
print(f'\nВычисление стат параметров')
compute(q_max)
compute_hi(q_max)



