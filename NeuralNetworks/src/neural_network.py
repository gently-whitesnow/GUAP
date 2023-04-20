from typing import List
from numpy import ndarray
from layer import Layer
from loss import Loss

# 1. Нейронная сеть принимает на вход набор данных X и последовательно пропускает его через каждый слой
# (а на самом деле — через ряд операций), и полученным результатом будет прогноз.
# 2. Прогноз сравнивается с У,чтобы рассчитать потери и сгенерировать «градиент потерь»,
# который является! частной производной потери по каждому элементу в последнем слое
# в сети (который и создает прогноз).
# 3. Наконец, этот градиент проходит по сети в обратном направлении через каждый уровень
# и вычисляются «градиенты параметров» — частная производная потерь по каждому из параметров.
# Результаты сохраняются.
#
# Содержит в себе список слоев, где каждый слой имеет количество нейронов и функцию активации. Вычисляет потери

class LayerBlock(object):

    def __init__(self, layers: List[Layer]):
        super().__init__()
        self.layers = layers

    def forward(self,
                X_batch: ndarray,
                inference=False) ->  ndarray:

        X_out = X_batch
        for layer in self.layers:
            X_out = layer.forward(X_out, inference)

        return X_out

    def backward(self, loss_grad: ndarray) -> ndarray:

        grad = loss_grad
        for layer in reversed(self.layers):
            grad = layer.backward(grad)

        return grad

    def params(self):
        for layer in self.layers:
            yield from layer.params

    def param_grads(self):
        for layer in self.layers:
            yield from layer.param_grads

    def __iter__(self):
        return iter(self.layers)

    def __repr__(self):
        layer_strs = [str(layer) for layer in self.layers]
        return f"{self.__class__.__name__}(\n  " + ",\n  ".join(layer_strs) + ")"


class NeuralNetwork(LayerBlock):
    '''
    Just a list of layers that runs forwards and backwards
    '''
    def __init__(self,
                 layers: List[Layer],
                 loss: Loss ,
                 seed: int = 1):
        super().__init__(layers)
        self.loss = loss
        self.seed = seed
        if seed:
            for layer in self.layers:
                setattr(layer, "seed", self.seed)

    def forward_loss(self,
                     X_batch: ndarray,
                     y_batch: ndarray,
                     inference: bool = False) -> float:

        prediction = self.forward(X_batch, inference)
        return self.loss.forward(prediction, y_batch)

    def train_batch(self,
                    X_batch: ndarray,
                    y_batch: ndarray,
                    inference: bool = False) -> float:

        prediction = self.forward(X_batch, inference)

        batch_loss = self.loss.forward(prediction, y_batch)
        loss_grad = self.loss.backward()

        self.backward(loss_grad)

        return batch_loss