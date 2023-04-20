import numpy as np
from scipy.special import logsumexp

# вспомогательные функции обработки данных

def normalize(a: np.ndarray):
    other = 1 - a
    return np.concatenate([a, other], axis=1)

def unnormalize(a: np.ndarray):
    return a[np.newaxis, 0]

def softmax(x, axis=None):
    return np.exp(x - logsumexp(x, axis=axis, keepdims=True))
def local_binary_pattern(img, P, R):
    """
    Compute LBP image of an image.

    Parameters
    ----------
    img : numpy array
        Input image.
    P : int
        Number of neighbors.
    R : float
        Radius of circle.

    Returns
    -------
    output : numpy array
        LBP image.

    """
    height, width = img.shape
    output = np.zeros((height, width), dtype=np.uint8)

    for i in range(R, height - R):
        for j in range(R, width - R):
            # Calculate center pixel's LBP value
            center = img[i, j]
            binary = ""
            for k in range(P):
                # Calculate coordinates of neighbor pixel
                x = i + int(round(R * np.cos(2 * np.pi * k / P)))
                y = j - int(round(R * np.sin(2 * np.pi * k / P)))
                # Calculate neighbor pixel's value
                neighbor = img[x, y]
                # Compare center pixel's value with neighbor pixel's value
                if neighbor >= center:
                    binary += "1"
                else:
                    binary += "0"
            # Convert binary value to decimal
            output[i, j] = int(binary, 2)

    return output
