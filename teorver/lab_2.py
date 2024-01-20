import numpy as np
import matplotlib.pyplot as plt
import os


def run():
    # Заданные параметры
    a = -8 / 10
    b = 8 / 2
    l1 = 100
    l2 = 1000

    # Пути к файлам
    file_path_l1 = 'lab_2_data_l1.txt'
    file_path_l2 = 'lab_2_data_l2.txt'

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

    # Визуализация данных
    plt.figure(figsize=(12, 5))

    plt.subplot(1, 2, 1)
    plt.hist(data_l1, bins=20, edgecolor='black', alpha=0.7)
    plt.title('Histogram for l1')

    plt.subplot(1, 2, 2)
    plt.hist(data_l2, bins=20, edgecolor='black', alpha=0.7)
    plt.title('Histogram for l2')

    plt.show()
