import { makeAutoObservable, configure } from "mobx";

class ViewStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  value = {};

  setValue = (value) => {
    this.value = value;
  };
}

export default ViewStore;
