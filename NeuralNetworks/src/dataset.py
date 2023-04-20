import numpy as np
from sklearn.model_selection import train_test_split

from src.common import local_binary_pattern


# https://www.kaggle.com/datasets/ardamavi/sign-language-digits-dataset
def load():
    data_images = np.load('/Users/gently/Downloads/gestures_data/X.npy')
    data_labels = np.load('/Users/gently/Downloads/gestures_data/Y.npy')

    flat_data = np.reshape(data_images, (data_images.shape[0], -1))
    # разделение на обучающую часть и тренировочную
    x_params_train, x_params_test, y_ans_train, y_ans_test = train_test_split(flat_data, data_labels, test_size=0.2,
                                                                              random_state=80718)
    # вычитания общего среднего
    x_params_train, x_params_test = x_params_train - np.mean(x_params_train), x_params_test - np.mean(x_params_train)

    # деления на общую дисперсию
    x_params_train, x_params_test = x_params_train / np.std(x_params_train), x_params_test / np.std(x_params_train)

    return x_params_train, x_params_test, y_ans_train, y_ans_test

load()