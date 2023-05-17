export const adjustFontSize = (ref, offset) => {
    const textarea = ref.current;
    if (!textarea) return;
    while (textarea.scrollHeight != textarea.clientHeight) {
      const computedStyle = getComputedStyle(textarea);
      const fontSize = parseInt(computedStyle.fontSize);

      if (textarea.scrollHeight > textarea.clientHeight) {
        textarea.style.fontSize = fontSize - offset + "px";
      } else if (textarea.scrollHeight < textarea.clientHeight) {
        textarea.style.fontSize = fontSize + offset + "px";
      } else {
        break;
      }
    }
  };