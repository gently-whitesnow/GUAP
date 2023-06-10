import sys
sys.path.insert(0, '/Users/gently/Projects/GUAP/NeuralNetworks/src')
import pickle
import numpy as np
from os import listdir
from os.path import isfile, join
from PIL import Image
from matplotlib import pyplot as plt
from numpy import asarray


imgpath = '/Users/gently/test-images'
onlyfiles = [f for f in listdir(imgpath) if isfile(join(imgpath, f)) and join(imgpath, f).__contains__("photo")]
print(len(onlyfiles))
i=1
imgdata = []
for img in onlyfiles:
    image = Image.open(imgpath + "/" + img).convert("LA")
    numpydata = asarray(image)
    first_layer = numpydata[:, :, 0]
    plt.subplot(4, 2, i)
    plt.title(i)
    i += 1
    plt.imshow(first_layer, cmap='gray', vmin=0, vmax=256)
    imgdata.append(first_layer)
plt.show()

with open("neural_tetwork_dump", 'rb') as file:
    model = pickle.load(file)
    imgdata = np.array(imgdata)
    imgdata = np.reshape(imgdata, (imgdata.shape[0], -1))
    print(model.forward(np.reshape(imgdata, (imgdata.shape[0], -1)), inference=True))