import numpy as np

# Вспомогательные функции для оценки модели

def calc_accuracy_model(model, test_set, y_test):
    return print(f'''The model validation accuracy is: {np.equal(np.argmax(model.forward(test_set, inference=True), axis=1), np.argmax(y_test, axis=1)).sum() * 100.0 / test_set.shape[0]:.2f}%''')