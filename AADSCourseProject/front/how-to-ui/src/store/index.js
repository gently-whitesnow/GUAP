import React from "react";
import TestStore from "./testStore";
import ColorStore from "./colorStore";
import SummaryStore from "./summaryStore";
import CourseStore from "./courseStore";
import ArticleStore from "./articleStore";
import ViewStore from "./viewStore";
import StateStore from "./stateStore";

class Store {
  constructor() {
    this.testStore = new TestStore(this);
    this.colorStore = new ColorStore(this);

    this.summaryStore = new SummaryStore(this);
    this.courseStore = new CourseStore(this);
    this.articleStore = new ArticleStore(this);
    this.viewStore = new ViewStore(this);

    this.stateStore = new StateStore(this);
  }
}

export const storeContext = React.createContext(new Store());
export const useStore = () => React.useContext(storeContext);
