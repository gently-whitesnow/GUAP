import numpy as np
from numpy import ndarray

from param_operation import ParamOperation

# Операция добавления отклонения для входных параметров

class BiasAdd(ParamOperation):

    def __init__(self,
                 B: ndarray):
        super().__init__(B)

    def _output(self, inference: bool) -> ndarray:
        return self.input_ + self.param

    def _input_grad(self, output_grad: ndarray) -> ndarray:
        return np.ones_like(self.input_) * output_grad

    def _param_grad(self, output_grad: ndarray) -> ndarray:
        output_grad_reshape = np.sum(output_grad, axis=0).reshape(1, -1)
        param_grad = np.ones_like(self.param)
        return param_grad * output_grad_reshape