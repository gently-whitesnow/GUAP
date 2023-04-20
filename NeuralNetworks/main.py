import pickle
import sys

from src import dataset
from src.estimate import calc_accuracy_model

sys.path.insert(0, '/Users/gently/Projects/GUAP/NeuralNetworks/src')
from src.activations import Tanh, Linear
from src.dense import Dense
from src.softmax_cross_entropy import SoftmaxCrossEntropy
from src.neural_network import NeuralNetwork
from src.optimizer import SGDMomentum
from src.trainer import Trainer


x_params_train, x_params_test, y_ans_train, y_ans_test = dataset.load()

model = NeuralNetwork(
    layers=[Dense(neurons=202,
                  activation=Tanh(), weight_init="glorot", dropout=0.9),
            Dense(neurons=101,
                  activation=Tanh(),
                  weight_init="glorot",
                  dropout=0.9),
            Dense(neurons=10,
                  activation=Linear(), weight_init="glorot")],
            loss = SoftmaxCrossEntropy(),
seed=14121999)

trainer = Trainer(model, SGDMomentum(lr = 0.01, momentum = 0.9, final_lr=0.05, decay_type='exponential'))
trainer.fit(x_params_train, y_ans_train, x_params_test, y_ans_test,
            epochs = 50,
            eval_every = 10,
            seed=14121999,
            batch_size=60)
print()

with open("neural_tetwork_dump", 'wb') as file:
    pickle.dump(model, file)

calc_accuracy_model(model, x_params_test, y_ans_test)





