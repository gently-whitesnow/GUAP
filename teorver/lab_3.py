import numpy as np
import matplotlib.pyplot as plt
import os

# вариант
c = 8


def getValues(size, func, arg1, arg2, title):
    # Пути к файлам
    file_path = f'lab_3_{title}-{arg2}_{size}.txt'

    # Проверка наличия данных
    if os.path.isfile(file_path):
        # Если данные уже сгенерированы, загружаем их
        data = np.loadtxt(file_path, delimiter=',')
    else:
        # Генерация данных
        data = func(arg1, arg2)

        # Запись данных в файл
        np.savetxt(file_path, data, delimiter=',')
    return data


# равномерное распределение
def getRandValues(a, b, size):
    def func(a, b):
        # Формирование выборок для интервала (0, 1)
        noise_01 = np.random.rand(size)

        # Преобразование выборок к интервалу (a, b)
        noise_ab = (b - a) * noise_01 + a
        return noise_ab

    return getValues(size, func, a, b, 'rand')


# нормальное распределение
def getGausValues(m, sigma, size):
    func = lambda m, sigma: np.random.normal(m, sigma, size)
    return getValues(size, func, m, sigma, 'gauss')


def plot_samples(datas, labels, title):
    plt.figure(figsize=(12, 5))
    for i, data in enumerate(datas):
        plt.subplot(1, len(datas), i + 1)
        plt.hist(data, bins=20, edgecolor='black', alpha=0.7)
        plt.title(labels[i])
    plt.suptitle(title)
    plt.show()


def case(func, arg1, arg2, title):
    case15 = func(arg1, arg2, 15)
    case30 = func(arg1, arg2, 30)
    case100 = func(arg1, arg2, 100)
    case1000 = func(arg1, arg2, 1000)
    datas = [case15, case30, case100, case1000]
    labels = ['15', '30', '100', '1000']
    # для отображения как сгенерировался шум
    plot_samples(datas, labels, title)

    # Расчет ряда наблюдаемых значений array[i] = c + noise[i]
    case15 = case15 + c
    case30 = case30 + c
    case100 = case100 + c
    case1000 = case1000 + c

    # Вычисление среднего значения для каждого случая
    mean_case15 = np.mean(case15)
    mean_case30 = np.mean(case30)
    mean_case100 = np.mean(case100)
    mean_case1000 = np.mean(case1000)

    print(f"Среднее значение для 15: {mean_case15}")
    print(f"Среднее значение для 30: {mean_case30}")
    print(f"Среднее значение для 100: {mean_case100}")
    print(f"Среднее значение для 1000: {mean_case1000}")

    # Вычисление полусуммы минимального и максимального элементов
    half_sum_min_max_15 = 0.5 * (np.min(case15) + np.max(case15))
    half_sum_min_max_30 = 0.5 * (np.min(case30) + np.max(case30))
    half_sum_min_max_100 = 0.5 * (np.min(case100) + np.max(case100))
    half_sum_min_max_1000 = 0.5 * (np.min(case1000) + np.max(case1000))

    print(f"Оценка (полусумма мин и макс) для 15: {half_sum_min_max_15}")
    print(f"Оценка (полусумма мин и макс) для 30: {half_sum_min_max_30}")
    print(f"Оценка (полусумма мин и макс) для 100: {half_sum_min_max_100}")
    print(f"Оценка (полусумма мин и макс) для 1000: {half_sum_min_max_1000}")

    # вычисления выборочной медианы
    median_case15 = np.median(case15)
    median_case30 = np.median(case30)
    median_case100 = np.median(case100)
    median_case1000 = np.median(case1000)

    print(f"Выборочная медиана для 15: {median_case15}")
    print(f"Выборочная медиана для 30: {median_case30}")
    print(f"Выборочная медиана для 100: {median_case100}")
    print(f"Выборочная медиана для 1000: {median_case1000}")

    # Добавление вычисления среднего с отбросом крайних членов вариационного ряда
    k = 2

    # Вычисление числа элементов для отброса
    num_elements_to_drop = int(k / 2)

    # Сортировка вариационного ряда
    sorted_case15 = np.sort(case15)
    sorted_case30 = np.sort(case30)
    sorted_case100 = np.sort(case100)
    sorted_case1000 = np.sort(case1000)

    # Вычисление среднего с отбросом
    mean_drop_15 = np.mean(sorted_case15[num_elements_to_drop:-num_elements_to_drop])
    mean_drop_30 = np.mean(sorted_case30[num_elements_to_drop:-num_elements_to_drop])
    mean_drop_100 = np.mean(sorted_case100[num_elements_to_drop:-num_elements_to_drop])
    mean_drop_1000 = np.mean(sorted_case1000[num_elements_to_drop:-num_elements_to_drop])

    print(f"Среднее с отбросом для 15 (k={k}): {mean_drop_15}")
    print(f"Среднее с отбросом для 30 (k={k}): {mean_drop_30}")
    print(f"Среднее с отбросом для 100 (k={k}): {mean_drop_100}")
    print(f"Среднее с отбросом для 1000 (k={k}): {mean_drop_1000}")

    # Добавление вычисления оценки дисперсии
    variance_estimate_15 = np.var(case15, ddof=1)
    variance_estimate_30 = np.var(case30, ddof=1)
    variance_estimate_100 = np.var(case100, ddof=1)
    variance_estimate_1000 = np.var(case1000, ddof=1)

    print(f"Оценка дисперсии для 15: {variance_estimate_15}")
    print(f"Оценка дисперсии для 30: {variance_estimate_30}")
    print(f"Оценка дисперсии для 100: {variance_estimate_100}")
    print(f"Оценка дисперсии для 1000: {variance_estimate_1000}")

    # Вычисление относительных ошибок оценивания
    error_x1 = np.abs((mean_case15 - 15) / 15)
    error_x2 = np.abs((mean_case30 - 30) / 30)
    error_x3 = np.abs((mean_case100 - 100) / 100)
    error_x4 = np.abs((mean_case1000 - 1000) / 1000)

    print(f"Относительная ошибка для x1: {error_x1}")
    print(f"Относительная ошибка для x2: {error_x2}")
    print(f"Относительная ошибка для x3: {error_x3}")
    print(f"Относительная ошибка для x4: {error_x4}")


# A
print('A')
case(getGausValues, 0, c / 100, 'A')
# B
print('\nB')
case(getGausValues, 0, c / 20, 'B')
# C
print('\nC')
case(getRandValues, -c / 100, c / 100, 'C')
# D
print('\nD')
case(getRandValues, -c / 20, c / 20, 'D')
