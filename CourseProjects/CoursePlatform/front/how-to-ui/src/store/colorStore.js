import { makeAutoObservable, configure } from "mobx";
import theme from "../theme";

class ColorStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  linearColorList = new LinearList({
    color: theme.colors.blue,
    next: {
      color: theme.colors.green,
      next: {
        color: theme.colors.violet,
        next: { color: theme.colors.orange, next: null },
      },
    },
  });
  currentColorTheme = this.linearColorList.getCurrentNode().color;

  setColorTheme = () => {
    this.linearColorList.next();
    this.currentColorTheme = this.linearColorList.getCurrentNode().color;
  };
}

export default ColorStore;


class LinearList {
  _first = {};
  _current = {};
  constructor(first) {
    this._first = first;
    this._current = first;
  }
  next() {
    // не используется циклический список, потому что 
    // ноды создаются на стеке и рекурсивно переполняют его
    if (this._current.next === null) {
      this._current = this._first;
      return;
    }
    this._current = this._current.next;
  }

  getCurrentNode() {
    return this._current;
  }
}
