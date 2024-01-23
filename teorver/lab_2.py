import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import os
import math
from scipy.stats import norm

# равномерное распределение
def getRandValues():
    # Заданные параметры
    a = -8 / 10
    b = 8 / 2
    l1 = 100
    l2 = 1000

    # Пути к файлам
    file_path_l1 = 'lab_2_rand_l1.txt'
    file_path_l2 = 'lab_2_rand_l2.txt'

    # Проверка наличия данных
    if os.path.isfile(file_path_l1) and os.path.isfile(file_path_l2):
        # Если данные уже сгенерированы, загружаем их
        data_l1 = np.loadtxt(file_path_l1, delimiter=',')
        data_l2 = np.loadtxt(file_path_l2, delimiter=',')
    else:
        # Генерация данных
        data_l1 = (b - a) * np.random.rand(l1) + a
        data_l2 = (b - a) * np.random.rand(l2) + a

        # Запись данных в файл
        np.savetxt(file_path_l1, data_l1, delimiter=',')
        np.savetxt(file_path_l2, data_l2, delimiter=',')
    return data_l1, data_l2

# нормальное распределение
def getGausValues(m, sigma, filename):
    # Задаем параметры нормального распределения
    l1 = 100
    l2 = 1000

    file_path_l1 = 'lab_2_'+filename+'_l1.txt'
    file_path_l2 = 'lab_2_'+filename+'_l2.txt'

    if os.path.isfile(file_path_l1) and os.path.isfile(file_path_l2):
        data_l1 = np.loadtxt(file_path_l1, delimiter=',')
        data_l2 = np.loadtxt(file_path_l2, delimiter=',')
    else:
        # Генерация гауссовской случайной величины
        data_l1 = np.random.normal(m, sigma, l1)
        data_l2 = np.random.normal(m, sigma, l2)

        np.savetxt(file_path_l1, data_l1, delimiter=',')
        np.savetxt(file_path_l2, data_l2, delimiter=',')

    return data_l1, data_l2


def compute(data_l1, data_l2, title):
    # Визуализация данных
    plt.figure(figsize=(12, 5))

    plt.subplot(1, 2, 1)
    hist_l1, bin_edges_l1 = np.histogram(data_l1, bins=7)
    plt.hist(data_l1, bins=7, edgecolor='black', alpha=0.7)
    plt.title('Гистограмма для l1-' + title)

    plt.subplot(1, 2, 2)
    hist_l2, bin_edges_l2 = np.histogram(data_l2, bins=5)
    plt.hist(data_l2, bins=5, edgecolor='black', alpha=0.7)
    plt.title('Гистограмма для l2 -' + title)

    plt.show()

    # Вычисление разности максимального и минимального значения
    diff_l1 = np.max(data_l1) - np.min(data_l1)
    diff_l2 = np.max(data_l2) - np.min(data_l2)

    # Таблица с границами интервалов и количеством значений в интервале
    table_l1 = pd.DataFrame({'Нижняя граница': bin_edges_l1[:-1],
                             'Верхняя граница': bin_edges_l1[1:],
                             'Количество значений': hist_l1})

    table_l2 = pd.DataFrame({'Нижняя граница': bin_edges_l2[:-1],
                             'Верхняя граница': bin_edges_l2[1:],
                             'Количество значений': hist_l2})

    print("Разность максимального и минимального значения для l1:", diff_l1)
    print("Разность максимального и минимального значения для l2:", diff_l2)

    print("\nТаблица для l1:")
    print(table_l1)

    print("\nТаблица для l2:")
    print(table_l2)

    # точечная оценка математического ожидания (среднее арифметическое)
    print("Точечная оценка математического ожидания для l1", np.mean(data_l1))
    print("Точечная оценка математического ожидания для l1", np.mean(data_l2))

    # нахождение медианы - значение, расположенное посередине в вариационном ряду.
    median_l1 = np.median(data_l1)
    median_l2 = np.median(data_l2)

    print("\nМедиана для l1:", median_l1)
    print("Медиана для l2:", median_l2)

    # мода - любая точка максимума ( значение, которое встречается наиболее часто в выборке)

    mode_l1 = pd.Series(data_l1).mode()[0]
    mode_l2 = pd.Series(data_l2).mode()[0]

    # так как бакеты достаточно малые, то значение моды не совпадает с гистограммой
    print("\nМода для l1:", mode_l1)
    print("Мода для l2:", mode_l2)

    # Определение точечной оценки дисперсии
    # меру разброса или изменчивости значений в выборке
    variance_l1 = np.var(data_l1)
    variance_l2 = np.var(data_l2)

    print("\nТочечная оценка дисперсии для l1:", variance_l1)
    print("Точечная оценка дисперсии для l2:", variance_l2)

    # Среднее квадратическое отклонение
    # насколько сильно каждое значение отклоняется от среднего значения
    print("\nСреднее квадратическое отклонение для l1:", math.sqrt(variance_l1))
    print("Среднее квадратическое отклонение для l2:", math.sqrt(variance_l2))


def plot_normal_distribution(data,m,sigma, title):

    # Генерация значений для построения графика
    # (параметры сглаживания прямой)
    x_values = np.linspace(m - 3 * sigma, m + 3 * sigma, 100)
    y_values = norm.pdf(x_values, m, sigma)

    # Плотность вероятности для нормального распределения
    plt.plot(x_values, y_values, label='Нормальное распределение')
    plt.hist(data, bins=20, density=True, alpha=0.7, label='Гистограмма')

    # Настройка графика
    plt.title('Гауссовское распределение с плотностью вероятности для ' + title)
    plt.xlabel('Значение')
    plt.ylabel('Плотность вероятности')
    plt.legend()
    plt.show()


print("RAND_______________________________")
# rand_l1, rand_l2 = getRandValues()
# compute(rand_l1, rand_l2, 'RAND')

# # Нормальное (гауссово) распределение
# print("GAUS_______________________________")
# # GAUS
m = 8  # Среднее значение
sigma = 8 / 3  # Стандартное отклонение
gauss_l1, gauss_l2 = getGausValues(m, sigma, 'gauss')
compute(gauss_l1, gauss_l2, 'GAUSS')
plot_normal_distribution(gauss_l1,m, sigma, 'l1')
plot_normal_distribution(gauss_l2,m, sigma, 'l2')

# m = 0
# sigma = 1
# gauss_l1, gauss_l2 = getGausValues(m, sigma,'gauss_0_1')
# compute(gauss_l1, gauss_l2, 'GAUSS')
# plot_normal_distribution(gauss_l1,m, sigma, 'l1 (0,1)')
# plot_normal_distribution(gauss_l2,m, sigma, 'l2 (0,1)')
