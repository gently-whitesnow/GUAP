# производная
import numpy as np

func = np.poly1d([3, 2, 5])
print("Source func:", func)

derivative = func.deriv()

print(derivative)
print(derivative(5))

