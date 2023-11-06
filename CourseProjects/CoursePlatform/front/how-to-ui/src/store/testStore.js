import { makeAutoObservable, configure } from "mobx";

class TestStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  testModuleCounters = new Map();

  increaseCounter = (id) => {
    if(this.testModuleCounters.has(id)){
      this.testModuleCounters.get(id).val++;
    }
    else{
      this.testModuleCounters.set(id,{val:1});
    }
  };

  getCounterValue = (id) => {
    if(this.testModuleCounters.has(id)){
      return this.testModuleCounters.get(id).val;
    }
    return 0;
  };
}

export default TestStore;
