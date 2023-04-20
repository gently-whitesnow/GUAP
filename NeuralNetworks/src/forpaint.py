from os import listdir
from os.path import isfile, join

import numpy as np
from PIL import Image
from numpy import asarray
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
# https://www.kaggle.com/datasets/mhantor/facial-expression

X = np.load('/Users/gently/Downloads/npdata/data.npy')
L = np.load('/Users/gently/Downloads/npdata/label.npy')
print(X.shape)
print(L.shape)
print(L[14888])
first_layer = X[14888][:, :, 1]
print(first_layer)
plt.imshow(first_layer, cmap='gray', vmin=0, vmax=256)
plt.show()

# path = '/Users/gently/Downloads/lfwcrop_color/faces'
# onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]
# print(len(onlyfiles))
# image = Image.open(path+"/"+onlyfiles[1488])
#
# numpydata = asarray(image)
#
# new_data = np.zeros((64, 192, 1), dtype=np.uint8)
# new_data[:, :64, 0] = numpydata[:, :, 0]
#
# # Copy the green channel of the RGB data to the second third of the new array
# new_data[:, 64:128, 0] = numpydata[:, :, 1]
#
# # Copy the blue channel of the RGB data to the last third of the new array
# new_data[:, 128:, 0] = numpydata[:, :, 2]
#
# print(new_data.shape)
# plt.subplot(131)
# plt.imshow(new_data, cmap='Greens', vmin=0, vmax=256)
#
# plt.show()


#
# path = '/Users/gently/Downloads/lfwcrop_color/lists'
# imgpath = '/Users/gently/Downloads/lfwcrop_color/faces'
# onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]
# i = 0
# for filec in range(4):
#     with open(path + "/" + onlyfiles[filec]) as f:
#         lines = f.read().split()
#         # lines = lines.sort()
#         print(lines)
#         print(onlyfiles[filec])
#         for img in range(4):
#             image = Image.open(imgpath + "/" + lines[img]+".ppm").convert("LA")
#             numpydata = asarray(image)
#             first_layer = numpydata[:, :, 0]
#             print(i)
#             plt.subplot(11,4,i+1)
#             plt.title(i)
#             i += 1
#             plt.imshow(first_layer, cmap='gray', vmin=0, vmax=256)
# plt.show()
