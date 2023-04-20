import numpy as np
from numpy import ndarray

from param_operation import ParamOperation

# Операция умножения матрицы входных параметров на их веса и при обратном проходе градиент операции и ее параметров

class WeightMultiply(ParamOperation):

    def __init__(self, W: ndarray):
        super().__init__(W)

    def _output(self, inference: bool) -> ndarray:
        return np.matmul(self.input_, self.param)

    def _input_grad(self, output_grad: ndarray) -> ndarray:
        return np.matmul(output_grad, self.param.transpose(1, 0))

    def _param_grad(self, output_grad: ndarray) -> ndarray:
        return np.matmul(self.input_.transpose(1, 0), output_grad)