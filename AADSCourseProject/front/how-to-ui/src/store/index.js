import React from "react";
import TestStore from "./testStore";
import ColorStore from "./colorStore";

class Store {
  constructor() {
    this.testStore = new TestStore(this);
    this.colorStore = new ColorStore(this);
  }
}

export const storeContext = React.createContext(new Store());
export const useStore = () => React.useContext(storeContext);
