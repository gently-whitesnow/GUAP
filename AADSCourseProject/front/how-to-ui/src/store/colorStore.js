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

  currentColorTheme = theme.colors.blue;

  setColorTheme = () => {
    this.currentColorTheme = theme.ReadableColors[
      Math.floor(Math.random() * theme.ReadableColors.length)
    ];
  };

  getColorTheme = () => {
    return this.currentColorTheme;
  };
}

export default ColorStore;
