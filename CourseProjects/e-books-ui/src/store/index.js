import React from "react";
import PopupStore from "./popupStore";
import StateStore from "./stateStore";
import SummaryStore from "./summaryStore";
import BookStore from "./bookStore";


class Store {
  constructor() {
    this.popupStore = new PopupStore(this);
    this.stateStore = new StateStore(this);
    this.summaryStore = new SummaryStore(this);
    this.bookStore = new BookStore(this);
  }
}

export const storeContext = React.createContext(new Store());
export const useStore = () => React.useContext(storeContext);
