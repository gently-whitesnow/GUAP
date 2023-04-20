from numpy import ndarray
from asserts import assert_same_shape
from operation import Operation

# Единица логики, содержащая параметры прохода (веса и смещения) и возвращающая градиент по функции и по параметрам

class ParamOperation(Operation):

    def __init__(self, param: ndarray) -> ndarray:
        super().__init__()
        self.param = param

    def backward(self, output_grad: ndarray) -> ndarray:

        assert_same_shape(self.output, output_grad)

        self.input_grad = self._input_grad(output_grad)
        self.param_grad = self._param_grad(output_grad)

        assert_same_shape(self.input_, self.input_grad)

        return self.input_grad

    def _param_grad(self, output_grad: ndarray) -> ndarray:
        raise NotImplementedError()