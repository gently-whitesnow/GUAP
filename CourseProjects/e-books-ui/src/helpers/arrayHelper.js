export function shuffle(arr) {
  var j, temp;
  for (var i = arr.length - 1; i > 0; i--) {
    j = Math.floor(Math.random() * (i + 1));
    temp = arr[j];
    arr[j] = arr[i];
    arr[i] = temp;
  }
  return arr;
}

export const BoolArrayChanged = (initialArray, dynamicArray) => {
  for (let index = 0; index < dynamicArray.length; index++) {
    let initialValue = false;
    if (initialArray.length > index)
      initialValue = initialArray[index] ?? false;
    if (initialValue !== (dynamicArray[index] ?? false)) return true;
  }
  return false;
};

export const CopyArray = (fromArray, toArray) => {
  if(!fromArray) return fromArray;

  for (let i = 0; i < fromArray.length; i++) {
    if (toArray.length <= i) toArray.push(fromArray[i]);
    else toArray[i] = fromArray[i];
  }
  return toArray;
};

export const NullToFalse = (array) => {
  for (let i = 0; i < array.length; i++) {
    if (array[i] === undefined) array[i] = false;
  }
  return array;
};

export const AllUndefined = (array) => {
  for (let i = 0; i < array.length; i++) {
    array[i] = undefined;
  }
  return array;
};
