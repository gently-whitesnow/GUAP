export const adjustFontSize = (ref, offset) => {
  const textarea = ref.current;
  if (!textarea) return;

  // временный костыль, ибо я не знаю как лучше сделать)
  const firstAcceptable = 26;

  const threshold = 1;
  while (Math.abs(textarea.scrollHeight - textarea.clientHeight) > threshold) {
    const computedStyle = getComputedStyle(textarea);
    const fontSize = parseInt(computedStyle.fontSize);
    if (fontSize > firstAcceptable) {
      textarea.style.fontSize = firstAcceptable + "px";
      break;
    }
    if (textarea.scrollHeight > textarea.clientHeight) {
      textarea.style.fontSize = fontSize - offset + "px";
    } else if (textarea.scrollHeight < textarea.clientHeight) {
      textarea.style.fontSize = fontSize + offset + "px";
    }
  }
};

export const clearTextAreaContent = (value) => {
  value = value.replace('\n',"")
  return value;
};
